using System;
using System.Collections.Generic;
using DatingApp.API.Models;

namespace DatingApp.API.Models
{
    public class Usuario
    {
        public int Id {get; set;}
        public string NombreUsuario { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Genero { get; set; }
        public string ConocidoComo { get; set; }
        public string Introduccion { get; set; }
        public string Intereses {get; set;}
        public string Buscando {get; set;}
        public string Ciudad {get; set;}
        public string Pais {get; set;}
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaUltActivo { get; set; }
        public ICollection<Photo> FotosPublicas { get; set; }
        
        // public ICollection<Photo> FotosPrivadas { get; set; }

        // Para mejorar se podria implementar propiedades que sean utiles para los administradores del sistema sobre el usuario
    }
}