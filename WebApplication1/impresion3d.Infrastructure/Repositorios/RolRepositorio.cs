using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using System.Data;
using Dapper;

namespace impresion3d.Infrastructure.Repositorios
{
    public class RolRepositorio : IRolRepositorio
    {
        private readonly IMiConexion _conexion;

        public RolRepositorio(IMiConexion conexion)
        {
            _conexion = conexion;

            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<List<Roles>> ObtenerTodosAsync()
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var roles =
                await conexion.QueryAsync<Roles>(
                    "sp_roles_obtener_todos",
                    commandType: CommandType.StoredProcedure
                );

            return roles.ToList();
        }
    }
}
