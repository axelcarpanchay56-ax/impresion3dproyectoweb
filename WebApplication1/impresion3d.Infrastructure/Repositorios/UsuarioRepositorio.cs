using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using MySqlConnector;
using System.Data;

namespace impresion3d.Infrastructure.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly Imiconexion _conexion;

        public UsuarioRepositorio(Imiconexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<Usuarios?> LoginAsync(
            string usuario,
            string passwordHash)
        {
            using var conexion =
        (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_usuarios_login",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("p_usuario", usuario);
            comando.Parameters.AddWithValue("p_password_hash", passwordHash);

            using var reader = await comando.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                Usuarios usuarioEncontrado = new Usuarios
                {
                    IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                    id_persona = Convert.ToInt32(reader["id_persona"]),
                    usuario = reader["usuario"].ToString(),
                    password_hash = reader["password_hash"].ToString(),
                    activo = Convert.ToBoolean(reader["activo"]),
                    fecha_creacion = Convert.ToDateTime(reader["fecha_creacion"]),

                    ultimo_acceso = reader["ultimo_acceso"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["ultimo_acceso"])
                };

                return usuarioEncontrado;
            }

            return null; 
        }

        public async Task<int> CrearAsync(Usuarios usuario)
        {
            using var conexion =
       (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_usuarios_crear",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_persona",
                usuario.id_persona
            );

            comando.Parameters.AddWithValue(
                "p_usuario",
                usuario.usuario
            );

            comando.Parameters.AddWithValue(
                "p_password_hash",
                usuario.password_hash
            );

            comando.Parameters.AddWithValue(
                "p_activo",
                usuario.activo
            );

            comando.Parameters.AddWithValue(
                "p_fecha_creacion",
                usuario.fecha_creacion
            );
            comando.Parameters.AddWithValue(
                "p_ultimo_acceso",
                usuario.ultimo_acceso
);

            int filasAfectadas =
                await comando.ExecuteNonQueryAsync();

            return filasAfectadas;
        }

        public async Task<bool> CambiarEstadoAsync(
            int idUsuario,
            bool activo)
        {
            using var conexion =
       (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_usuarios_cambiar_estado",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_usuario",
                idUsuario
            );

            comando.Parameters.AddWithValue(
                "p_activo",
                activo
            );

            int filasAfectadas =
                await comando.ExecuteNonQueryAsync();

            return filasAfectadas > 0; 
        }
    }
}
