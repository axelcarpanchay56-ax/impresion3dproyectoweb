using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using impresion3d.Infrastructure.Contexto;
using MySqlConnector;
using System.Data;
using System.Net.NetworkInformation;

namespace impresion3d.Infrastructure.Repositorios
{
    public class MovimientoMaterialRepositorio : IMovimientoMaterialRepositorio
    {
        private readonly Imiconexion _conexion;

        public MovimientoMaterialRepositorio(Imiconexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<List<MovimientoMaterial>> ObtenerTodosAsync()
        {
            var lista = new List<MovimientoMaterial>();

            using var conexion =
                (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_movimiento_material_obtener_todos",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            using var reader = await comando.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var movimiento = new MovimientoMaterial
                {
                    IdMovimientoMaterial =
        Convert.ToInt32(reader["id_movimiento_material"]),

                    id_material =
        Convert.ToInt32(reader["id_material"]),

                    tipo_movimiento =
        reader["tipo_movimiento"].ToString(),

                    cantidad =
        Convert.ToDecimal(reader["cantidad"]),

                    fecha_hora =
        Convert.ToDateTime(reader["fecha_actualizacion"]),

                    observacion =
        reader["observacion"] == DBNull.Value
            ? null
            : reader["observacion"].ToString() 
                };

                lista.Add(movimiento); 
            }

            return lista; 
        }

        public async Task<MovimientoMaterial?> ObtenerPorIdAsync(int id)
        {
            using var conexion =
        (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_movimiento_material_obtener_por_id",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_movimiento_material",
                id
            );

            using var reader = await comando.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new MovimientoMaterial
                {
                    IdMovimientoMaterial =
                        Convert.ToInt32(reader["id_movimiento_material"]),

                    id_material =
                        Convert.ToInt32(reader["id_material"]),

                    tipo_movimiento =
                        reader["tipo_movimiento"].ToString(),

                    cantidad =
                        Convert.ToDecimal(reader["cantidad"]),

                    fecha_hora =
                        Convert.ToDateTime(reader["fecha_actualizacion"]),

                    observacion =
                        reader["observacion"] == DBNull.Value
                            ? null
                            : reader["observacion"].ToString()
                };
            }

            return null;
        } 

        public async Task<int> CrearAsync(MovimientoMaterial movimientoMaterial)
        {
            using var conexion =
        (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_movimiento_material_crear",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_material",
                movimientoMaterial.id_material
            );

            comando.Parameters.AddWithValue(
                "p_tipo_movimiento",
                movimientoMaterial.tipo_movimiento
            );

            comando.Parameters.AddWithValue(
                "p_cantidad",
                movimientoMaterial.cantidad
            );

            comando.Parameters.AddWithValue(
                "p_fecha_actualizacion",
                movimientoMaterial.fecha_hora
            );

            comando.Parameters.AddWithValue(
                "p_observacion",
                movimientoMaterial.observacion
            );

            object? resultado = await comando.ExecuteScalarAsync();

            return Convert.ToInt32(resultado); 
        }

        public async Task<bool> ActualizarAsync(MovimientoMaterial movimientoMaterial)
        {
            using var conexion =
        (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_movimiento_material_actualizar",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_movimiento_material",
                movimientoMaterial.IdMovimientoMaterial
            );

            comando.Parameters.AddWithValue(
                "p_id_material",
                movimientoMaterial.id_material
            );

            comando.Parameters.AddWithValue(
                "p_tipo_movimiento",
                movimientoMaterial.tipo_movimiento
            );

            comando.Parameters.AddWithValue(
                "p_cantidad",
                movimientoMaterial.cantidad
            );

            comando.Parameters.AddWithValue(
                "p_fecha_actualizacion",
                movimientoMaterial.fecha_hora
            );

            comando.Parameters.AddWithValue(
                "p_observacion",
                movimientoMaterial.observacion
            );

            int filasAfectadas =
                await comando.ExecuteNonQueryAsync();

            return filasAfectadas > 0; 
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var conexion =
       (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_movimiento_material_eliminar",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_movimiento_material",
                id
            );

            int filasAfectadas =
                await comando.ExecuteNonQueryAsync();

            return filasAfectadas > 0; 
        }
    }
} 
