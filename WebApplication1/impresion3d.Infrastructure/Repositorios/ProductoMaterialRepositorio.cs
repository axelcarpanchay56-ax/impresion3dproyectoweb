using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using System.Data;
using Dapper;

namespace impresion3d.Infrastructure.Repositorios
{
    public class ProductoMaterialRepositorio : IProductoMaterialRepositorio
    {
        private readonly IMiConexion _conexion;

        public ProductoMaterialRepositorio(IMiConexion conexion)
        {
            _conexion = conexion;

            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<List<ProductoMaterial>> ObtenerPorProductoAsync(
            int idProducto)
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add(
                "p_id_producto",
                idProducto,
                DbType.Int32,
                ParameterDirection.Input
            );

            var lista =
                await conexion.QueryAsync<ProductoMaterial>(
                    "sp_producto_material_obtener_por_producto",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return lista.ToList();
        }

        public async Task<int> AsignarAsync(
            ProductoMaterial productoMaterial)
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add("p_id_producto", productoMaterial.IdProducto);
            parametros.Add("p_id_material", productoMaterial.IdMaterial);
            parametros.Add("p_cantidad_estimada", productoMaterial.cantidad_estimada);

            int filasAfectadas =
                await conexion.ExecuteAsync(
                    "sp_producto_material_asignar",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return filasAfectadas;
        }

        public async Task<bool> ActualizarAsync(
            ProductoMaterial productoMaterial)
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add("p_id_producto", productoMaterial.IdProducto);
            parametros.Add("p_id_material", productoMaterial.IdMaterial);
            parametros.Add("p_cantidad_estimada", productoMaterial.cantidad_estimada);

            int filasAfectadas =
                await conexion.ExecuteAsync(
                    "sp_producto_material_actualizar",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(
            int idProducto,
            int idMaterial)
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add("p_id_producto", idProducto);
            parametros.Add("p_id_material", idMaterial);

            int filasAfectadas =
                await conexion.ExecuteAsync(
                    "sp_producto_material_eliminar",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return filasAfectadas > 0;
        }
    }
}
