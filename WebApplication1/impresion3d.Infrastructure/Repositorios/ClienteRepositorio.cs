using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using System.Data;
using Dapper;

namespace impresion3d.Infrastructure.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly IMiConexion _conexion;

        public ClienteRepositorio(IMiConexion conexion)
        {
            _conexion = conexion;

            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<List<Clientes>> ObtenerTodosAsync()
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var clientes =
                await conexion.QueryAsync<Clientes>(
                    "sp_clientes_obtener_todos",
                    commandType: CommandType.StoredProcedure
                );

            return clientes.ToList();
        }

        public async Task<int> CrearAsync(Clientes cliente)
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add("p_id_persona", cliente.id_persona);

            int filasAfectadas =
                await conexion.ExecuteAsync(
                    "sp_clientes_crear",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return filasAfectadas;
        }
    }
}
