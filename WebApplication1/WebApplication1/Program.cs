using impresion3d.Infrastructure.ExtensionesServicios;
using FluentValidation;
using impresion3d.Api.DTOs;
using impresion3d.Api.Validadores;
using impresion3d.Api.AutoMapper;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using impresion3d.Core.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Dapper;

namespace WebApplication1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Servicios
            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("PermitirFrontend", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            builder.Services.AddInfrastructure(
                builder.Configuration
            );

            // Validadores Módulo 1
            builder.Services.AddScoped<
                IValidator<CrearPersonaDto>,
                PersonaCrearValidator
            >();

            builder.Services.AddScoped<
                IValidator<CrearClienteDto>,
                ClienteCrearValidator
            >();

            builder.Services.AddScoped<
                IValidator<CrearUsuarioDto>,
                UsuarioCrearValidator
            >();

            builder.Services.AddScoped<
                IValidator<RegistroUsuarioDto>,
                UsuarioRegistroValidator
            >();

            builder.Services.AddScoped<
                IValidator<AsignarUsuarioRolDto>,
                UsuarioRolAsignarValidator
            >();

            // Validadores Módulo 2
            builder.Services.AddScoped<
                IValidator<CrearProductoDto>,
                ProductoCrearValidator
            >();

            builder.Services.AddScoped<
                IValidator<AsignarProductoMaterialDto>,
                ProductoMaterialAsignarValidator
            >();

            builder.Services.AddScoped<
                IValidator<CrearMovimientoMaterialDto>,
                MovimientoMaterialCrearValidator
            >();

            // Validadores Módulo 3
            builder.Services.AddScoped<
                IValidator<Modelo3DCrearDto>,
                Modelo3DCrearValidator
            >();

            builder.Services.AddScoped<
                IValidator<MaterialesCrearDto>,
                MaterialesCrearValidator
            >();

            builder.Services.AddScoped<
                IValidator<ImpresoraCrearDto>,
                ImpresoraCrearValidator
            >();

            builder.Services.AddScoped<
                IValidator<ImpresoraMaterialCrearDto>,
                ImpresoraMaterialCrearValidator
            >();

            builder.Services.AddScoped<
                IValidator<EstadoImpresionCrearDto>,
                EstadoImpresionCrearValidator
            >();

            builder.Services.AddScoped<
                IValidator<TrabajoImpresionCrearDto>,
                TrabajoImpresionCrearValidator
            >();

            builder.Services.AddScoped<
                IValidator<ConsumoMaterialCrearDto>,
                ConsumoMaterialCrearValidator
            >();

            // Validadores Módulo 4
            builder.Services.AddScoped<
                IValidator<PedidosCrearDto>,
                PedidosCrearValidator
            >();

            builder.Services.AddScoped<
                IValidator<DetallePedidoCrearDto>,
                DetallePedidoCrearValidator
            >();

            builder.Services.AddScoped<
                IValidator<EstadoPedidoCrearDto>,
                EstadoPedidoCrearValidator
            >();

            builder.Services.AddAutoMapper(
                typeof(MappeoDto)
            );

            // Servicio de autenticación
            builder.Services.AddScoped<
                IAutenticacionService,
                AutenticacionService
            >();

            // JWT
            var llaveSecreta =
                builder.Configuration[
                    "Jwt:llave_secreta"
                ];

            builder.Services
                .AddAuthentication(
                    JwtBearerDefaults.AuthenticationScheme
                )
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer =
                                builder.Configuration[
                                    "Jwt:emisor"
                                ],

                            ValidAudience =
                                builder.Configuration[
                                    "Jwt:audiencia"
                                ],

                            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(
                                        llaveSecreta!
                                    )
                                )
                        };
                });

            builder.Services.AddAuthorization();

            var app = builder.Build();

            await InicializarAdminAsync(app);

            app.UseCors("PermitirFrontend");

            var rutaFrontendRepo = Path.GetFullPath(
                Path.Combine(
                    app.Environment.ContentRootPath,
                    "..",
                    "..",
                    "paginaImpresiones3D"
                )
            );

            var rutaFrontendHome = Path.GetFullPath(
                Path.Combine(
                    app.Environment.ContentRootPath,
                    "..",
                    "..",
                    "..",
                    "paginaImpresiones3D"
                )
            );

            var rutaFrontend = Directory.Exists(rutaFrontendHome)
                ? rutaFrontendHome
                : rutaFrontendRepo;

            if (Directory.Exists(rutaFrontend))
            {
                app.UseFileServer(new FileServerOptions
                {
                    FileProvider = new PhysicalFileProvider(rutaFrontend),
                    EnableDefaultFiles = true
                });
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
        }

        private static async Task InicializarAdminAsync(WebApplication app)
        {
            try
            {
                using var scope = app.Services.CreateScope();
                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var miConexion = scope.ServiceProvider.GetRequiredService<IMiConexion>();

                var adminUser = config["AdminConfig:Usuario"] ?? "admin";
                var adminPassHashConfig = config["AdminConfig:PasswordHash"];
                var adminPassPlain = config["AdminConfig:Password"];
                var adminRol = config["AdminConfig:Rol"] ?? "Administrador";

                using var conexion = await miConexion.ObtenerConexionAsync();

                var idRol = await conexion.ExecuteScalarAsync<int?>(
                    "SELECT id_rol FROM roles WHERE nombre = @Nombre LIMIT 1;",
                    new { Nombre = adminRol }
                );

                if (idRol == null || idRol == 0)
                {
                    await conexion.ExecuteAsync(
                        "INSERT INTO roles (nombre, descripcion) VALUES (@Nombre, 'Rol de administración total del sistema');",
                        new { Nombre = adminRol }
                    );

                    idRol = await conexion.ExecuteScalarAsync<int>(
                        "SELECT id_rol FROM roles WHERE nombre = @Nombre LIMIT 1;",
                        new { Nombre = adminRol }
                    );
                }

                await conexion.ExecuteAsync(
                    "INSERT IGNORE INTO roles (nombre, descripcion) VALUES ('Cliente', 'Cliente registrado en la tienda web');"
                );

                var idPersona = await conexion.ExecuteScalarAsync<int?>(
                    "SELECT id_persona FROM personas WHERE dni = '00000001' LIMIT 1;"
                );

                if (idPersona == null || idPersona == 0)
                {
                    await conexion.ExecuteAsync(
                        @"INSERT INTO personas (dni, nombre, apellido, telefono, email, direccion, activo, fecha_alta)
                          VALUES ('00000001', 'Administrador', 'Innova 3D', '+54 3838 40-1302', 'admin@innova3d.com', 'Sede Central', 1, CURDATE());"
                    );

                    idPersona = await conexion.ExecuteScalarAsync<int>(
                        "SELECT id_persona FROM personas WHERE dni = '00000001' LIMIT 1;"
                    );
                }

                var idUsuario = await conexion.ExecuteScalarAsync<int?>(
                    "SELECT id_usuario FROM usuarios WHERE usuario = @Usuario LIMIT 1;",
                    new { Usuario = adminUser }
                );

                if (idUsuario == null || idUsuario == 0)
                {
                    var hash = !string.IsNullOrWhiteSpace(adminPassHashConfig)
                        ? adminPassHashConfig
                        : (!string.IsNullOrWhiteSpace(adminPassPlain)
                            ? ServicioPassword.HashPassword(adminPassPlain)
                            : "$2a$11$K.Aa88yJ5aW/c7ld5yYlUOd1oSzaxMjVttj3HDiBSBm6lFTi98HpK");

                    await conexion.ExecuteAsync(
                        @"INSERT INTO usuarios (id_persona, usuario, password_hash, activo, fecha_creacion, ultimo_acceso)
                          VALUES (@IdPersona, @Usuario, @PasswordHash, 1, CURDATE(), NOW());",
                        new
                        {
                            IdPersona = idPersona,
                            Usuario = adminUser,
                            PasswordHash = hash
                        }
                    );

                    idUsuario = await conexion.ExecuteScalarAsync<int>(
                        "SELECT id_usuario FROM usuarios WHERE usuario = @Usuario LIMIT 1;",
                        new { Usuario = adminUser }
                    );
                }

                await conexion.ExecuteAsync(
                    "INSERT IGNORE INTO usuario_rol (id_usuario, id_rol) VALUES (@IdUsuario, @IdRol);",
                    new { IdUsuario = idUsuario, IdRol = idRol }
                );

                await conexion.ExecuteAsync(
                    "INSERT IGNORE INTO clientes (id_persona) VALUES (@IdPersona);",
                    new { IdPersona = idPersona }
                );

                await conexion.ExecuteAsync(
                    "INSERT IGNORE INTO estado_pedido (id_estado_pedido, nombre) VALUES (1, 'Pendiente');"
                );
            }
            catch
            {
                // Si la base de datos no está disponible en el arranque, continúa normalmente.
            }
        }
    }
}
