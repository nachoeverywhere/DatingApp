using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.DTOs;
using DatingApp.API.Interfaces;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {

        private readonly IAuthRepositorio Repo;
        private readonly IConfiguration Config;

        public AuthController(IAuthRepositorio repo, IConfiguration config)
        {
            this.Config = config;
            this.Repo = repo;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(UsuarioARegistrarDTO form)
        { //FromBody no es necesario


            //if(!ModelState.IsValid) return BadRequest(ModelState); 
            // En caso de no utilizar [ApiController] la linea anterior es necesaria para validar (toma en cuenta las data annotations del modelo UsuarioARegistrarDTO)

            form.Username = form.Username.ToLower();
            if (await Repo.UserExists(form.Username)) return BadRequest("El usuario ya existe");

            var usuarioACrear = new Usuario
            {
                NombreUsuario = form.Username
            };

            var usuarioCreado = await Repo.Register(usuarioACrear, form.Password);

            return StatusCode(201);

        }

        [HttpPost("ingresar")]
        public async Task<IActionResult> Ingresar(UsuarioAIngresarDTO login)
        {

            var usuario = await Repo.Login(login.Username.ToLower(), login.Password);
            if (usuario == null) return Unauthorized();

            // Las 'Claims' son la informacion publica que contiene el token, esta informacion se usa para veitar el ir a la base de datos
            // Hay que tener cuidado con no poner informacion sensible. 
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(Config.GetSection("AppSettings:Token").Value));
            //Crea una nueva 'key' (seria algo asi como la 'salt'). Primero hace un ByteArray con el encoding, luego lo llena en base a el campo Token de App settings (como si estuviera leyendo de un archivo de texto, o xlm), no es el mismo valor sino uno calculado en base a el.
            //Para poder hacer eso debo inyectar en el controlador un objeto del tipo IConfiguration (Config)

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //El tokenDescriptor contiene las claims, fecha de expracion y signin credentials.
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            //Inicializo un token handler para asi acceder a sus metodos.
            var tokenHandler = new JwtSecurityTokenHandler();

            //Creo el token usando un metodo del token handler.
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //Devuelvo un objeto tipo JSON (new{}) con un atributo llamado token, token va a ser el token a utilizar 
            // y lo escribo (algo asi como un toString) usando el metodo WriteToken del tokenHandler.
            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}