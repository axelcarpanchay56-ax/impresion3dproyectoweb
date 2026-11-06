using impresion3d.Infrastructure.ExtensionesServicios;
using FluentValidation;
using impresion3d.Api.DTOs;
using impresion3d.Api.Validadores; 

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Servicios
            builder.Services.AddControllers();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddScoped<IValidator<CrearPersonaDto>, PersonaCrearValidator>(); 


            var app = builder.Build();

            

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
