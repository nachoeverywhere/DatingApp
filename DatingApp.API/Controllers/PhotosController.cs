using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using DatingApp.API.DTOs;
using DatingApp.API.Helpers;
using DatingApp.API.Interfaces;
using DatingApp.API.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using DatingApp.API.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using CloudinaryDotNet.Actions;
using System.Linq;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/usuarios/{id}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IAppRepositorio repo;
        private readonly IMapper mapper;
        private readonly IOptions<ConfCloudinary> cloudinaryConfig;
        private Cloudinary cloudinary;
        // IMapper es propio de dotnet, IOptions transforma las appsetings a un objeto del tipo que especifique, hace un mappeo en base a los nombres de las properties. 
        public PhotosController(IAppRepositorio repo, IMapper mapper, IOptions<ConfCloudinary> cloudinaryConfig)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
                cloudinaryConfig.Value.CloudName,
                cloudinaryConfig.Value.ApiKey,
                cloudinaryConfig.Value.ApiSecret
            );

            this.cloudinary = new Cloudinary(acc);
        }
        [HttpGet("{idPhoto}", Name = "ObtenerPhoto")]
        public async Task<IActionResult> ObtenerPhoto(int idPhoto)
        {
            var photoRepositorio = await repo.ObtenerPhoto(idPhoto);
            var photo = mapper.Map<PhotoADevolverDTO>(photoRepositorio);

            return Ok(photo);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarFotoAUsuario(int id, [FromForm] PhotoACrearDTO photoDTO)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var usuario = await repo.GetUsuario(id);

            var archivo = photoDTO.Archivo;

            // UTILIZANDO LA LIBRERIA DE CLOUDINARY
            var resultado = new ImageUploadResult();

            if (archivo.Length > 0)
            {
                using (var stream = archivo.OpenReadStream())
                {
                    // File es el nombre del atributo de la clase ImageUploadParams (Propio de Cloudinary)
                    var parametrosDeSubida = new ImageUploadParams()
                    {
                        File = new FileDescription(archivo.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };
                    resultado = cloudinary.Upload(parametrosDeSubida);
                }
            }

            photoDTO.Url = resultado.Uri.ToString();
            photoDTO.PublicId = resultado.PublicId;

            var photo = mapper.Map<Photo>(photoDTO);

            if (!usuario.FotosPublicas.Any(u => u.EsPrincipal))
            {
                photo.EsPrincipal = true;
            }

            usuario.FotosPublicas.Add(photo);

            if (await repo.GuardarCambios())
            {
                var photoADevolver = mapper.Map<PhotoADevolverDTO>(photo);
                return CreatedAtRoute("ObtenerPhoto", new { id = usuario.Id, idPhoto = photo.id }, photoADevolver);
                // id es el parametro de id de usuario que requiere la ruta, idPhoto es la que requiere ObtenerPhoto
            }
            return BadRequest("No se pudo agregar la foto.");
        }
        [HttpPost("{idPhoto}/establecerPhotoPrincipal")] // Debiera ser un put, pero al estar modificando simplemente un atributo de true a false me parece mas practico asi. (Cons: La API no es tan Restful)
        public async Task<IActionResult> EstablecerPhotoPrincipal(int id, int idPhoto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var usuario = await repo.GetUsuario(id);

            if (!usuario.FotosPublicas.Any(p => p.id == idPhoto))
                return Unauthorized();
            
            var photo = await repo.ObtenerPhoto(idPhoto);

            if (photo.EsPrincipal)
                return BadRequest("La foto ya es principal.");
            
            var photoPrincipalActual = await repo.ObtenerFotoPrincipalUsuario(id);

            photoPrincipalActual.EsPrincipal = false;

            photo.EsPrincipal = true;

            if (await repo.GuardarCambios())
                return NoContent();
            return BadRequest("No se pudo actualizar el estado de la foto");
        }

    }
}