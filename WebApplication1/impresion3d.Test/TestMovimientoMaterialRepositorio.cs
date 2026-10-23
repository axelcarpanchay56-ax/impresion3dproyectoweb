using impresion3d.Core.Entidades;
using impresion3d.Infrastructure.Contexto;
using impresion3d.Infrastructure.Repositorios;
using Xunit;

namespace impresion3d.Test
{
    public class TestMovimientoMaterialRepositorio
    {
        [Fact]
        public async Task ObtenerTodosAsync_DeberiaTraerMovimientos()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new MovimientoMaterialRepositorio(conexion);

            var movimientos =
                await repositorio.ObtenerTodosAsync();

            Assert.NotNull(movimientos);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaTraerMovimiento()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new MovimientoMaterialRepositorio(conexion);

            var movimiento =
                await repositorio.ObtenerPorIdAsync(2);

            Assert.NotNull(movimiento);
            Assert.Equal(2, movimiento.IdMovimientoMaterial);
        }
        [Fact]
        public async Task CrearAsync_DeberiaCrearMovimiento()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new MovimientoMaterialRepositorio(conexion);

            var movimiento = new MovimientoMaterial
            {
                id_material = 1,
                tipo_movimiento = "Salida",
                cantidad = 1.25m,
                fecha_hora = new DateTime(2026, 10, 22),
                observacion = "Movimiento creado desde test"
            };

            int idGenerado =
                await repositorio.CrearAsync(movimiento);

            Assert.True(idGenerado > 0);
        }
        [Fact]
        public async Task ActualizarAsync_DeberiaActualizarMovimiento()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new MovimientoMaterialRepositorio(conexion);

            var movimiento = new MovimientoMaterial
            {
                IdMovimientoMaterial = 3,
                id_material = 1,
                tipo_movimiento = "Salida",
                cantidad = 2.00m,
                fecha_hora = new DateTime(2026, 10, 22),
                observacion = "Movimiento actualizado desde test"
            };

            bool resultado =
                await repositorio.ActualizarAsync(movimiento);

            Assert.True(resultado);
        }
        [Fact]
        public async Task EliminarAsync_DeberiaEliminarMovimiento()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new MovimientoMaterialRepositorio(conexion);

            bool resultado =
                await repositorio.EliminarAsync(3);

            Assert.True(resultado);
        } 
    }
} 