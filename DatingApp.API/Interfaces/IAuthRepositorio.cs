using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Interfaces
{
    public interface IAuthRepositorio
    {
         Task<Usuario> Register(Usuario user, string password);
         Task<Usuario> Login(string user, string password);
         Task<bool> UserExists(string username);
    }
}