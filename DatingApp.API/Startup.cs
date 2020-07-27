using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Helpers;
using DatingApp.API.Interfaces;
using DatingApp.API.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AplicacionContext>(c => c.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
           
           // Uso esto para evitar error de dependencias circulares al momento de obtener objetos (Usuario-Photo-Usuario-Photo-Usuario..)
            services.AddControllers().AddNewtonsoftJson(opt => {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            //Esto hace un binding de appsettings con la clase confcloudinary que cree (MIND-BLOWN! :O)
            services.Configure<ConfCloudinary>(Configuration.GetSection("Cloudinary"));
            services.AddCors();
            services.AddAutoMapper(typeof(AppRepositorio).Assembly);
            // Hago los repositorios accesibles a nivel global del programa.
            services.AddScoped<IAuthRepositorio, AuthRepositorio>();
            services.AddScoped<IAppRepositorio, AppRepositorio>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
            AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                    .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false

                };

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else // Agrego el else y el contenido para manejar excepciones globales. 
            { 
                app.UseExceptionHandler(builder => {
                    builder.Run(async context => { // Corro la respuesta del contexto (muestro el error que obtengo en los pasos siguientes)
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>(); //Recibo el el error devuelto por la excepcion 
                        if (error != null){
                            context.Response.AgregarErrorDeAplicacion(error.Error.Message); //Creo un metodo de extension para poder procesar los errores y mostrarlos con mas informacion. (En el formato que quiera)
                            await context.Response.WriteAsync(error.Error.Message); // Me quedo con el texto del error 
                        }
                    });
                });
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
