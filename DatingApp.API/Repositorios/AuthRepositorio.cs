using System;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Interfaces;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Repositorios
{
    public class AuthRepositorio : IAuthRepositorio
    {
        private readonly AplicacionContext Db;

        public AuthRepositorio(AplicacionContext db)
        {
            this.Db = db;

        }

        public async Task<Usuario> Login(string username, string password)
        {
            var user = await Db.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == username);
            
            if(user == null) return null;
            if(!VerificarPasswordHash(password, user.PasswordSalt, user.PasswordHash))return null; // Metodo propio, creado.

            return user;
        }

        private bool VerificarPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
        {
               using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) //Al pasarle la 'sal' (key), el metodo genera el hash para ese valor.
            { // Uso el using por un tema de seguridad, para que la variable muera fuera de los corchetes, asi lo borramos antes que el garbaje colector.
                var hashCalculado = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // Si el hash desencriptado, coincide con el hash que se genera al computar un hash con el password que ingreso el usuario significa que el password esta correcto.
                for (int i = 0; i < hashCalculado.Length; i++)
                {
                    if (hashCalculado[i] != passwordHash[i]) return false;   // Comparo cada posicion del byte array, en caso de que no coincida algun caracter no valida.
                }
            }
            return true;
        }

        public async Task<Usuario> Register(Usuario user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CrearPasswordHash(password, out passwordHash, out passwordSalt); // Metodo propio creado.
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await Db.Usuarios.AddAsync(user);
            await Db.SaveChangesAsync();

            return user;
        }

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; //La 'sal' se genera aleatoriamente con cada instancia de hmac nueva. (Key)
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                // Para computar (crear) el hash preciso pasarle un byte array, esto se lleva a cabo con el metodo GetBytes de System.Text.Encoding
            }
        }

        public async Task<bool> UserExists(string username)
        {
             if(await Db.Usuarios.AnyAsync(u => u.NombreUsuario == username)) return true;

             return false;
        }
    }
}