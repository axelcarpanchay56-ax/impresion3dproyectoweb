using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using System.Data;
using Dapper;

namespace impresion3d.Infrastructure.Repositorios
{
    public class UsuarioRolRepositorio : IUsuarioRolRepositorio
    {
        private readonly IMiConexion _conexion;

        public UsuarioRolRepositorio(IMiConexion conexion)
        {
            _conexion = conexion;

            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<int> AsignarAsync(Usuario_Rol usuarioRol)
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add("p_id_usuario", usuarioRol.id_usuario);
            parametros.Add("p_id_rol", usuarioRol.id_rol);

            int filasAfectadas =
                await conexion.ExecuteAsync(
                    "sp_usuario_rol_asignar",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return filasAfectadas;
        }

        public async Task<List<Usuario_Rol>> ObtenerPorUsuarioAsync(int idUsuario)
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add(
                "p_id_usuario",
                idUsuario,
                DbType.Int32,
                ParameterDirection.Input
            );

            var rolesUsuario =
                await conexion.QueryAsync<Usuario_Rol>(
                    "sp_usuario_rol_obtener_por_usuario",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return rolesUsuario.ToList();
        }
    }
}
