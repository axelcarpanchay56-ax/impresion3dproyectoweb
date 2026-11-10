using System.Data;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using impresion3d.Infrastructure.Contexto;
using MySqlConnector;
using Dapper;

namespace impresion3d.Infrastructure.Repositorios
{
    public class ImpresoraMaterialRepositorio : IImpresoraMaterialRepositorio
    {
        private readonly IMiConexion _conexion;

        public ImpresoraMaterialRepositorio(IMiConexion conexion)
        {
            _conexion = conexion;

            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<List<ImpresoraMaterial>> ObtenerTodosAsync()
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var lista =
                    await conexion.QueryAsync<ImpresoraMaterial>(
                        "sp_impresora_material_obtener_todos",
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

        public async Task<ImpresoraMaterial?> ObtenerPorIdAsync(
            int id_impresora,
            int id_material)
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var parametros = new DynamicParameters();

                parametros.Add("p_id_impresora", id_impresora);
                parametros.Add("p_id_material", id_material);

                return await conexion.QueryFirstOrDefaultAsync<ImpresoraMaterial>(
                    "sp_impresora_material_obtener_por_id",
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

        public async Task<List<ImpresoraMaterial>> ObtenerPorImpresoraAsync(
            int id_impresora)
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var parametros = new DynamicParameters();

                parametros.Add(
                    "p_id_impresora",
                    id_impresora,
                    DbType.Int32,
                    ParameterDirection.Input
                );

                var lista =
                    await conexion.QueryAsync<ImpresoraMaterial>(
                        "sp_impresora_material_obtener_por_impresora",
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

        public async Task<bool> CrearAsync(ImpresoraMaterial entidad)
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var parametros = new DynamicParameters();

                parametros.Add("p_id_impresora", entidad.id_impresora);
                parametros.Add("p_id_material", entidad.id_material);
                parametros.Add("p_observacion", entidad.observacion);

                int filasAfectadas =
                    await conexion.ExecuteAsync(
                        "sp_impresora_material_crear",
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

        public async Task<bool> ActualizarAsync(ImpresoraMaterial entidad)
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var parametros = new DynamicParameters();

                parametros.Add("p_id_impresora", entidad.id_impresora);
                parametros.Add("p_id_material", entidad.id_material);
                parametros.Add("p_observacion", entidad.observacion);

                int filasAfectadas =
                    await conexion.ExecuteAsync(
                        "sp_impresora_material_actualizar",
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

        public async Task<bool> EliminarAsync(int id_impresora, int id_material)
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var parametros = new DynamicParameters();

                parametros.Add("p_id_impresora", id_impresora);
                parametros.Add("p_id_material", id_material);

                int filasAfectadas =
                    await conexion.ExecuteAsync(
                        "sp_impresora_material_eliminar",
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
