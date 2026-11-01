using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using impresion3d.Infrastructure.Contexto;
using MySqlConnector;

namespace impresion3d.Infrastructure.Repositorios
{
    public class ImpresoraMaterialRepositorio : IImpresoraMaterialRepositorio
    {
        private readonly IMiConexion _conexion;

        public ImpresoraMaterialRepositorio(IMiConexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<List<ImpresoraMaterial>> ObtenerTodosAsync()
        {
            var lista = new List<ImpresoraMaterial>();
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_impresora_material_obtener_todos";

                using var reader = await (cmd as MySqlCommand)!.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    lista.Add(new ImpresoraMaterial
                    {
                        id_impresora = reader.GetInt32("id_impresora"),
                        id_material = reader.GetInt32("id_material"),
                        observacion = reader.IsDBNull(reader.GetOrdinal("observacion")) ? null : reader.GetString("observacion")
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

        public async Task<ImpresoraMaterial?> ObtenerPorIdAsync(int id_impresora, int id_material)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_impresora_material_obtener_por_id";

                AddParam(cmd, "p_id_impresora", id_impresora);
                AddParam(cmd, "p_id_material", id_material);

                using var reader = await (cmd as MySqlCommand)!.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new ImpresoraMaterial
                    {
                        id_impresora = reader.GetInt32("id_impresora"),
                        id_material = reader.GetInt32("id_material"),
                        observacion = reader.IsDBNull(reader.GetOrdinal("observacion")) ? null : reader.GetString("observacion")
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

        public async Task<List<ImpresoraMaterial>> ObtenerPorImpresoraAsync(int id_impresora)
        {
            var lista = new List<ImpresoraMaterial>();
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_impresora_material_obtener_por_impresora";

                AddParam(cmd, "p_id_impresora", id_impresora);

                using var reader = await (cmd as MySqlCommand)!.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    lista.Add(new ImpresoraMaterial
                    {
                        id_impresora = reader.GetInt32("id_impresora"),
                        id_material = reader.GetInt32("id_material"),
                        observacion = reader.IsDBNull(reader.GetOrdinal("observacion")) ? null : reader.GetString("observacion")
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

        public async Task<bool> CrearAsync(ImpresoraMaterial entidad)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_impresora_material_crear";

                AddParam(cmd, "p_id_impresora", entidad.id_impresora);
                AddParam(cmd, "p_id_material", entidad.id_material);
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

        public async Task<bool> ActualizarAsync(ImpresoraMaterial entidad)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_impresora_material_actualizar";

                AddParam(cmd, "p_id_impresora", entidad.id_impresora);
                AddParam(cmd, "p_id_material", entidad.id_material);
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

        public async Task<bool> EliminarAsync(int id_impresora, int id_material)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_impresora_material_eliminar";

                AddParam(cmd, "p_id_impresora", id_impresora);
                AddParam(cmd, "p_id_material", id_material);

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
