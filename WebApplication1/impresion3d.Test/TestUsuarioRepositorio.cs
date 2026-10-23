using impresion3d.Core.Entidades;
using impresion3d.Infrastructure.Contexto;
using impresion3d.Infrastructure.Repositorios;
using Xunit;

namespace impresion3d.Test
{
    public class TestUsuarioRepositorio
    {
        [Fact]
        public async Task LoginAsync_DeberiaTraerUsuario()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new UsuarioRepositorio(conexion);

            var usuario = await repositorio.LoginAsync(
                "juan.admin",
                "hash_juan_prueba"
            );

            Assert.NotNull(usuario);
            Assert.Equal("juan.admin", usuario.usuario);
        }
        [Fact]
        public async Task CrearAsync_DeberiaCrearUsuario()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var personaRepositorio =
                new PersonaRepositorio(conexion);

            var usuarioRepositorio =
                new UsuarioRepositorio(conexion);

            string dniAleatorio =
                Random.Shared.Next(10000000, 99999999).ToString();

            var nuevaPersona = new Persona
            {
                Dni = dniAleatorio,
                Nombre = "Usuario",
                Apellido = "Prueba",
                Telefono = "3815551234",
                Email = "usuario.prueba@gmail.com",
                Direccion = "Calle Usuario 123",
                Activo = true,
                FechaAlta = DateTime.Now
            };

            await personaRepositorio.CrearAsync(nuevaPersona);

            var personaCreada =
                await personaRepositorio.ObtenerPorDniAsync(dniAleatorio);

            Assert.NotNull(personaCreada);

            string nombreUsuario =
                "usuario" + Random.Shared.Next(1000, 9999);

            var nuevoUsuario = new Usuarios
            {
                id_persona = personaCreada.IdPersona,
                usuario = nombreUsuario,
                password_hash = "hash_prueba_usuario",
                activo = true,
                fecha_creacion = DateTime.Now
            };

            int resultado =
                await usuarioRepositorio.CrearAsync(nuevoUsuario);

            Assert.True(resultado > 0);
        }
        [Fact]
        public async Task CambiarEstadoAsync_DeberiaCambiarEstadoUsuario()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);
            var repositorio = new UsuarioRepositorio(conexion);

            var usuario = await repositorio.LoginAsync(
                "juan.admin",
                "hash_juan_prueba"
            );

            Assert.NotNull(usuario);

            bool resultado = await repositorio.CambiarEstadoAsync(
                usuario.IdUsuario,
                false
            );

            Assert.True(resultado);

            var usuarioDesactivado = await repositorio.LoginAsync(
                "juan.admin",
                "hash_juan_prueba"
            );

            Assert.NotNull(usuarioDesactivado);
            Assert.False(usuarioDesactivado.activo);
        } 
    }
} 