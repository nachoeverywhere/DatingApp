using System;

namespace DatingApp.API.DTOs
{
    public class PhotoDetallesDTO
    {
        
        public int id {get; set;}
        public string Url { get; set; }
        public string Descripcion { get; set; }
        public bool EsPrincipal {get; set;}
        public DateTime FechaSubida {get; set;}
        
    }
}