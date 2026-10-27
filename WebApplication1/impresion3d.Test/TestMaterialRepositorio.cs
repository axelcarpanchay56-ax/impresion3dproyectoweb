using impresion3d.Core.Entidades;
using impresion3d.Infrastructure.Contexto;
using impresion3d.Infrastructure.Repositorios;
using Xunit;

namespace impresion3d.Test
{
    public class TestMaterialRepositorio
    {
        [Fact]
        public async Task ObtenerTodosAsync_DeberiaTraerMateriales()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new MaterialRepositorio(conexion);

            var materiales =
                await repositorio.ObtenerTodosAsync();

            Assert.NotNull(materiales);
            Assert.True(materiales.Count > 0);
        }
        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaTraerMaterial()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new MaterialRepositorio(conexion);

            var material =
                await repositorio.ObtenerPorIdAsync(1);

            Assert.NotNull(material);
            Assert.Equal(1, material.IdMaterial);
        }
        [Fact]
        public async Task CrearAsync_DeberiaCrearMaterial()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new MaterialRepositorio(conexion);

            var nuevoMaterial = new Materiales
            {
                nombre = "PETG Azul",
                tipo = "Filamento",
                unidad_medida = "kg",
                color = "Azul",
                diametro = 1.75m,
                activo = true
            };

            int resultado =
                await repositorio.CrearAsync(nuevoMaterial);

            Assert.True(resultado > 0);
        }
        [Fact]
        public async Task ActualizarAsync_DeberiaActualizarMaterial()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new MaterialRepositorio(conexion);

            var materiales =
                await repositorio.ObtenerTodosAsync();

            var material = materiales
                .FirstOrDefault(m => m.nombre == "PETG Azul");

            Assert.NotNull(material);

            material.nombre = "PETG Azul Premium";
            material.color = "Azul Oscuro";

            bool resultado =
                await repositorio.ActualizarAsync(material);

            Assert.True(resultado);

            var materialActualizado =
                await repositorio.ObtenerPorIdAsync(
                    material.IdMaterial
                );

            Assert.NotNull(materialActualizado);

            Assert.Equal(
                "PETG Azul Premium",
                materialActualizado.nombre
            );

            Assert.Equal(
                "Azul Oscuro",
                materialActualizado.color
            );
        }
        [Fact]
        public async Task CambiarEstadoAsync_DeberiaCambiarEstadoMaterial()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var repositorio =
                new MaterialRepositorio(conexion);

            var materiales =
                await repositorio.ObtenerTodosAsync();

            var material = materiales
                .FirstOrDefault(m => m.nombre == "PETG Azul Premium");

            Assert.NotNull(material);

            bool resultado =
                await repositorio.CambiarEstadoAsync(
                    material.IdMaterial,
                    false
                );

            Assert.True(resultado);

            var materialActualizado =
                await repositorio.ObtenerPorIdAsync(
                    material.IdMaterial
                );

            Assert.NotNull(materialActualizado);

            Assert.False(materialActualizado.activo);
        } 
    }
} 