using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using System.Data;
using Dapper;

namespace impresion3d.Infrastructure.Repositorios
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly IMiConexion _conexion;

        public ProductoRepositorio(IMiConexion conexion)
        {
            _conexion = conexion;

            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<List<Productos>> ObtenerTodosAsync()
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var productos =
                await conexion.QueryAsync<Productos>(
                    "sp_productos_obtener_todos",
                    commandType: CommandType.StoredProcedure
                );

            return productos.ToList();
        }

        public async Task<Productos?> ObtenerPorIdAsync(int id)
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add(
                "p_id_producto",
                id,
                DbType.Int32,
                ParameterDirection.Input
            );

            var producto =
                await conexion.QueryFirstOrDefaultAsync<Productos>(
                    "sp_productos_obtener_por_id",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return producto;
        }

        public async Task<int> CrearAsync(Productos producto)
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add(
                "p_id_modelo3d",
                producto.id_modelo3d == 0
                    ? null
                    : producto.id_modelo3d,
                DbType.Int32
            );
            parametros.Add("p_nombre", producto.nombre);
            parametros.Add("p_descripcion", producto.descripcion);
            parametros.Add("p_precio_venta", producto.precio_venta);
            parametros.Add("p_activo", producto.activo);
            parametros.Add("p_fecha_alta", producto.fecha_alta);

            int filasAfectadas =
                await conexion.ExecuteAsync(
                    "sp_productos_crear",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return filasAfectadas;
        }

        public async Task<bool> ActualizarAsync(Productos producto)
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add("p_id_producto", producto.IdProducto);
            parametros.Add(
                "p_id_modelo3d",
                producto.id_modelo3d == 0
                    ? null
                    : producto.id_modelo3d,
                DbType.Int32
            );
            parametros.Add("p_nombre", producto.nombre);
            parametros.Add("p_descripcion", producto.descripcion);
            parametros.Add("p_precio_venta", producto.precio_venta);
            parametros.Add("p_activo", producto.activo);
            parametros.Add("p_fecha_alta", producto.fecha_alta);

            int filasAfectadas =
                await conexion.ExecuteAsync(
                    "sp_productos_actualizar",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return filasAfectadas > 0;
        }

        public async Task<bool> CambiarEstadoAsync(int id, bool activo)
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add("p_id_producto", id);
            parametros.Add("p_activo", activo);

            int filasAfectadas =
                await conexion.ExecuteAsync(
                    "sp_productos_cambiar_estado",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return filasAfectadas > 0;
        }
    }
}
