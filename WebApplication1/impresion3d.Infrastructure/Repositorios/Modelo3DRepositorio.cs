using System.Data;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using impresion3d.Infrastructure.Contexto;
using MySqlConnector;
using Dapper;

namespace impresion3d.Infrastructure.Repositorios
{
    public class Modelo3DRepositorio : IModelo3DRepositorio
    {
        private readonly IMiConexion _conexion;

        public Modelo3DRepositorio(IMiConexion conexion)
        {
            _conexion = conexion;

            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<List<Modelo3D>> ObtenerTodosAsync()
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var lista =
                    await conexion.QueryAsync<Modelo3D>(
                        "sp_modelo3d_obtener_todos",
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

        public async Task<Modelo3D?> ObtenerPorIdAsync(int id)
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

                return await conexion.QueryFirstOrDefaultAsync<Modelo3D>(
                    "sp_modelo3d_obtener_por_id",
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

        public async Task<int> CrearAsync(Modelo3D entidad)
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var parametros = new DynamicParameters();

                parametros.Add("p_nombre", entidad.nombre);
                parametros.Add("p_archivo", entidad.archivo);
                parametros.Add("p_formato", entidad.formato);
                parametros.Add("p_descripcion", entidad.descripcion);
                parametros.Add(
                    "p_fecha_creacion",
                    entidad.fecha_creacion == default
                        ? DateTime.Now
                        : entidad.fecha_creacion
                );

                return await conexion.ExecuteScalarAsync<int>(
                    "sp_modelo3d_crear",
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

        public async Task<bool> ActualizarAsync(Modelo3D entidad)
        {
            try
            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var parametros = new DynamicParameters();

                parametros.Add("p_id", entidad.id_modelo3d);
                parametros.Add("p_nombre", entidad.nombre);
                parametros.Add("p_archivo", entidad.archivo);
                parametros.Add("p_formato", entidad.formato);
                parametros.Add("p_descripcion", entidad.descripcion);

                int filasAfectadas =
                    await conexion.ExecuteAsync(
                        "sp_modelo3d_actualizar",
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
                        "sp_modelo3d_eliminar",
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
