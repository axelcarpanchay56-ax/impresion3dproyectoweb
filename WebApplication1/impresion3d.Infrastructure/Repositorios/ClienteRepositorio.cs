using MySqlConnector;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using System.Data;

namespace impresion3d.Infrastructure.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly Imiconexion _conexion;

        public ClienteRepositorio(Imiconexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<List<Clientes>> ObtenerTodosAsync()
        {
            var clientes = new List<Clientes>();

            using var conexion =
                (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_clientes_obtener_todos",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            using var reader = await comando.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                Clientes cliente = new Clientes
                {
                    IdCliente = Convert.ToInt32(reader["id_cliente"]),
                    id_persona = Convert.ToInt32(reader["id_persona"])
                };

                clientes.Add(cliente);
            }

            return clientes;
        }

        public async Task<int> CrearAsync(Clientes cliente)
        {
            using var conexion =
        (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_clientes_crear",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_persona",
                cliente.id_persona
            );

            int filasAfectadas = await comando.ExecuteNonQueryAsync();

            return filasAfectadas; 
        }
    }
}
