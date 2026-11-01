using System.Data;
using MySqlConnector;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Infrastructure.Repositorios
{
    public class RolRepositorio : IRolRepositorio
    {
        private readonly Imiconexion _conexion;

        public RolRepositorio(Imiconexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<List<Roles>> ObtenerTodosAsync()
        {
            var roles = new List<Roles>();

            using var conexion =
                (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_roles_obtener_todos",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            using var reader = await comando.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                Roles rol = new Roles
                {
                    IdRol = Convert.ToInt32(reader["id_rol"]),
                    nombre = reader["nombre"].ToString(),
                    descripcion = reader["descripcion"] == DBNull.Value
                        ? null
                        : reader["descripcion"].ToString()
                };

                roles.Add(rol);
            }

            return roles;
        }
    }
}
