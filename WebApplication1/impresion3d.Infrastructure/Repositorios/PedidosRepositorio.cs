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
    public class PedidosRepositorio : IPedidosRepositorio
    {
        private readonly IMiConexion _conexion;

        public PedidosRepositorio(IMiConexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<List<Pedidos>> ObtenerTodosAsync()
        {
            var lista = new List<Pedidos>();
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_pedidos_obtener_todos";

                using var reader = await (cmd as MySqlCommand)!.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    lista.Add(new Pedidos
                    {
                        id_pedido = reader.GetInt32("id_pedido"),
                        id_cliente = reader.GetInt32("id_cliente"),
                        fecha_hora = reader.GetDateTime("fecha_hora"),
                        fecha_entrega_estimada = reader.IsDBNull(reader.GetOrdinal("fecha_entrega_estimada")) ? null : reader.GetDateTime("fecha_entrega_estimada"),
                        fecha_entrega_real = reader.IsDBNull(reader.GetOrdinal("fecha_entrega_real")) ? null : reader.GetDateTime("fecha_entrega_real"),
                        id_estado = reader.GetInt32("id_estado"),
                        total_estimado = reader.GetDecimal("total_estimado"),
                        subtotal = reader.GetDecimal("subtotal"),
                        descuento = reader.GetDecimal("descuento"),
                        total = reader.GetDecimal("total")
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

        public async Task<Pedidos?> ObtenerPorIdAsync(int id)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_pedidos_obtener_por_id";

                AddParam(cmd, "p_id", id);

                using var reader = await (cmd as MySqlCommand)!.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Pedidos
                    {
                        id_pedido = reader.GetInt32("id_pedido"),
                        id_cliente = reader.GetInt32("id_cliente"),
                        fecha_hora = reader.GetDateTime("fecha_hora"),
                        fecha_entrega_estimada = reader.IsDBNull(reader.GetOrdinal("fecha_entrega_estimada")) ? null : reader.GetDateTime("fecha_entrega_estimada"),
                        fecha_entrega_real = reader.IsDBNull(reader.GetOrdinal("fecha_entrega_real")) ? null : reader.GetDateTime("fecha_entrega_real"),
                        id_estado = reader.GetInt32("id_estado"),
                        total_estimado = reader.GetDecimal("total_estimado"),
                        subtotal = reader.GetDecimal("subtotal"),
                        descuento = reader.GetDecimal("descuento"),
                        total = reader.GetDecimal("total")
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

        public async Task<List<Pedidos>> ObtenerPorClienteAsync(int id_cliente)
        {
            var lista = new List<Pedidos>();
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_pedidos_obtener_por_cliente";

                AddParam(cmd, "p_id_cliente", id_cliente);

                using var reader = await (cmd as MySqlCommand)!.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    lista.Add(new Pedidos
                    {
                        id_pedido = reader.GetInt32("id_pedido"),
                        id_cliente = reader.GetInt32("id_cliente"),
                        fecha_hora = reader.GetDateTime("fecha_hora"),
                        fecha_entrega_estimada = reader.IsDBNull(reader.GetOrdinal("fecha_entrega_estimada")) ? null : reader.GetDateTime("fecha_entrega_estimada"),
                        fecha_entrega_real = reader.IsDBNull(reader.GetOrdinal("fecha_entrega_real")) ? null : reader.GetDateTime("fecha_entrega_real"),
                        id_estado = reader.GetInt32("id_estado"),
                        total_estimado = reader.GetDecimal("total_estimado"),
                        subtotal = reader.GetDecimal("subtotal"),
                        descuento = reader.GetDecimal("descuento"),
                        total = reader.GetDecimal("total")
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

        public async Task<int> CrearAsync(Pedidos entidad)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_pedidos_crear";

                AddParam(cmd, "p_id_cliente", entidad.id_cliente);
                AddParam(cmd, "p_fecha_hora", entidad.fecha_hora == default ? DateTime.Now : entidad.fecha_hora);
                AddParam(cmd, "p_fecha_entrega_estimada", entidad.fecha_entrega_estimada);
                AddParam(cmd, "p_fecha_entrega_real", entidad.fecha_entrega_real);
                AddParam(cmd, "p_id_estado", entidad.id_estado);
                AddParam(cmd, "p_total_estimado", entidad.total_estimado);
                AddParam(cmd, "p_subtotal", entidad.subtotal);
                AddParam(cmd, "p_descuento", entidad.descuento);
                AddParam(cmd, "p_total", entidad.total);

                var res = await (cmd as MySqlCommand)!.ExecuteScalarAsync();
                return Convert.ToInt32(res);
            }
            catch (MySqlException ex)
            {
                ArchivoLog.RegistrarErrores(ex);
                throw;
            }
        }

        public async Task<bool> ActualizarAsync(Pedidos entidad)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_pedidos_actualizar";

                AddParam(cmd, "p_id", entidad.id_pedido);
                AddParam(cmd, "p_id_cliente", entidad.id_cliente);
                AddParam(cmd, "p_fecha_hora", entidad.fecha_hora);
                AddParam(cmd, "p_fecha_entrega_estimada", entidad.fecha_entrega_estimada);
                AddParam(cmd, "p_fecha_entrega_real", entidad.fecha_entrega_real);
                AddParam(cmd, "p_id_estado", entidad.id_estado);
                AddParam(cmd, "p_total_estimado", entidad.total_estimado);
                AddParam(cmd, "p_subtotal", entidad.subtotal);
                AddParam(cmd, "p_descuento", entidad.descuento);
                AddParam(cmd, "p_total", entidad.total);

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
                cmd.CommandText = "sp_pedidos_eliminar";

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
