using impresion3d.Infrastructure.Contexto;
using impresion3d.Infrastructure.Repositorios;
using Xunit;

namespace impresion3d.Test
{
    public class TestRolRepositorio
    {
        [Fact]
        public async Task ObtenerTodosAsync_DeberiaTraerRoles()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio = new RolRepositorio(conexion);

            var roles = await repositorio.ObtenerTodosAsync();

            Assert.NotNull(roles);
            Assert.True(roles.Count > 0);
        }
    }
}
