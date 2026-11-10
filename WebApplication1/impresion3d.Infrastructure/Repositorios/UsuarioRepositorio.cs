using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using System.Data;
using Dapper;

namespace impresion3d.Infrastructure.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly IMiConexion _conexion;

        public UsuarioRepositorio(IMiConexion conexion)
        {
            _conexion = conexion;

            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<Usuarios?> LoginAsync(
            string usuario,
            string passwordHash)
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add(
                "p_usuario",
                usuario,
                DbType.String,
                ParameterDirection.Input
            );

            parametros.Add(
                "p_password_hash",
                passwordHash,
                DbType.String,
                ParameterDirection.Input
            );

            var usuarioEncontrado =
                await conexion.QueryFirstOrDefaultAsync<Usuarios>(
                    "sp_usuarios_login",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return usuarioEncontrado;
        }

        public async Task<int> CrearAsync(Usuarios usuario)
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add("p_id_persona", usuario.id_persona);
            parametros.Add("p_usuario", usuario.usuario);
            parametros.Add("p_password_hash", usuario.password_hash);
            parametros.Add("p_activo", usuario.activo);
            parametros.Add("p_fecha_creacion", usuario.fecha_creacion);
            parametros.Add("p_ultimo_acceso", usuario.ultimo_acceso);

            int filasAfectadas =
                await conexion.ExecuteAsync(
                    "sp_usuarios_crear",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return filasAfectadas;
        }

        public async Task<bool> CambiarEstadoAsync(
            int idUsuario,
            bool activo)
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add("p_id_usuario", idUsuario);
            parametros.Add("p_activo", activo);

            int filasAfectadas =
                await conexion.ExecuteAsync(
                    "sp_usuarios_cambiar_estado",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return filasAfectadas > 0;
        }

        public async Task<Usuarios?> ObtenerPorUsuarioAsync(string usuario)
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add(
                "p_usuario",
                usuario,
                DbType.String,
                ParameterDirection.Input
            );

            var usuarioEncontrado =
                await conexion.QueryFirstOrDefaultAsync<Usuarios>(
                    "sp_usuario_obtener_por_usuario",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return usuarioEncontrado;
        }
    }
}
