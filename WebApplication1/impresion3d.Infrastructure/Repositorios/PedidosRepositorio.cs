using System.Data;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using impresion3d.Infrastructure.Contexto;
using MySqlConnector;
using Dapper;

namespace impresion3d.Infrastructure.Repositorios
{
    public class PedidosRepositorio : IPedidosRepositorio
    {
        private readonly IMiConexion _conexion;

        public PedidosRepositorio(IMiConexion conexion)
        {
            _conexion = conexion;

            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<List<Pedidos>> ObtenerTodosAsync()
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var lista =
                    await conexion.QueryAsync<Pedidos>(
                        "sp_pedidos_obtener_todos",
                        commandType: CommandType.StoredProcedure
                    );

                return lista.ToList();
            }
            catch (MySqlException ex)
            {
                ArchivoLog.RegistrarErrores(ex);
                throw;
            }
        }

        public async Task<Pedidos?> ObtenerPorIdAsync(int id)
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var parametros = new DynamicParameters();

                parametros.Add(
                    "p_id",
                    id,
                    DbType.Int32,
                    ParameterDirection.Input
                );

                return await conexion.QueryFirstOrDefaultAsync<Pedidos>(
                    "sp_pedidos_obtener_por_id",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (MySqlException ex)
            {
                ArchivoLog.RegistrarErrores(ex);
                throw;
            }
        }

        public async Task<List<Pedidos>> ObtenerPorClienteAsync(int id_cliente)
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var parametros = new DynamicParameters();

                parametros.Add(
                    "p_id_cliente",
                    id_cliente,
                    DbType.Int32,
                    ParameterDirection.Input
                );

                var lista =
                    await conexion.QueryAsync<Pedidos>(
                        "sp_pedidos_obtener_por_cliente",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                return lista.ToList();
            }
            catch (MySqlException ex)
            {
                ArchivoLog.RegistrarErrores(ex);
                throw;
            }
        }

        public async Task<int> CrearAsync(Pedidos entidad)
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var parametros = new DynamicParameters();

                parametros.Add("p_id_cliente", entidad.id_cliente);
                parametros.Add(
                    "p_fecha_hora",
                    entidad.fecha_hora == default
                        ? DateTime.Now
                        : entidad.fecha_hora
                );
                parametros.Add("p_fecha_entrega_estimada", entidad.fecha_entrega_estimada);
                parametros.Add("p_fecha_entrega_real", entidad.fecha_entrega_real);
                parametros.Add("p_id_estado", entidad.id_estado);
                parametros.Add("p_total_estimado", entidad.total_estimado);
                parametros.Add("p_subtotal", entidad.subtotal);
                parametros.Add("p_descuento", entidad.descuento);
                parametros.Add("p_total", entidad.total);

                return await conexion.ExecuteScalarAsync<int>(
                    "sp_pedidos_crear",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );
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
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var parametros = new DynamicParameters();

                parametros.Add("p_id", entidad.id_pedido);
                parametros.Add("p_id_cliente", entidad.id_cliente);
                parametros.Add("p_fecha_hora", entidad.fecha_hora);
                parametros.Add("p_fecha_entrega_estimada", entidad.fecha_entrega_estimada);
                parametros.Add("p_fecha_entrega_real", entidad.fecha_entrega_real);
                parametros.Add("p_id_estado", entidad.id_estado);
                parametros.Add("p_total_estimado", entidad.total_estimado);
                parametros.Add("p_subtotal", entidad.subtotal);
                parametros.Add("p_descuento", entidad.descuento);
                parametros.Add("p_total", entidad.total);

                int filasAfectadas =
                    await conexion.ExecuteAsync(
                        "sp_pedidos_actualizar",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                return filasAfectadas > 0;
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
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var parametros = new DynamicParameters();

                parametros.Add(
                    "p_id",
                    id,
                    DbType.Int32,
                    ParameterDirection.Input
                );

                int filasAfectadas =
                    await conexion.ExecuteAsync(
                        "sp_pedidos_eliminar",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                return filasAfectadas > 0;
            }
            catch (MySqlException ex)
            {
                ArchivoLog.RegistrarErrores(ex);
                throw;
            }
        }
    }
}
