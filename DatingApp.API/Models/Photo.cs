using System;

namespace DatingApp.API.Models
{
    public class Photo
    {
        public int id {get; set;}
        public string Url { get; set; }
        public string Descripcion { get; set; }
        public string PublicId { get; set; }
        public bool EsPrincipal {get; set;}
        public DateTime FechaSubida {get; set;}
        public virtual Usuario Usuario {get; set;}
        public int UsuarioId {get; set;}
    }
}