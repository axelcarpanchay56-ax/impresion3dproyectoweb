using impresion3d.Core.Entidades;
using impresion3d.Infrastructure.Contexto;
using impresion3d.Infrastructure.Repositorios;
using Xunit;

namespace impresion3d.Test
{
    public class TestUsuarioRolRepositorio
    {
        [Fact]
        public async Task AsignarAsync_DeberiaAsignarRolAUsuario()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var personaRepositorio = new PersonaRepositorio(conexion);
            var usuarioRepositorio = new UsuarioRepositorio(conexion);
            var usuarioRolRepositorio = new UsuarioRolRepositorio(conexion);

            string identificador = Guid.NewGuid().ToString("N").Substring(0, 12);

            var nuevaPersona = new Persona
            {
                Dni = Random.Shared.Next(10000000, 99999999).ToString(),
                Nombre = "Usuario",
                Apellido = "Prueba Rol",
                Telefono = "3815551234",
                Email = "rol." + identificador + "@gmail.com",
                Direccion = "Calle Prueba 123",
                Activo = true,
                FechaAlta = DateTime.Now
            };

            await personaRepositorio.CrearAsync(nuevaPersona);

            var personaCreada =
                await personaRepositorio.ObtenerPorDniAsync(nuevaPersona.Dni);

            Assert.NotNull(personaCreada);

            string nombreUsuario = "testrol_" + identificador;
            string password = "hash_prueba_rol";

            var nuevoUsuario = new Usuarios
            {
                id_persona = personaCreada.IdPersona,
                usuario = nombreUsuario,
                password_hash = password,
                activo = true,
                fecha_creacion = DateTime.Now
            };

            int resultadoCreacion =
                await usuarioRepositorio.CrearAsync(nuevoUsuario);

            Assert.True(resultadoCreacion > 0);

            var usuarioCreado =
                await usuarioRepositorio.LoginAsync(nombreUsuario, password);

            Assert.NotNull(usuarioCreado);

            var usuarioRol = new Usuario_Rol
            {
                id_usuario = usuarioCreado.IdUsuario,
                id_rol = 2
            };

            int resultado =
                await usuarioRolRepositorio.AsignarAsync(usuarioRol);

            Assert.True(resultado > 0);

            var roles =
                await usuarioRolRepositorio.ObtenerPorUsuarioAsync(usuarioCreado.IdUsuario);

            Assert.Contains(roles, r => r.id_rol == 2);
        } 
        [Fact]
        public async Task ObtenerPorUsuarioAsync_DeberiaTraerRoles()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var usuarioRepositorio =
                new UsuarioRepositorio(conexion);

            var usuarioRolRepositorio =
                new UsuarioRolRepositorio(conexion);

            var usuario = await usuarioRepositorio.LoginAsync(
                "juan.admin",
                "hash_juan_prueba"
            );

            Assert.NotNull(usuario);

            var roles =
                await usuarioRolRepositorio.ObtenerPorUsuarioAsync(
                    usuario.IdUsuario
                );

            Assert.NotNull(roles);
            Assert.True(roles.Count > 0);
        }
    }
}