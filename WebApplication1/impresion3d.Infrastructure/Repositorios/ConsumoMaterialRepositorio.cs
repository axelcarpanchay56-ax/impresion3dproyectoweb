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
    public class ConsumoMaterialRepositorio : IConsumoMaterialRepositorio
    {
        private readonly IMiConexion _conexion;

        public ConsumoMaterialRepositorio(IMiConexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<List<ConsumoMaterial>> ObtenerTodosAsync()
        {
            var lista = new List<ConsumoMaterial>();
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_consumo_material_obtener_todos";

                using var reader = await (cmd as MySqlCommand)!.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    lista.Add(new ConsumoMaterial
                    {
                        id_consumo_material = reader.GetInt32("id_consumo_material"),
                        id_trabajo = reader.GetInt32("id_trabajo"),
                        id_material = reader.GetInt32("id_material"),
                        cantidad = reader.GetDecimal("cantidad"),
                        fecha_hora = reader.IsDBNull(reader.GetOrdinal("fecha_hora")) ? default : reader.GetDateTime("fecha_hora")
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

        public async Task<ConsumoMaterial?> ObtenerPorIdAsync(int id)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_consumo_material_obtener_por_id";

                AddParam(cmd, "p_id", id);

                using var reader = await (cmd as MySqlCommand)!.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new ConsumoMaterial
                    {
                        id_consumo_material = reader.GetInt32("id_consumo_material"),
                        id_trabajo = reader.GetInt32("id_trabajo"),
                        id_material = reader.GetInt32("id_material"),
                        cantidad = reader.GetDecimal("cantidad"),
                        fecha_hora = reader.IsDBNull(reader.GetOrdinal("fecha_hora")) ? default : reader.GetDateTime("fecha_hora")
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

        public async Task<List<ConsumoMaterial>> ObtenerPorTrabajoAsync(int id_trabajo)
        {
            var lista = new List<ConsumoMaterial>();
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_consumo_material_obtener_por_trabajo";

                AddParam(cmd, "p_id_trabajo", id_trabajo);

                using var reader = await (cmd as MySqlCommand)!.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    lista.Add(new ConsumoMaterial
                    {
                        id_consumo_material = reader.GetInt32("id_consumo_material"),
                        id_trabajo = reader.GetInt32("id_trabajo"),
                        id_material = reader.GetInt32("id_material"),
                        cantidad = reader.GetDecimal("cantidad"),
                        fecha_hora = reader.IsDBNull(reader.GetOrdinal("fecha_hora")) ? default : reader.GetDateTime("fecha_hora")
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

        public async Task<int> CrearAsync(ConsumoMaterial entidad)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_consumo_material_crear";

                AddParam(cmd, "p_id_trabajo", entidad.id_trabajo);
                AddParam(cmd, "p_id_material", entidad.id_material);
                AddParam(cmd, "p_cantidad", entidad.cantidad);
                AddParam(cmd, "p_fecha_hora", entidad.fecha_hora == default ? DateTime.Now : entidad.fecha_hora);

                var res = await (cmd as MySqlCommand)!.ExecuteScalarAsync();
                return Convert.ToInt32(res);
            }
            catch (MySqlException ex)
            {
                ArchivoLog.RegistrarErrores(ex);
                throw;
            }
        }

        public async Task<bool> ActualizarAsync(ConsumoMaterial entidad)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_consumo_material_actualizar";

                AddParam(cmd, "p_id", entidad.id_consumo_material);
                AddParam(cmd, "p_id_trabajo", entidad.id_trabajo);
                AddParam(cmd, "p_id_material", entidad.id_material);
                AddParam(cmd, "p_cantidad", entidad.cantidad);
                AddParam(cmd, "p_fecha_hora", entidad.fecha_hora);

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
                cmd.CommandText = "sp_consumo_material_eliminar";

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
