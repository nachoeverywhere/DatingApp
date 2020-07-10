using System;
using System.Collections.Generic;
using DatingApp.API.Models;

namespace DatingApp.API.DTOs
{
    public class UsuarioEnListaDTO
    {
        public int Id {get; set;}
        public string NombreUsuario { get; set; }
        public string Genero { get; set; }
        public string ConocidoComo { get; set; }
        public string Buscando {get; set;}
        public string Ciudad {get; set;}
        public string Pais {get; set;}
        public int Edad { get; set; } // Sustituyo Fnac por edad
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaUltActivo { get; set; }
        public string PhotoUrl {get;set;}
        
    }
}