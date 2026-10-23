using impresion3d.Core.Entidades;
using impresion3d.Infrastructure.Contexto;
using impresion3d.Infrastructure.Repositorios;
using Xunit;

namespace impresion3d.Test
{
    public class TestProductoRepositorio
    {
        [Fact]
        public async Task ObtenerTodosAsync_DeberiaTraerProductos()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new ProductoRepositorio(conexion);

            var productos =
                await repositorio.ObtenerTodosAsync();

            Assert.NotNull(productos);
            Assert.True(productos.Count > 0);
        }
        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaTraerProducto()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new ProductoRepositorio(conexion);

            var producto =
                await repositorio.ObtenerPorIdAsync(1);

            Assert.NotNull(producto);
            Assert.Equal(1, producto.IdProducto);
        }
        [Fact]
        public async Task CrearAsync_DeberiaCrearProducto()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new ProductoRepositorio(conexion);

            var nuevoProducto = new Productos
            {
                id_modelo3d = 0,
                nombre = "Soporte para Celular",
                descripcion = "Soporte impreso en 3D",
                precio_venta = 12000.00m,
                activo = true,
                fecha_alta = DateTime.Now
            };

            int resultado =
                await repositorio.CrearAsync(nuevoProducto);

            Assert.True(resultado > 0);
        }
        [Fact]
        public async Task ActualizarAsync_DeberiaActualizarProducto()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new ProductoRepositorio(conexion);

            var productos =
                await repositorio.ObtenerTodosAsync();

            var producto = productos
                .FirstOrDefault(p => p.nombre == "Soporte para Celular");

            Assert.NotNull(producto);

            producto.nombre = "Soporte para Celular Premium";
            producto.descripcion = "Soporte impreso en 3D actualizado";
            producto.precio_venta = 13500.00m;

            bool resultado =
                await repositorio.ActualizarAsync(producto);

            Assert.True(resultado);

            var productoActualizado =
                await repositorio.ObtenerPorIdAsync(
                    producto.IdProducto
                );

            Assert.NotNull(productoActualizado);

            Assert.Equal(
                "Soporte para Celular Premium",
                productoActualizado.nombre
            );

            Assert.Equal(
                13500.00m,
                productoActualizado.precio_venta
            );
        }
        [Fact]
        public async Task CambiarEstadoAsync_DeberiaCambiarEstadoProducto()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new ProductoRepositorio(conexion);

            var productos =
                await repositorio.ObtenerTodosAsync();

            var producto = productos
                .FirstOrDefault(p => p.nombre == "Soporte para Celular Premium");

            Assert.NotNull(producto);

            bool resultado =
                await repositorio.CambiarEstadoAsync(
                    producto.IdProducto,
                    false
                );

            Assert.True(resultado);

            var productoActualizado =
                await repositorio.ObtenerPorIdAsync(
                    producto.IdProducto
                );

            Assert.NotNull(productoActualizado);

            Assert.False(productoActualizado.activo);
        } 
    }
} 
