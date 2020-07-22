using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Models;
using DatingApp.API.Interfaces;
using DatingApp.API.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;
using System.Security.Claims;
using System;

namespace DatingApp.API.Controllers
{
    [Authorize] // Para poder controlar el que acceda un usuario logueado
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IAppRepositorio Db;
        // Tuve que instalar el nuget de AutoMapper
        // AutoMapper se encarga de convertir de un tipo de clase a otra en base a los nombres de los atributos (Ideal para trabajar con dtos)
        // Para la edad y demas tuve que crearle un perfil (AutoMapperProfile)
        private readonly IMapper Mapper;
        public UsuariosController(IAppRepositorio db, IMapper mapper)
        {
            this.Mapper = mapper;
            this.Db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await Db.GetUsuarios();
            var usuariosADevolver = Mapper.Map<IEnumerable<UsuarioEnListaDTO>>(usuarios);
            return Ok(usuariosADevolver);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var usuario = await Db.GetUsuario(id);
                                          //Clase destino    //Clase Origen
            var usuarioADevolver = Mapper.Map<UsuarioDetallesDTO>(usuario);

            return Ok(usuarioADevolver);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarUsuario(int id, UsuarioAEditarDTO usuarioEdit)
        {
            // Verifico que el usuario que esta intentando actualizar su perfil coincide con el perfil a editar
            // Lo que hace esta linea es obtener el NameIdentifier del token del usuario, convertirlo en int y compararlo con el id del parametro. 

            // This ClaimType comes from the HttpRequest context, which has access to the token that was sent up by the client to the API.    From here we can get the user id so it will always match with the currently logged on user.
            
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            return Unauthorized();

            var usuario = await Db.GetUsuario(id);
            Mapper.Map(usuarioEdit, usuario);

            if (await Db.GuardarCambios()) return NoContent();
            throw new Exception("Ha ocurrido un error al actualizar los datos del usuario id:" + id);
        }


    }
}