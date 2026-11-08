using System.Data;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using impresion3d.Infrastructure.Contexto;
using MySqlConnector;

namespace impresion3d.Infrastructure.Repositorios
{
    public class TrabajoImpresionRepositorio : ITrabajoImpresionRepositorio
    {
        private readonly IMiConexion _conexion;

        public TrabajoImpresionRepositorio(IMiConexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<List<TrabajoImpresion>> ObtenerTodosAsync()
        {
            var lista = new List<TrabajoImpresion>();
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_trabajo_impresion_obtener_todos";

                using var reader = await (cmd as MySqlCommand)!.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    lista.Add(new TrabajoImpresion
                    {
                        id_trabajo_impresion = reader.GetInt32("id_trabajo_impresion"),
                        id_pedido = reader.GetInt32("id_pedido"),
                        id_producto = reader.GetInt32("id_producto"),
                        id_impresora = reader.GetInt32("id_impresora"),
                        id_modelo = reader.GetInt32("id_modelo"),
                        id_material = reader.GetInt32("id_material"),
                        cantidad = reader.GetInt32("cantidad"),
                        id_estado = reader.GetInt32("id_estado"),
                        fecha_inicio = reader.IsDBNull(reader.GetOrdinal("fecha_inicio")) ? default : reader.GetDateTime("fecha_inicio"),
                        fecha_fin = reader.IsDBNull(reader.GetOrdinal("fecha_fin")) ? default : reader.GetDateTime("fecha_fin"),
                        tiempo_estimado = reader.IsDBNull(reader.GetOrdinal("tiempo_estimado")) ? default : reader.GetFieldValue<TimeSpan>(reader.GetOrdinal("tiempo_estimado")),
                        peso_estimado = reader.IsDBNull(reader.GetOrdinal("peso_estimado")) ? 0 : reader.GetDecimal("peso_estimado"),
                        observacion = reader.IsDBNull(reader.GetOrdinal("observacion")) ? string.Empty : reader.GetString("observacion")
                    });
                }
            }
            catch (MySqlException ex)
            {
                ArchivoLog.RegistrarErrores(ex);
                throw;
            }
            return lista;
        }

        public async Task<TrabajoImpresion?> ObtenerPorIdAsync(int id)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_trabajo_impresion_obtener_por_id";

                AddParam(cmd, "p_id", id);

                using var reader = await (cmd as MySqlCommand)!.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new TrabajoImpresion
                    {
                        id_trabajo_impresion = reader.GetInt32("id_trabajo_impresion"),
                        id_pedido = reader.GetInt32("id_pedido"),
                        id_producto = reader.GetInt32("id_producto"),
                        id_impresora = reader.GetInt32("id_impresora"),
                        id_modelo = reader.GetInt32("id_modelo"),
                        id_material = reader.GetInt32("id_material"),
                        cantidad = reader.GetInt32("cantidad"),
                        id_estado = reader.GetInt32("id_estado"),
                        fecha_inicio = reader.IsDBNull(reader.GetOrdinal("fecha_inicio")) ? default : reader.GetDateTime("fecha_inicio"),
                        fecha_fin = reader.IsDBNull(reader.GetOrdinal("fecha_fin")) ? default : reader.GetDateTime("fecha_fin"),
                        tiempo_estimado = reader.IsDBNull(reader.GetOrdinal("tiempo_estimado")) ? default : reader.GetFieldValue<TimeSpan>(reader.GetOrdinal("tiempo_estimado")),
                        peso_estimado = reader.IsDBNull(reader.GetOrdinal("peso_estimado")) ? 0 : reader.GetDecimal("peso_estimado"),
                        observacion = reader.IsDBNull(reader.GetOrdinal("observacion")) ? string.Empty : reader.GetString("observacion")
                    };
                }
            }
            catch (MySqlException ex)
            {
                ArchivoLog.RegistrarErrores(ex);
                throw;
            }
            return null;
        }

        public async Task<int> CrearAsync(TrabajoImpresion entidad)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_trabajo_impresion_crear";

                AddParam(cmd, "p_id_pedido", entidad.id_pedido);
                AddParam(cmd, "p_id_producto", entidad.id_producto);
                AddParam(cmd, "p_id_impresora", entidad.id_impresora);
                AddParam(cmd, "p_id_modelo", entidad.id_modelo);
                AddParam(cmd, "p_id_material", entidad.id_material);
                AddParam(cmd, "p_cantidad", entidad.cantidad);
                AddParam(cmd, "p_id_estado", entidad.id_estado);
                AddParam(cmd, "p_fecha_inicio", entidad.fecha_inicio == default ? DateTime.Now : entidad.fecha_inicio);
                AddParam(cmd, "p_peso_estimado", entidad.peso_estimado);
                AddParam(cmd, "p_observacion", entidad.observacion);

                var res = await (cmd as MySqlCommand)!.ExecuteScalarAsync();
                return Convert.ToInt32(res);
            }
            catch (MySqlException ex)
            {
                ArchivoLog.RegistrarErrores(ex);
                throw;
            }
        }

        public async Task<bool> ActualizarAsync(TrabajoImpresion entidad)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_trabajo_impresion_actualizar";

                AddParam(cmd, "p_id", entidad.id_trabajo_impresion);
                AddParam(cmd, "p_id_pedido", entidad.id_pedido);
                AddParam(cmd, "p_id_producto", entidad.id_producto);
                AddParam(cmd, "p_id_impresora", entidad.id_impresora);
                AddParam(cmd, "p_id_modelo", entidad.id_modelo);
                AddParam(cmd, "p_id_material", entidad.id_material);
                AddParam(cmd, "p_cantidad", entidad.cantidad);
                AddParam(cmd, "p_id_estado", entidad.id_estado);
                AddParam(cmd, "p_fecha_inicio", entidad.fecha_inicio);
                AddParam(cmd, "p_fecha_fin", entidad.fecha_fin == default ? (object)DBNull.Value : entidad.fecha_fin);
                AddParam(cmd, "p_peso_estimado", entidad.peso_estimado);
                AddParam(cmd, "p_observacion", entidad.observacion);

                int rows = await (cmd as MySqlCommand)!.ExecuteNonQueryAsync();
                return rows > 0;
            }
            catch (MySqlException ex)
            {
                ArchivoLog.RegistrarErrores(ex);
                throw;
            }
        }

        public async Task<bool> EliminarAsync(int id)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_trabajo_impresion_eliminar";

                AddParam(cmd, "p_id", id);

                int rows = await (cmd as MySqlCommand)!.ExecuteNonQueryAsync();
                return rows > 0;
            }
            catch (MySqlException ex)
            {
                ArchivoLog.RegistrarErrores(ex);
                throw;
            }
        }

        private static void AddParam(IDbCommand cmd, string name, object? value)
        {
            var p = cmd.CreateParameter();
            p.ParameterName = name;
            p.Value = value ?? DBNull.Value;
            cmd.Parameters.Add(p);
        }
    }
}
