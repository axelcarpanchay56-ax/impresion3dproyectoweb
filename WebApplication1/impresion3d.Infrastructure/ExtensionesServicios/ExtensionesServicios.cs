using impresion3d.Core.Interfaces;
using impresion3d.Infrastructure.Contexto;
using impresion3d.Infrastructure.Repositorios;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using impresion3d.Core.Servicios; 

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

            services.AddScoped<IPersonaRepositorio, PersonaRepositorio>();
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<IRolRepositorio, RolRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IUsuarioRolRepositorio, UsuarioRolRepositorio>();

            services.AddScoped<ServicioPersona>();
            services.AddScoped<ServicioCliente>();
            services.AddScoped<ServicioRol>();
            services.AddScoped<ServicioUsuario>();
            services.AddScoped<ServicioUsuarioRol>();

            services.AddScoped<IMaterialRepositorio, MaterialRepositorio>();
            services.AddScoped<ServicioMaterial>();

            services.AddScoped<IProductoRepositorio, ProductoRepositorio>();
            services.AddScoped<ServicioProducto>();

            services.AddScoped<IProductoMaterialRepositorio, ProductoMaterialRepositorio>();
            services.AddScoped<ServicioProductoMaterial>();

            services.AddScoped<IMovimientoMaterialRepositorio, MovimientoMaterialRepositorio>();
            services.AddScoped<ServicioMovimientoMaterial>();

            services.AddScoped<IModelo3DRepositorio, Modelo3DRepositorio>();
            services.AddScoped<ServicioModelo3D>(); 

            return services;
        }
    }
}

