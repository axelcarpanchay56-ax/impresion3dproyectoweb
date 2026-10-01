using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using impresion3d.Core.Interfaces;
using impresion3d.Infrastructure.Contexto;

namespace impresion3d.Infrastructure.ExtensionesServicios
{
    public static class ExtensionesServicios
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "La cadena de conexión 'DefaultConnection' no está configurada."
                );

            services.AddSingleton<Imiconexion>(
                new MyConexion(connectionString)
            );

            return services;
        }
    }
}

