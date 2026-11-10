using System.Data;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using impresion3d.Infrastructure.Contexto;
using MySqlConnector;
using Dapper;

namespace impresion3d.Infrastructure.Repositorios
{
    public class TrabajoImpresionRepositorio : ITrabajoImpresionRepositorio
    {
        private readonly IMiConexion _conexion;

        public TrabajoImpresionRepositorio(IMiConexion conexion)
        {
            _conexion = conexion;

            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<List<TrabajoImpresion>> ObtenerTodosAsync()
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var lista =
                    await conexion.QueryAsync<TrabajoImpresion>(
                        "sp_trabajo_impresion_obtener_todos",
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

        public async Task<TrabajoImpresion?> ObtenerPorIdAsync(int id)
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

                return await conexion.QueryFirstOrDefaultAsync<TrabajoImpresion>(
                    "sp_trabajo_impresion_obtener_por_id",
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

        public async Task<int> CrearAsync(TrabajoImpresion entidad)
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var parametros = new DynamicParameters();

                parametros.Add("p_id_pedido", entidad.id_pedido);
                parametros.Add("p_id_producto", entidad.id_producto);
                parametros.Add("p_id_impresora", entidad.id_impresora);
                parametros.Add("p_id_modelo", entidad.id_modelo);
                parametros.Add("p_id_material", entidad.id_material);
                parametros.Add("p_cantidad", entidad.cantidad);
                parametros.Add("p_id_estado", entidad.id_estado);
                parametros.Add(
                    "p_fecha_inicio",
                    entidad.fecha_inicio == default
                        ? DateTime.Now
                        : entidad.fecha_inicio
                );
                parametros.Add("p_peso_estimado", entidad.peso_estimado);
                parametros.Add("p_observacion", entidad.observacion);

                return await conexion.ExecuteScalarAsync<int>(
                    "sp_trabajo_impresion_crear",
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

        public async Task<bool> ActualizarAsync(TrabajoImpresion entidad)
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var parametros = new DynamicParameters();

                parametros.Add("p_id", entidad.id_trabajo_impresion);
                parametros.Add("p_id_pedido", entidad.id_pedido);
                parametros.Add("p_id_producto", entidad.id_producto);
                parametros.Add("p_id_impresora", entidad.id_impresora);
                parametros.Add("p_id_modelo", entidad.id_modelo);
                parametros.Add("p_id_material", entidad.id_material);
                parametros.Add("p_cantidad", entidad.cantidad);
                parametros.Add("p_id_estado", entidad.id_estado);
                parametros.Add("p_fecha_inicio", entidad.fecha_inicio);
                parametros.Add(
                    "p_fecha_fin",
                    entidad.fecha_fin == default
                        ? (DateTime?)null
                        : entidad.fecha_fin
                );
                parametros.Add("p_peso_estimado", entidad.peso_estimado);
                parametros.Add("p_observacion", entidad.observacion);

                int filasAfectadas =
                    await conexion.ExecuteAsync(
                        "sp_trabajo_impresion_actualizar",
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
                        "sp_trabajo_impresion_eliminar",
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
