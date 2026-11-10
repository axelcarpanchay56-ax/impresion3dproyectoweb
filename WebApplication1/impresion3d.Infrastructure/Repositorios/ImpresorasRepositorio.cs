using System.Data;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using impresion3d.Infrastructure.Contexto;
using MySqlConnector;
using Dapper;

namespace impresion3d.Infrastructure.Repositorios
{
    public class ImpresorasRepositorio : IImpresorasRepositorio
    {
        private readonly IMiConexion _conexion;

        public ImpresorasRepositorio(IMiConexion conexion)
        {
            _conexion = conexion;

            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<List<Impresoras>> ObtenerTodosAsync()
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var lista =
                    await conexion.QueryAsync<Impresoras>(
                        "sp_impresoras_obtener_todas",
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

        public async Task<Impresoras?> ObtenerPorIdAsync(int id)
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

                return await conexion.QueryFirstOrDefaultAsync<Impresoras>(
                    "sp_impresoras_obtener_por_id",
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

        public async Task<int> CrearAsync(Impresoras entidad)
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var parametros = new DynamicParameters();

                parametros.Add("p_nombre", entidad.nombre);
                parametros.Add("p_marca", entidad.marca);
                parametros.Add("p_modelo", entidad.modelo);
                parametros.Add("p_volumen_x", entidad.volumen_x);
                parametros.Add("p_volumen_y", entidad.volumen_y);
                parametros.Add("p_volumen_z", entidad.volumen_z);
                parametros.Add("p_tecnologia", entidad.tecnologia);
                parametros.Add("p_estado", entidad.estado);
                parametros.Add(
                    "p_fecha_alta",
                    entidad.fecha_alta == default
                        ? DateTime.Now
                        : entidad.fecha_alta
                );

                return await conexion.ExecuteScalarAsync<int>(
                    "sp_impresoras_crear",
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

        public async Task<bool> ActualizarAsync(Impresoras entidad)
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var parametros = new DynamicParameters();

                parametros.Add("p_id", entidad.id_impresora);
                parametros.Add("p_nombre", entidad.nombre);
                parametros.Add("p_marca", entidad.marca);
                parametros.Add("p_modelo", entidad.modelo);
                parametros.Add("p_volumen_x", entidad.volumen_x);
                parametros.Add("p_volumen_y", entidad.volumen_y);
                parametros.Add("p_volumen_z", entidad.volumen_z);
                parametros.Add("p_tecnologia", entidad.tecnologia);
                parametros.Add("p_estado", entidad.estado);

                int filasAfectadas =
                    await conexion.ExecuteAsync(
                        "sp_impresoras_actualizar",
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

        public async Task<bool> CambiarEstadoAsync(int id, bool estado)
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var parametros = new DynamicParameters();

                parametros.Add("p_id", id);
                parametros.Add("p_estado", estado);

                int filasAfectadas =
                    await conexion.ExecuteAsync(
                        "sp_impresoras_cambiar_estado",
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
