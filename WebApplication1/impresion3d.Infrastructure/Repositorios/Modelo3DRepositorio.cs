using System.Data;
using MySqlConnector;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Infrastructure.Repositorios
{
    public class Modelo3DRepositorio : IModelo3DRepositorio
    {
        private readonly Imiconexion _conexion;

        public Modelo3DRepositorio(Imiconexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<List<Modelo3D>> ObtenerTodosAsync()
        {
            var lista = new List<Modelo3D>();

            using var conexion =
                (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_modelo3d_obtener_todos",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            using var reader = await comando.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var modelo = new Modelo3D
                {
                    IdModelo3d =
                        Convert.ToInt32(reader["id_modelo3d"]),

                    nombre =
                        reader["nombre"].ToString(),

                    archivo =
                        reader["archivo"] == DBNull.Value
                            ? null
                            : reader["archivo"].ToString(),

                    formato =
                        reader["formato"] == DBNull.Value
                            ? null
                            : reader["formato"].ToString(),

                    descripcion =
                        reader["descripcion"] == DBNull.Value
                            ? null
                            : reader["descripcion"].ToString(),

                    fecha_creacion =
                        Convert.ToDateTime(reader["fecha_creacion"])
                };

                lista.Add(modelo);
            }

            return lista; 
        }

        public async Task<Modelo3D?> ObtenerPorIdAsync(int id)
        {
            using var conexion =
        (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_modelo3d_obtener_por_id",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_modelo3d",
                id
            );

            using var reader = await comando.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Modelo3D
                {
                    IdModelo3d =
                        Convert.ToInt32(reader["id_modelo3d"]),

                    nombre =
                        reader["nombre"].ToString(),

                    archivo =
                        reader["archivo"] == DBNull.Value
                            ? null
                            : reader["archivo"].ToString(),

                    formato =
                        reader["formato"] == DBNull.Value
                            ? null
                            : reader["formato"].ToString(),

                    descripcion =
                        reader["descripcion"] == DBNull.Value
                            ? null
                            : reader["descripcion"].ToString(),

                    fecha_creacion =
                        Convert.ToDateTime(reader["fecha_creacion"])
                };
            }

            return null; 
        }

        public async Task<int> CrearAsync(Modelo3D modelo3D)
        {
            using var conexion =
        (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_modelo3d_crear",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_nombre",
                modelo3D.nombre
            );

            comando.Parameters.AddWithValue(
                "p_archivo",
                modelo3D.archivo
            );

            comando.Parameters.AddWithValue(
                "p_formato",
                modelo3D.formato
            );

            comando.Parameters.AddWithValue(
                "p_descripcion",
                modelo3D.descripcion
            );

            comando.Parameters.AddWithValue(
                "p_fecha_creacion",
                modelo3D.fecha_creacion
            );

            object? resultado =
                await comando.ExecuteScalarAsync();

            return Convert.ToInt32(resultado);
 
        }

        public async Task<bool> ActualizarAsync(Modelo3D modelo3D)
        {
            using var conexion =
        (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_modelo3d_actualizar",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_modelo3d",
                modelo3D.IdModelo3d
            );

            comando.Parameters.AddWithValue(
                "p_nombre",
                modelo3D.nombre
            );

            comando.Parameters.AddWithValue(
                "p_archivo",
                modelo3D.archivo
            );

            comando.Parameters.AddWithValue(
                "p_formato",
                modelo3D.formato
            );

            comando.Parameters.AddWithValue(
                "p_descripcion",
                modelo3D.descripcion
            );

            comando.Parameters.AddWithValue(
                "p_fecha_creacion",
                modelo3D.fecha_creacion
            );

            int filasAfectadas =
                await comando.ExecuteNonQueryAsync();

            return filasAfectadas > 0; 
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var conexion =
       (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_modelo3d_eliminar",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_modelo3d",
                id
            );

            int filasAfectadas =
                await comando.ExecuteNonQueryAsync();

            return filasAfectadas > 0; 
        }
    }
} 
