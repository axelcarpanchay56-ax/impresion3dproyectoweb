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

            var usuarioRepositorio =
                new UsuarioRepositorio(conexion);

            var usuarioRolRepositorio =
                new UsuarioRolRepositorio(conexion);

            var usuario = await usuarioRepositorio.LoginAsync(
                "juan.admin",
                "hash_juan_prueba"
            );

            Assert.NotNull(usuario);

            var usuarioRol = new Usuario_Rol
            {
                id_usuario = usuario.IdUsuario,
                id_rol = 2
            };

            int resultado =
                await usuarioRolRepositorio.AsignarAsync(usuarioRol);

            Assert.True(resultado > 0);
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