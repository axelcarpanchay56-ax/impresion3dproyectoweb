using impresion3d.Core.Entidades;
using impresion3d.Infrastructure.Contexto;
using impresion3d.Infrastructure.Repositorios;
using Xunit;

namespace impresion3d.Test
{
    public class TestPersonaRepositorio
    {
        [Fact]
        public async Task ObtenerTodasAsync_DeberiaTraerPersonas()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio = new PersonaRepositorio(conexion);

            var personas = await repositorio.ObtenerTodasAsync();

            Assert.NotNull(personas);
            Assert.True(personas.Count > 0);
        }
        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaTraerPersona()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio = new PersonaRepositorio(conexion);

            var persona = await repositorio.ObtenerPorIdAsync(1);

            Assert.NotNull(persona);
            Assert.Equal(1, persona.IdPersona);
        }
        [Fact]
        public async Task ObtenerPorDniAsync_DeberiaTraerPersona()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio = new PersonaRepositorio(conexion);

            var persona = await repositorio.ObtenerPorDniAsync("30111222");

            Assert.NotNull(persona);
            Assert.Equal("30111222", persona.Dni);
        }
        [Fact]
        public async Task CrearAsync_DeberiaCrearPersona()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio = new PersonaRepositorio(conexion);

            var nuevaPersona = new Persona
            {
                Dni = "40123456",
                Nombre = "Pedro",
                Apellido = "Ramirez",
                Telefono = "3815554444",
                Email = "pedro@gmail.com",
                Direccion = "Rivadavia 123",
                Activo = true,
                FechaAlta = DateTime.Now
            };

            int resultado = await repositorio.CrearAsync(nuevaPersona);

            Assert.True(resultado > 0);
        }
        [Fact]
        public async Task ActualizarAsync_DeberiaActualizarPersona()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio = new PersonaRepositorio(conexion);

            var persona = await repositorio.ObtenerPorDniAsync("40123456");

            Assert.NotNull(persona);

            persona.Nombre = "Pedro Actualizado";
            persona.Telefono = "3815559999";
            persona.Direccion = "Rivadavia 999";

            bool resultado = await repositorio.ActualizarAsync(persona);

            Assert.True(resultado);

            var personaActualizada =
                await repositorio.ObtenerPorIdAsync(persona.IdPersona);

            Assert.NotNull(personaActualizada);

            Assert.Equal(
                "Pedro Actualizado",
                personaActualizada.Nombre
            );

            Assert.Equal(
                "3815559999",
                personaActualizada.Telefono
            );

            Assert.Equal(
                "Rivadavia 999",
                personaActualizada.Direccion
            );
        }
        [Fact]
        public async Task CambiarEstadoAsync_DeberiaCambiarEstadoPersona()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio = new PersonaRepositorio(conexion);

            var persona = await repositorio.ObtenerPorDniAsync("40123456");

            Assert.NotNull(persona);

            bool resultado =
                await repositorio.CambiarEstadoAsync(
                    persona.IdPersona,
                    false
                );

            Assert.True(resultado);

            var personaActualizada =
                await repositorio.ObtenerPorIdAsync(persona.IdPersona);

            Assert.NotNull(personaActualizada);

            Assert.False(personaActualizada.Activo);
        } 
    }
}
