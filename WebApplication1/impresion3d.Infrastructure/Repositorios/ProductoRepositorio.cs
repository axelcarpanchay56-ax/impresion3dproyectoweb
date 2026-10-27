using System.Data;
using MySqlConnector;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Infrastructure.Repositorios
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly Imiconexion _conexion;

        public ProductoRepositorio(Imiconexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<List<Productos>> ObtenerTodosAsync()
        {
            var productos = new List<Productos>();

            using var conexion =
                (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_productos_obtener_todos",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            using var reader = await comando.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var producto = new Productos
                {
                    IdProducto = Convert.ToInt32(reader["id_producto"]),

                    id_modelo3d = reader["id_modelo3d"] == DBNull.Value
                        ? 0
                        : Convert.ToInt32(reader["id_modelo3d"]),

                    nombre = reader["nombre"].ToString(),

                    descripcion = reader["descripcion"] == DBNull.Value
                        ? null
                        : reader["descripcion"].ToString(),

                    precio_venta = Convert.ToDecimal(reader["precio_venta"]),

                    activo = Convert.ToBoolean(reader["activo"]),

                    fecha_alta = Convert.ToDateTime(reader["fecha_alta"])
                };

                productos.Add(producto);
            }

            return productos; 
        }

        public async Task<Productos?> ObtenerPorIdAsync(int id)
        {
            using var conexion =
       (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_productos_obtener_por_id",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_producto",
                id
            );

            using var reader = await comando.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var producto = new Productos
                {
                    IdProducto = Convert.ToInt32(reader["id_producto"]),

                    id_modelo3d = reader["id_modelo3d"] == DBNull.Value
                        ? 0
                        : Convert.ToInt32(reader["id_modelo3d"]),

                    nombre = reader["nombre"].ToString(),

                    descripcion = reader["descripcion"] == DBNull.Value
                        ? null
                        : reader["descripcion"].ToString(),

                    precio_venta = Convert.ToDecimal(reader["precio_venta"]),

                    activo = Convert.ToBoolean(reader["activo"]),

                    fecha_alta = Convert.ToDateTime(reader["fecha_alta"])
                };

                return producto;
            }

            return null; 
        }

        public async Task<int> CrearAsync(Productos producto)
        {
            using var conexion =
        (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_productos_crear",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_modelo3d",
                producto.id_modelo3d == 0
                    ? DBNull.Value
                    : producto.id_modelo3d
            );

            comando.Parameters.AddWithValue(
                "p_nombre",
                producto.nombre
            );

            comando.Parameters.AddWithValue(
                "p_descripcion",
                producto.descripcion
            );

            comando.Parameters.AddWithValue(
                "p_precio_venta",
                producto.precio_venta
            );

            comando.Parameters.AddWithValue(
                "p_activo",
                producto.activo
            );

            comando.Parameters.AddWithValue(
                "p_fecha_alta",
                producto.fecha_alta
            );

            int filasAfectadas =
                await comando.ExecuteNonQueryAsync();

            return filasAfectadas; 
        }

        public async Task<bool> ActualizarAsync(Productos producto)
        {
            using var conexion =
        (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_productos_actualizar",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_producto",
                producto.IdProducto
            );

            comando.Parameters.AddWithValue(
                "p_id_modelo3d",
                producto.id_modelo3d == 0
                    ? DBNull.Value
                    : producto.id_modelo3d
            );

            comando.Parameters.AddWithValue(
                "p_nombre",
                producto.nombre
            );

            comando.Parameters.AddWithValue(
                "p_descripcion",
                producto.descripcion
            );

            comando.Parameters.AddWithValue(
                "p_precio_venta",
                producto.precio_venta
            );

            comando.Parameters.AddWithValue(
                "p_activo",
                producto.activo
            );

            comando.Parameters.AddWithValue(
                "p_fecha_alta",
                producto.fecha_alta
            );

            int filasAfectadas =
                await comando.ExecuteNonQueryAsync();

            return filasAfectadas > 0; 
        }

        public async Task<bool> CambiarEstadoAsync(int id, bool activo)
        {
            using var conexion =
        (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_productos_cambiar_estado",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_producto",
                id
            );

            comando.Parameters.AddWithValue(
                "p_activo",
                activo
            );

            int filasAfectadas =
                await comando.ExecuteNonQueryAsync();

            return filasAfectadas > 0; 
        }
    }
} 
