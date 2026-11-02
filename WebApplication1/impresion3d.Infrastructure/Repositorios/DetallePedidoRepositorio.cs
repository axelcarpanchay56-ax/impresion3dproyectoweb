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
    public class DetallePedidoRepositorio : IDetallePedidoRepositorio
    {
        private readonly IMiConexion _conexion;

        public DetallePedidoRepositorio(IMiConexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<List<DetallePedido>> ObtenerTodosAsync()
        {
            var lista = new List<DetallePedido>();
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_detalle_pedido_obtener_todos";

                using var reader = await (cmd as MySqlCommand)!.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    lista.Add(new DetallePedido
                    {
                        id_detalle_pedido = reader.GetInt32("id_detalle_pedido"),
                        id_pedido = reader.GetInt32("id_pedido"),
                        id_producto = reader.GetInt32("id_producto"),
                        cantidad = reader.GetInt32("cantidad"),
                        precio_unitario = reader.GetDecimal("precio_unitario"),
                        subtotal = reader.GetDecimal("subtotal")
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

        public async Task<DetallePedido?> ObtenerPorIdAsync(int id)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_detalle_pedido_obtener_por_id";

                AddParam(cmd, "p_id", id);

                using var reader = await (cmd as MySqlCommand)!.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new DetallePedido
                    {
                        id_detalle_pedido = reader.GetInt32("id_detalle_pedido"),
                        id_pedido = reader.GetInt32("id_pedido"),
                        id_producto = reader.GetInt32("id_producto"),
                        cantidad = reader.GetInt32("cantidad"),
                        precio_unitario = reader.GetDecimal("precio_unitario"),
                        subtotal = reader.GetDecimal("subtotal")
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

        public async Task<List<DetallePedido>> ObtenerPorPedidoAsync(int id_pedido)
        {
            var lista = new List<DetallePedido>();
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_detalle_pedido_obtener_por_pedido";

                AddParam(cmd, "p_id_pedido", id_pedido);

                using var reader = await (cmd as MySqlCommand)!.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    lista.Add(new DetallePedido
                    {
                        id_detalle_pedido = reader.GetInt32("id_detalle_pedido"),
                        id_pedido = reader.GetInt32("id_pedido"),
                        id_producto = reader.GetInt32("id_producto"),
                        cantidad = reader.GetInt32("cantidad"),
                        precio_unitario = reader.GetDecimal("precio_unitario"),
                        subtotal = reader.GetDecimal("subtotal")
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

        public async Task<int> CrearAsync(DetallePedido entidad)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_detalle_pedido_crear";

                AddParam(cmd, "p_id_pedido", entidad.id_pedido);
                AddParam(cmd, "p_id_producto", entidad.id_producto);
                AddParam(cmd, "p_cantidad", entidad.cantidad);
                AddParam(cmd, "p_precio_unitario", entidad.precio_unitario);
                AddParam(cmd, "p_subtotal", entidad.subtotal);

                var res = await (cmd as MySqlCommand)!.ExecuteScalarAsync();
                return Convert.ToInt32(res);
            }
            catch (MySqlException ex)
            {
                ArchivoLog.RegistrarErrores(ex);
                throw;
            }
        }

        public async Task<bool> ActualizarAsync(DetallePedido entidad)
        {
            try
            {
                using var conn = await _conexion.ObtenerConexionAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_detalle_pedido_actualizar";

                AddParam(cmd, "p_id", entidad.id_detalle_pedido);
                AddParam(cmd, "p_id_pedido", entidad.id_pedido);
                AddParam(cmd, "p_id_producto", entidad.id_producto);
                AddParam(cmd, "p_cantidad", entidad.cantidad);
                AddParam(cmd, "p_precio_unitario", entidad.precio_unitario);
                AddParam(cmd, "p_subtotal", entidad.subtotal);

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
                cmd.CommandText = "sp_detalle_pedido_eliminar";

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
