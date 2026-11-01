using impresion3d.Core.Entidades;
using impresion3d.Infrastructure.Contexto;
using impresion3d.Infrastructure.Repositorios;
using Xunit;

namespace impresion3d.Test
{
    public class TestModelo3DRepositorio
    {
        [Fact]
        public async Task ObtenerTodosAsync_DeberiaTraerModelos()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new Modelo3DRepositorio(conexion);

            var modelos =
                await repositorio.ObtenerTodosAsync();

            Assert.NotNull(modelos);
        }
        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaTraerModelo()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new Modelo3DRepositorio(conexion);

            var modelo =
                await repositorio.ObtenerPorIdAsync(2);

            Assert.NotNull(modelo);
            Assert.Equal(2, modelo.IdModelo3d);
        }
        [Fact]
        public async Task CrearAsync_DeberiaCrearModelo()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new Modelo3DRepositorio(conexion);

            var modelo = new Modelo3D
            {
                nombre = "Maceta Geometrica",
                archivo = "maceta_geometrica.stl",
                formato = "STL",
                descripcion = "Modelo creado desde test",
                fecha_creacion = new DateTime(2026, 10, 25)
            };

            int idGenerado =
                await repositorio.CrearAsync(modelo);

            Assert.True(idGenerado > 0);
        }
        [Fact]
        public async Task ActualizarAsync_DeberiaActualizarModelo()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new Modelo3DRepositorio(conexion);

            var modelo = new Modelo3D
            {
                IdModelo3d = 5,
                nombre = "Maceta Geometrica Premium",
                archivo = "maceta_geometrica_premium.stl",
                formato = "STL",
                descripcion = "Modelo actualizado desde test",
                fecha_creacion = new DateTime(2026, 10, 25)
            };

            bool resultado =
                await repositorio.ActualizarAsync(modelo);

            Assert.True(resultado);
        }
        [Fact]
        public async Task EliminarAsync_DeberiaEliminarModelo()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio = new Modelo3DRepositorio(conexion);

            var modelo = new Modelo3D
            {
                nombre = "Modelo para eliminar",
                archivo = "prueba_eliminar.stl",
                formato = "STL",
                descripcion = "Modelo temporal de prueba",
                fecha_creacion = new DateTime(2026, 10, 25)
            };

            int idGenerado = await repositorio.CrearAsync(modelo);

            Assert.True(idGenerado > 0);

            bool resultado = await repositorio.EliminarAsync(idGenerado);

            Assert.True(resultado);
        }
    } 
} 
