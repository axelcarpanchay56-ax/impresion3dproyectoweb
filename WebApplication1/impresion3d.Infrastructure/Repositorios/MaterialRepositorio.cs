using System.Data;
using MySqlConnector;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Infrastructure.Repositorios
{
    public class MaterialRepositorio : IMaterialRepositorio
    {
        private readonly Imiconexion _conexion;

        public MaterialRepositorio(Imiconexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<List<Materiales>> ObtenerTodosAsync()
        {
            var materiales = new List<Materiales>();

            using var conexion =
                (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_materiales_obtener_todos",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            using var reader = await comando.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var material = new Materiales
                {
                    IdMaterial = Convert.ToInt32(reader["id_material"]),
                    nombre = reader["nombre"].ToString(),
                    tipo = reader["tipo"] == DBNull.Value
                        ? null
                        : reader["tipo"].ToString(),
                    unidad_medida = reader["unidad_medida"] == DBNull.Value
                        ? null
                        : reader["unidad_medida"].ToString(),
                    color = reader["color"] == DBNull.Value
                        ? null
                        : reader["color"].ToString(),
                    diametro = reader["diametro"] == DBNull.Value
                        ? 0
                        : Convert.ToDecimal(reader["diametro"]),
                    activo = Convert.ToBoolean(reader["activo"])
                };

                materiales.Add(material);
            }

            return materiales; 
        }

        public async Task<Materiales?> ObtenerPorIdAsync(int id)
        {
            using var conexion =
        (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_materiales_obtener_por_id",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_material",
                id
            );

            using var reader = await comando.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var material = new Materiales
                {
                    IdMaterial = Convert.ToInt32(reader["id_material"]),
                    nombre = reader["nombre"].ToString(),
                    tipo = reader["tipo"] == DBNull.Value
                        ? null
                        : reader["tipo"].ToString(),
                    unidad_medida = reader["unidad_medida"] == DBNull.Value
                        ? null
                        : reader["unidad_medida"].ToString(),
                    color = reader["color"] == DBNull.Value
                        ? null
                        : reader["color"].ToString(),
                    diametro = reader["diametro"] == DBNull.Value
                        ? 0
                        : Convert.ToDecimal(reader["diametro"]),
                    activo = Convert.ToBoolean(reader["activo"])
                };

                return material;
            }

            return null; 
        }

        public async Task<int> CrearAsync(Materiales material)
        {
            using var conexion =
         (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_materiales_crear",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_nombre",
                material.nombre
            );

            comando.Parameters.AddWithValue(
                "p_tipo",
                material.tipo
            );

            comando.Parameters.AddWithValue(
                "p_unidad_medida",
                material.unidad_medida
            );

            comando.Parameters.AddWithValue(
                "p_color",
                material.color
            );

            comando.Parameters.AddWithValue(
                "p_diametro",
                material.diametro
            );

            comando.Parameters.AddWithValue(
                "p_activo",
                material.activo
            );

            int filasAfectadas =
                await comando.ExecuteNonQueryAsync();

            return filasAfectadas; 
        }

        public async Task<bool> ActualizarAsync(Materiales material)
        {
            using var conexion =
       (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_materiales_actualizar",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_material",
                material.IdMaterial
            );

            comando.Parameters.AddWithValue(
                "p_nombre",
                material.nombre
            );

            comando.Parameters.AddWithValue(
                "p_tipo",
                material.tipo
            );

            comando.Parameters.AddWithValue(
                "p_unidad_medida",
                material.unidad_medida
            );

            comando.Parameters.AddWithValue(
                "p_color",
                material.color
            );

            comando.Parameters.AddWithValue(
                "p_diametro",
                material.diametro
            );

            comando.Parameters.AddWithValue(
                "p_activo",
                material.activo
            );

            int filasAfectadas =
                await comando.ExecuteNonQueryAsync();

            return filasAfectadas > 0; 
        }

        public async Task<bool> CambiarEstadoAsync(int id, bool activo)
        {
            using var conexion =
        (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_materiales_cambiar_estado",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_material",
                id
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