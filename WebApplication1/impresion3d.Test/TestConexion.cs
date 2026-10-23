using System.Data;
using impresion3d.Infrastructure.Contexto;
using Xunit;

namespace impresion3d.Test
{
    public class TestConexion
    {
        [Fact]
        public async Task TestConexionBD()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            MyConexion conexion = new MyConexion(connectionString);

            using var conn = await conexion.obtenerConexionAsync();

            Assert.NotNull(conn);
            Assert.Equal(ConnectionState.Open, conn.State);
        }
    }
}
