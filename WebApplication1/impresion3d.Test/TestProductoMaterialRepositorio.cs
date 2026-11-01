using impresion3d.Core.Entidades;
using impresion3d.Infrastructure.Contexto;
using impresion3d.Infrastructure.Repositorios;
using Xunit;

namespace impresion3d.Test
{
    public class TestProductoMaterialRepositorio
    {
        [Fact]
        public async Task ObtenerPorProductoAsync_DeberiaTraerMateriales()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new ProductoMaterialRepositorio(conexion);

            var materiales =
                await repositorio.ObtenerPorProductoAsync(1);

            Assert.NotNull(materiales);
            Assert.True(materiales.Count > 0);
        }
        [Fact]
        public async Task AsignarAsync_DeberiaAsignarMaterialAProducto()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new ProductoMaterialRepositorio(conexion);

            var productoMaterial = new ProductoMaterial
            {
                IdProducto = 1,
                IdMaterial = 2,
                cantidad_estimada = 3
            };

            int resultado =
                await repositorio.AsignarAsync(productoMaterial);

            Assert.True(resultado > 0);
        }
        [Fact]
        public async Task ActualizarAsync_DeberiaActualizarCantidad()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new ProductoMaterialRepositorio(conexion);

            var productoMaterial = new ProductoMaterial
            {
                IdProducto = 1,
                IdMaterial = 2,
                cantidad_estimada = 6
            };

            bool resultado =
                await repositorio.ActualizarAsync(productoMaterial);

            Assert.True(resultado);
        }
        [Fact]
        public async Task EliminarAsync_DeberiaEliminarRelacion()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new ProductoMaterialRepositorio(conexion);

            bool resultado =
                await repositorio.EliminarAsync(1, 2);

            Assert.True(resultado);
        } 
    }
} 
