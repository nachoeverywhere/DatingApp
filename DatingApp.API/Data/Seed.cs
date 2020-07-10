using System.Collections.Generic;
using System.Linq;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Newtonsoft.Json;

namespace DatingApp.API.DTOs
{
    public class Seed
    {
        // Serializar significa convertir de JSON a objeto. 
        // Hago SeedUsers statica para no tener que instanciarme. 
        public static void SeedUsers(AplicacionContext context)
        {
            // Chequeo que no hayan usuarios en la base de datos.
            if(!context.Usuarios.Any()){
                var dataUsuario = System.IO.File.ReadAllText("DTOs/SeedDataUsuario.json");
                var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(dataUsuario);
                foreach(var usuario in usuarios){
                    byte[] passwordhash, passwordsalt;
                    CrearPasswordHash("password", out passwordhash, out passwordsalt);
                    usuario.PasswordHash = passwordhash;
                    usuario.PasswordSalt = passwordsalt;
                    usuario.NombreUsuario = usuario.NombreUsuario.ToLower();
                    context.Usuarios.Add(usuario);
                }
                context.SaveChanges();
            }

        }

        private static void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; //La 'sal' se genera aleatoriamente con cada instancia de hmac nueva. (Key)
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                // Para computar (crear) el hash preciso pasarle un byte array, esto se lleva a cabo con el metodo GetBytes de System.Text.Encoding
            }
        }
    }
}