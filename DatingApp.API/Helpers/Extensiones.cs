using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helpers
{
    public static class Extensiones //la hago statica para no tener que instanciarla cada vez que quiero llamar a sus metodos.
    {                                               // this captura el error que haya en el tiempo de ejecucion no lo recibe por parametro. 
        public static void AgregarErrorDeAplicacion(this HttpResponse respuesta, string mensaje)
        {
            respuesta.Headers.Add("Aplication-Error", mensaje);
            respuesta.Headers.Add("Access-Control-Expose-Headers", "Application-Error"); //Habilita que muestre los headers de Access-Control y Application-Error.
            respuesta.Headers.Add("Access-Control-Allow-Origin", "*"); // Permite que cualquier origen pueda verlos.
        }
    }
}