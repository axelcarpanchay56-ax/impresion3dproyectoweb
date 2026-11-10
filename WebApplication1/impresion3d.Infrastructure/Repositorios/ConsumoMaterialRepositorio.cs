using System.Data;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using impresion3d.Infrastructure.Contexto;
using MySqlConnector;
using Dapper;

namespace impresion3d.Infrastructure.Repositorios
{
    public class ConsumoMaterialRepositorio : IConsumoMaterialRepositorio
    {
        private readonly IMiConexion _conexion;

        public ConsumoMaterialRepositorio(IMiConexion conexion)
        {
            _conexion = conexion;

            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<List<ConsumoMaterial>> ObtenerTodosAsync()
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var lista =
                    await conexion.QueryAsync<ConsumoMaterial>(
                        "sp_consumo_material_obtener_todos",
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

        public async Task<ConsumoMaterial?> ObtenerPorIdAsync(int id)
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

                return await conexion.QueryFirstOrDefaultAsync<ConsumoMaterial>(
                    "sp_consumo_material_obtener_por_id",
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

        public async Task<List<ConsumoMaterial>> ObtenerPorTrabajoAsync(
            int id_trabajo)
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var parametros = new DynamicParameters();

                parametros.Add(
                    "p_id_trabajo",
                    id_trabajo,
                    DbType.Int32,
                    ParameterDirection.Input
                );

                var lista =
                    await conexion.QueryAsync<ConsumoMaterial>(
                        "sp_consumo_material_obtener_por_trabajo",
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

        public async Task<int> CrearAsync(ConsumoMaterial entidad)
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var parametros = new DynamicParameters();

                parametros.Add("p_id_trabajo", entidad.id_trabajo);
                parametros.Add("p_id_material", entidad.id_material);
                parametros.Add("p_cantidad", entidad.cantidad);
                parametros.Add(
                    "p_fecha_hora",
                    entidad.fecha_hora == default
                        ? DateTime.Now
                        : entidad.fecha_hora
                );

                return await conexion.ExecuteScalarAsync<int>(
                    "sp_consumo_material_crear",
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

        public async Task<bool> ActualizarAsync(ConsumoMaterial entidad)
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var parametros = new DynamicParameters();

                parametros.Add("p_id", entidad.id_consumo_material);
                parametros.Add("p_id_trabajo", entidad.id_trabajo);
                parametros.Add("p_id_material", entidad.id_material);
                parametros.Add("p_cantidad", entidad.cantidad);
                parametros.Add("p_fecha_hora", entidad.fecha_hora);

                int filasAfectadas =
                    await conexion.ExecuteAsync(
                        "sp_consumo_material_actualizar",
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
                        "sp_consumo_material_eliminar",
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
