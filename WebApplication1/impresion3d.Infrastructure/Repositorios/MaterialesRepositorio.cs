using System.Data;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using impresion3d.Infrastructure.Contexto;
using MySqlConnector;

namespace impresion3d.Infrastructure.Repositorios
{
    public class MaterialesRepositorio : IMaterialesRepositorio
    {
        private readonly IMiConexion _conexion;

        public MaterialesRepositorio(IMiConexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<List<Materiales>> ObtenerTodosAsync()
        {
            var lista = new List<Materiales>();
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_materiales_obtener_todos";

                using var reader = await (cmd as MySqlCommand)!.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    lista.Add(new Materiales
                    {
                        id_material = reader.GetInt32("id_material"),
                        nombre = reader.GetString("nombre"),
                        tipo = reader.GetString("tipo"),
                        unidad_medida = reader.GetString("unidad_medida"),
                        color = reader.GetString("color"),
                        diametro = reader.IsDBNull(reader.GetOrdinal("diametro")) ? (decimal?)null : reader.GetDecimal("diametro"),
                        activo = reader.GetBoolean("activo")
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

        public async Task<Materiales?> ObtenerPorIdAsync(int id)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_materiales_obtener_por_id";

                AddParam(cmd, "p_id", id);

                using var reader = await (cmd as MySqlCommand)!.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Materiales
                    {
                        id_material = reader.GetInt32("id_material"),
                        nombre = reader.GetString("nombre"),
                        tipo = reader.GetString("tipo"),
                        unidad_medida = reader.GetString("unidad_medida"),
                        color = reader.GetString("color"),
                        diametro = reader.IsDBNull(reader.GetOrdinal("diametro")) ? (decimal?)null : reader.GetDecimal("diametro"),
                        activo = reader.GetBoolean("activo")
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

        public async Task<int> CrearAsync(Materiales entidad)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_materiales_crear";

                AddParam(cmd, "p_nombre", entidad.nombre);
                AddParam(cmd, "p_tipo", entidad.tipo);
                AddParam(cmd, "p_unidad_medida", entidad.unidad_medida);
                AddParam(cmd, "p_color", entidad.color);
                AddParam(cmd, "p_diametro", entidad.diametro);
                AddParam(cmd, "p_activo", entidad.activo);

                var res = await (cmd as MySqlCommand)!.ExecuteScalarAsync();
                return Convert.ToInt32(res);
            }
            catch (MySqlException ex)
            {
                ArchivoLog.RegistrarErrores(ex);
                throw;
            }
        }

        public async Task<bool> ActualizarAsync(Materiales entidad)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_materiales_actualizar";

                AddParam(cmd, "p_id", entidad.id_material);
                AddParam(cmd, "p_nombre", entidad.nombre);
                AddParam(cmd, "p_tipo", entidad.tipo);
                AddParam(cmd, "p_unidad_medida", entidad.unidad_medida);
                AddParam(cmd, "p_color", entidad.color);
                AddParam(cmd, "p_diametro", entidad.diametro);
                AddParam(cmd, "p_activo", entidad.activo);

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
                cmd.CommandText = "sp_materiales_eliminar";

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