using System.Data;
using MySqlConnector;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Infrastructure.Repositorios
{
    public class UsuarioRolRepositorio : IUsuarioRolRepositorio
    {
        private readonly Imiconexion _conexion;

        public UsuarioRolRepositorio(Imiconexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<int> AsignarAsync(Usuario_Rol usuarioRol)
        {
            using var conexion =
        (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_usuario_rol_asignar",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_usuario",
                usuarioRol.id_usuario
            );

            comando.Parameters.AddWithValue(
                "p_id_rol",
                usuarioRol.id_rol
            );

            int filasAfectadas =
                await comando.ExecuteNonQueryAsync();

            return filasAfectadas;
        }

        public async Task<List<Usuario_Rol>> ObtenerPorUsuarioAsync(int idUsuario)
        {
            var rolesUsuario = new List<Usuario_Rol>();

            using var conexion =
                (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_usuario_rol_obtener_por_usuario",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_usuario",
                idUsuario
            );

            using var reader = await comando.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var usuarioRol = new Usuario_Rol
                {
                    id_usuario = Convert.ToInt32(reader["id_usuario"]),
                    id_rol = Convert.ToInt32(reader["id_rol"])
                };

                rolesUsuario.Add(usuarioRol);
            }

            return rolesUsuario; 
        }
    }
} 
