using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AplicacionContext : DbContext
    {
        public AplicacionContext(DbContextOptions<AplicacionContext> options) : base(options){}

        public DbSet<Valor> Valores {get; set;}
        public DbSet<Usuario> Usuarios {get; set;}
    }
}