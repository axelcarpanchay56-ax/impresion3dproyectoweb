using impresion3d.Infrastructure.ExtensionesServicios;
using FluentValidation;
using impresion3d.Api.DTOs;
using impresion3d.Api.Validadores;
using impresion3d.Api.AutoMapper;
using impresion3d.Core.Interfaces;
using impresion3d.Core.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Servicios
            builder.Services.AddControllers();

            builder.Services.AddInfrastructure(
                builder.Configuration
            );

            builder.Services.AddScoped<
                IValidator<CrearPersonaDto>,
                PersonaCrearValidator
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

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
} 