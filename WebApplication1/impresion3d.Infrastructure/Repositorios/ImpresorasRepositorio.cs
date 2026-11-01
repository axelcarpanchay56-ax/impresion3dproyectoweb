using System.Data;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using impresion3d.Infrastructure.Contexto;
using MySqlConnector;

namespace impresion3d.Infrastructure.Repositorios
{
    public class ImpresorasRepositorio : IImpresorasRepositorio
    {
        private readonly IMiConexion _conexion;

        public ImpresorasRepositorio(IMiConexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<List<Impresoras>> ObtenerTodosAsync()
        {
            var lista = new List<Impresoras>();
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_impresoras_obtener_todas";

                using var reader = await (cmd as MySqlCommand)!.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    lista.Add(new Impresoras
                    {
                        id_impresora = reader.GetInt32("id_impresora"),
                        nombre = reader.GetString("nombre"),
                        marca = reader.GetString("marca"),
                        modelo = reader.GetString("modelo"),
                        volumen_x = reader.GetDecimal("volumen_x"),
                        volumen_y = reader.GetDecimal("volumen_y"),
                        volumen_z = reader.GetDecimal("volumen_z"),
                        tecnologia = reader.GetString("tecnologia"),
                        estado = reader.GetBoolean("estado"),
                        fecha_alta = reader.GetDateTime("fecha_alta")
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

        public async Task<Impresoras?> ObtenerPorIdAsync(int id)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_impresoras_obtener_por_id";

                AddParam(cmd, "p_id", id);

                using var reader = await (cmd as MySqlCommand)!.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Impresoras
                    {
                        id_impresora = reader.GetInt32("id_impresora"),
                        nombre = reader.GetString("nombre"),
                        marca = reader.GetString("marca"),
                        modelo = reader.GetString("modelo"),
                        volumen_x = reader.GetDecimal("volumen_x"),
                        volumen_y = reader.GetDecimal("volumen_y"),
                        volumen_z = reader.GetDecimal("volumen_z"),
                        tecnologia = reader.GetString("tecnologia"),
                        estado = reader.GetBoolean("estado"),
                        fecha_alta = reader.GetDateTime("fecha_alta")
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

        public async Task<int> CrearAsync(Impresoras entidad)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_impresoras_crear";

                AddParam(cmd, "p_nombre", entidad.nombre);
                AddParam(cmd, "p_marca", entidad.marca);
                AddParam(cmd, "p_modelo", entidad.modelo);
                AddParam(cmd, "p_volumen_x", entidad.volumen_x);
                AddParam(cmd, "p_volumen_y", entidad.volumen_y);
                AddParam(cmd, "p_volumen_z", entidad.volumen_z);
                AddParam(cmd, "p_tecnologia", entidad.tecnologia);
                AddParam(cmd, "p_estado", entidad.estado);
                AddParam(cmd, "p_fecha_alta", entidad.fecha_alta == default ? DateTime.Now : entidad.fecha_alta);

                var res = await (cmd as MySqlCommand)!.ExecuteScalarAsync();
                return Convert.ToInt32(res);
            }
            catch (MySqlException ex)
            {
                ArchivoLog.RegistrarErrores(ex);
                throw;
            }
        }

        public async Task<bool> ActualizarAsync(Impresoras entidad)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_impresoras_actualizar";

                AddParam(cmd, "p_id", entidad.id_impresora);
                AddParam(cmd, "p_nombre", entidad.nombre);
                AddParam(cmd, "p_marca", entidad.marca);
                AddParam(cmd, "p_modelo", entidad.modelo);
                AddParam(cmd, "p_volumen_x", entidad.volumen_x);
                AddParam(cmd, "p_volumen_y", entidad.volumen_y);
                AddParam(cmd, "p_volumen_z", entidad.volumen_z);
                AddParam(cmd, "p_tecnologia", entidad.tecnologia);
                AddParam(cmd, "p_estado", entidad.estado);

                int rows = await (cmd as MySqlCommand)!.ExecuteNonQueryAsync();
                return rows > 0;
            }
            catch (MySqlException ex)
            {
                ArchivoLog.RegistrarErrores(ex);
                throw;
            }
        }

        public async Task<bool> CambiarEstadoAsync(int id, bool estado)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_impresoras_cambiar_estado";

                AddParam(cmd, "p_id", id);
                AddParam(cmd, "p_estado", estado);

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