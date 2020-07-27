using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.DTOs
{
    public class PhotoADevolverDTO
    {
        public string Url {get; set;}
        public string Descripcion {get; set;}
        public DateTime FechaSubida {get; set;}
        public bool EsPrincipal {get; set;}
        public string PublicId {get; set;}

    }
}