using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.DTOs
{
    public class UsuarioARegistrarDTO
    {
        [Required]
        [StringLength(12, MinimumLength = 4, ErrorMessage="El nombre de usuario debe tener entre 4 y 12 caracteres")]
        public string Username {get; set;}
        
        [Required]
        [StringLength(13, MinimumLength = 8, ErrorMessage="La clave debe tener entre 8 y 13 caracteres")]
        public string Password {get; set;}

        
    }
}