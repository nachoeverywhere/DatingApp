using System;
using DatingApp.API.Data;
using DatingApp.API.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DatingApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

           var host = CreateHostBuilder(args).Build();

            // Hago los siguientes pasos para cargar la base de datos con datos de prueba desde un JSON.
           using (var scope = host.Services.CreateScope())
           {
               var servicios = scope.ServiceProvider;
               try
               {
                   var context = servicios.GetRequiredService<AplicacionContext>();
                   context.Database.Migrate(); // El metodo "migrate" aplica las migraciones que haya pendientes al momento de correr el codigo. 
                   Seed.SeedUsers(context); // Seed es la clase tengo creada para cargar el JSON que genere. 
               }
               catch (Exception ex)
               {
                   var logger = servicios.GetRequiredService<ILogger<Program>>(); // ILogger reqiere el tipo de clase que va a loggear, en este caso es "Program"
                   logger.LogError(ex, "Ocurrio un error en el proceso de migracion");
                   throw;
               }
           }
           
           host.Run(); // Inicializo el programa, lo corro con los agregados anteriores. 

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
