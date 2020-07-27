using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.DTOs
{
    public class PhotoACrearDTO
    {
        public string Url {get; set;}
        public IFormFile Archivo {get; set;}
        public string Descripcion {get; set;}
        public DateTime FechaSubida {get; set;}
        public string PublicId {get; set;}


        public PhotoACrearDTO()
        {
            FechaSubida = DateTime.Now;
        }
    }
}