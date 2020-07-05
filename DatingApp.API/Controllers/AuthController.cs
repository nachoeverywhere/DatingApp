using System.Threading.Tasks;
using DatingApp.API.DTOs;
using DatingApp.API.Interfaces;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private readonly IAuthRepositorio Repo;

        public AuthController(IAuthRepositorio repo)
        {
            this.Repo = repo;
        }

        public async Task<IActionResult> Registrar(UsuarioARegistrarDTO form){ //FromBody no es necesario

        
            //if(!ModelState.IsValid) return BadRequest(ModelState); 
            // En caso de no utilizar [ApiController] la linea anterior es necesaria para validar (toma en cuenta las data annotations del modelo UsuarioARegistrarDTO)

            form.Username = form.Username.ToLower();
            if(await Repo.UserExists(form.Username))return BadRequest("El usuario ya existe");

            var usuarioACrear = new Usuario { 
             NombreUsuario = form.Username
            };

            var usuarioCreado = await Repo.Register(usuarioACrear, form.Password);

            return StatusCode(201);

        }
    }
}