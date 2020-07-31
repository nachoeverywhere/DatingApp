using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Interfaces;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Repositorios
{
    public class AppRepositorio : IAppRepositorio
    {
        private readonly AplicacionContext Db;
        public AppRepositorio(AplicacionContext db)
        {
            Db = db;

        }

        // Estos metodos no son asyncronos ya que cuando agregamos algo en realidad no estamos agregandolo a la BD, sino que lo estamos .
        public void Add<T>(T entity) where T : class
        {
            Db.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
             Db.Remove(entity);
        }

        public async Task<Usuario> GetUsuario(int id)
        {   //En este caso si es async por que consulta directamente contra la base.
            var usuario = await Db.Usuarios.Include(p => p.FotosPublicas).FirstOrDefaultAsync(u => u.Id == id);
            return usuario;
        }

        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            var usuarios = await Db.Usuarios.Include(p => p.FotosPublicas).ToListAsync();
            return usuarios;
        }

        public async Task<bool> GuardarCambios()
        {
            //Por cada cambio en la base de dato devuelve +1, entonces si comparo con > 0 se puede determinar si guardo o no. 
            return await Db.SaveChangesAsync() > 0;
        }

        public async Task<Photo> ObtenerFotoPrincipalUsuario(int usuarioId)
        {
            var photo = await Db.Photos.FirstOrDefaultAsync(p => p.UsuarioId == usuarioId && p.EsPrincipal == true);
            return photo;
        }

        public async Task<Photo> ObtenerPhoto(int id)
        {
            var photo = await Db.Photos.FirstOrDefaultAsync(p => p.id == id);
            return photo;
        }
    }
}