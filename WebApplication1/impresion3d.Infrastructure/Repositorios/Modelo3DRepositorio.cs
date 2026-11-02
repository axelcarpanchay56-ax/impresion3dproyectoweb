using System.Data;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using impresion3d.Infrastructure.Contexto;
using MySqlConnector;

namespace impresion3d.Infrastructure.Repositorios
{
    public class Modelo3DRepositorio : IModelo3DRepositorio
    {
        private readonly IMiConexion _conexion;

        public Modelo3DRepositorio(IMiConexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<List<Modelo3D>> ObtenerTodosAsync()
        {
            var lista = new List<Modelo3D>();
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_modelo3d_obtener_todos";

                using var reader = await (cmd as MySqlCommand)!.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    lista.Add(new Modelo3D
                    {
                        id_modelo3d = reader.GetInt32("id_modelo3d"),
                        nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? string.Empty : reader.GetString("nombre"),
                        archivo = reader.IsDBNull(reader.GetOrdinal("archivo")) ? string.Empty : reader.GetString("archivo"),
                        formato = reader.IsDBNull(reader.GetOrdinal("formato")) ? string.Empty : reader.GetString("formato"),
                        descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) ? string.Empty : reader.GetString("descripcion"),
                        fecha_creacion = reader.GetDateTime("fecha_creacion")
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

        public async Task<Modelo3D?> ObtenerPorIdAsync(int id)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_modelo3d_obtener_por_id";

                var paramId = cmd.CreateParameter();
                paramId.ParameterName = "p_id";
                paramId.Value = id;
                cmd.Parameters.Add(paramId);

                using var reader = await (cmd as MySqlCommand)!.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Modelo3D
                    {
                        id_modelo3d = reader.GetInt32("id_modelo3d"),
                        nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? string.Empty : reader.GetString("nombre"),
                        archivo = reader.IsDBNull(reader.GetOrdinal("archivo")) ? string.Empty : reader.GetString("archivo"),
                        formato = reader.IsDBNull(reader.GetOrdinal("formato")) ? string.Empty : reader.GetString("formato"),
                        descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) ? string.Empty : reader.GetString("descripcion"),
                        fecha_creacion = reader.GetDateTime("fecha_creacion")
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

        public async Task<int> CrearAsync(Modelo3D entidad)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_modelo3d_crear";

                AddParam(cmd, "p_nombre", entidad.nombre);
                AddParam(cmd, "p_archivo", entidad.archivo);
                AddParam(cmd, "p_formato", entidad.formato);
                AddParam(cmd, "p_descripcion", entidad.descripcion);
                AddParam(cmd, "p_fecha_creacion", entidad.fecha_creacion == default ? DateTime.Now : entidad.fecha_creacion);

                var res = await (cmd as MySqlCommand)!.ExecuteScalarAsync();
                return Convert.ToInt32(res);
            }
            catch (MySqlException ex)
            {
                ArchivoLog.RegistrarErrores(ex);
                throw;
            }
        }

        public async Task<bool> ActualizarAsync(Modelo3D entidad)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_modelo3d_actualizar";

                AddParam(cmd, "p_id", entidad.id_modelo3d);
                AddParam(cmd, "p_nombre", entidad.nombre);
                AddParam(cmd, "p_archivo", entidad.archivo);
                AddParam(cmd, "p_formato", entidad.formato);
                AddParam(cmd, "p_descripcion", entidad.descripcion);

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
                cmd.CommandText = "sp_modelo3d_eliminar";

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
