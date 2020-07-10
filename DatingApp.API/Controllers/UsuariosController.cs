using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Models;
using DatingApp.API.Interfaces;
using DatingApp.API.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;

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

    }
}