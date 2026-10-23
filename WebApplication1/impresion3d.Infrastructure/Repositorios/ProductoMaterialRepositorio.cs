using System.Data;
using MySqlConnector;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Infrastructure.Repositorios
{
    public class ProductoMaterialRepositorio : IProductoMaterialRepositorio
    {
        private readonly Imiconexion _conexion;

        public ProductoMaterialRepositorio(Imiconexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<List<ProductoMaterial>> ObtenerPorProductoAsync(
            int idProducto)
        {
            var lista = new List<ProductoMaterial>();

            using var conexion =
                (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_producto_material_obtener_por_producto",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_producto",
                idProducto
            );

            using var reader = await comando.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var productoMaterial = new ProductoMaterial
                {
                    IdProducto = Convert.ToInt32(reader["id_producto"]),
                    IdMaterial = Convert.ToInt32(reader["id_material"]),
                    cantidad_estimada = Convert.ToInt32(reader["cantidad_estimada"])
                };

                lista.Add(productoMaterial);
            }

            return lista; 
        }

        public async Task<int> AsignarAsync(
            ProductoMaterial productoMaterial)
        {
            using var conexion =
        (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_producto_material_asignar",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_producto",
                productoMaterial.IdProducto
            );

            comando.Parameters.AddWithValue(
                "p_id_material",
                productoMaterial.IdMaterial
            );

            comando.Parameters.AddWithValue(
                "p_cantidad_estimada",
                productoMaterial.cantidad_estimada
            );

            int filasAfectadas =
                await comando.ExecuteNonQueryAsync();

            return filasAfectadas; 
        }

        public async Task<bool> ActualizarAsync(
            ProductoMaterial productoMaterial)
        {
            using var conexion =
        (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_producto_material_actualizar",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_producto",
                productoMaterial.IdProducto
            );

            comando.Parameters.AddWithValue(
                "p_id_material",
                productoMaterial.IdMaterial
            );

            comando.Parameters.AddWithValue(
                "p_cantidad_estimada",
                productoMaterial.cantidad_estimada
            );

            int filasAfectadas =
                await comando.ExecuteNonQueryAsync();

            return filasAfectadas > 0; 
        }

        public async Task<bool> EliminarAsync(
            int idProducto,
            int idMaterial)
        {
            using var conexion =
        (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_producto_material_eliminar",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_producto",
                idProducto
            );

            comando.Parameters.AddWithValue(
                "p_id_material",
                idMaterial
            );

            int filasAfectadas =
                await comando.ExecuteNonQueryAsync();

            return filasAfectadas > 0; 
        }
    }
} 
