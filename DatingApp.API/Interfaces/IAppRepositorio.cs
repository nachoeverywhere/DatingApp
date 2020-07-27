using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Interfaces
{
    public interface IAppRepositorio
    {
         void Add<T>(T entity) where T: class; // Generico indico que agrego y restrinjo a clases unicamente;
         void Delete<T>(T entity) where T: class; 
         Task<bool> GuardarCambios();
         Task<IEnumerable<Usuario>> GetUsuarios();
         Task<Usuario> GetUsuario(int id);
        Task<Photo> ObtenerPhoto(int id);
    }
}