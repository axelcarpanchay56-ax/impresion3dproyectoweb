using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using impresion3d.Core.Interfaces;
using impresion3d.Core.Servicios;
using impresion3d.Infrastructure.Contexto;
using impresion3d.Infrastructure.Repositorios;

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

            services.AddSingleton<IMiConexion>(
                new MyConexion(connectionString)
            );

            // Inyección de dependencias Módulo 3: Impresión 3D
            services.AddScoped<IModelo3DRepositorio, Modelo3DRepositorio>();
            services.AddScoped<ServicioModelo3D>();

            services.AddScoped<IImpresorasRepositorio, ImpresorasRepositorio>();
            services.AddScoped<ServicioImpresoras>();

            services.AddScoped<IEstadoImpresionRepositorio, EstadoImpresionRepositorio>();
            services.AddScoped<ServicioEstadoImpresion>();

            services.AddScoped<IMaterialesRepositorio, MaterialesRepositorio>();
            services.AddScoped<ServicioMateriales>();

            services.AddScoped<ITrabajoImpresionRepositorio, TrabajoImpresionRepositorio>();
            services.AddScoped<ServicioTrabajoImpresion>();

            services.AddScoped<IImpresoraMaterialRepositorio, ImpresoraMaterialRepositorio>();
            services.AddScoped<ServicioImpresoraMaterial>();

            services.AddScoped<IConsumoMaterialRepositorio, ConsumoMaterialRepositorio>();
            services.AddScoped<ServicioConsumoMaterial>();

            // Inyección de dependencias Módulo 4: Pedidos
            services.AddScoped<IPedidosRepositorio, PedidosRepositorio>();
            services.AddScoped<ServicioPedidos>();

            services.AddScoped<IDetallePedidoRepositorio, DetallePedidoRepositorio>();
            services.AddScoped<ServicioDetallePedido>();

            services.AddScoped<IEstadoPedidoRepositorio, EstadoPedidoRepositorio>();
            services.AddScoped<ServicioEstadoPedido>();

            return services;
        }
    }
}
