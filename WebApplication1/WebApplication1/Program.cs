using impresion3d.Infrastructure.ExtensionesServicios;

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
            

            var app = builder.Build();

            

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
