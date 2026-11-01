using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using System.Data;
using MySqlConnector;



namespace impresion3d.Infrastructure.Repositorios
{
    public class PersonaRepositorio : IPersonaRepositorio
    {
        private readonly Imiconexion _conexion;

        public PersonaRepositorio(Imiconexion conexion)
        {
            _conexion = conexion;
        } 
        public async Task<List<Persona>> ObtenerTodasAsync()
        {
            var personas = new List<Persona>();

            using var conexion =
                (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_personas_obtener_todas",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            using var reader = await comando.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                Persona persona = new Persona
                {
                    IdPersona = Convert.ToInt32(reader["id_persona"]),
                    Dni = reader["dni"].ToString(),
                    Nombre = reader["nombre"].ToString(),
                    Apellido = reader["apellido"].ToString(),
                    Telefono = reader["telefono"] == DBNull.Value
                        ? null
                        : reader["telefono"].ToString(),
                    Email = reader["email"] == DBNull.Value
                        ? null
                        : reader["email"].ToString(),
                    Direccion = reader["direccion"] == DBNull.Value
                        ? null
                        : reader["direccion"].ToString(),
                    Activo = Convert.ToBoolean(reader["activo"]),
                    FechaAlta = Convert.ToDateTime(reader["fecha_alta"])
                };

                personas.Add(persona);
            }

            return personas;
        }

        public async Task<Persona?> ObtenerPorIdAsync(int id)
        {
            using var conexion =
        (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_personas_obtener_por_id",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_id_persona",
                id
            );

            using var reader = await comando.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                Persona persona = new Persona
                {
                    IdPersona = Convert.ToInt32(reader["id_persona"]),
                    Dni = reader["dni"].ToString(),
                    Nombre = reader["nombre"].ToString(),
                    Apellido = reader["apellido"].ToString(),
                    Telefono = reader["telefono"] == DBNull.Value
                        ? null
                        : reader["telefono"].ToString(),
                    Email = reader["email"] == DBNull.Value
                        ? null
                        : reader["email"].ToString(),
                    Direccion = reader["direccion"] == DBNull.Value
                        ? null
                        : reader["direccion"].ToString(),
                    Activo = Convert.ToBoolean(reader["activo"]),
                    FechaAlta = Convert.ToDateTime(reader["fecha_alta"])
                };

                return persona;
            }

            return null;
        }

        public async Task<Persona?> ObtenerPorDniAsync(string dni)
        {
            using var conexion =
       (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_personas_obtener_por_dni",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "p_dni",
                dni
            );

            using var reader = await comando.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                Persona persona = new Persona
                {
                    IdPersona = Convert.ToInt32(reader["id_persona"]),
                    Dni = reader["dni"].ToString(),
                    Nombre = reader["nombre"].ToString(),
                    Apellido = reader["apellido"].ToString(),
                    Telefono = reader["telefono"] == DBNull.Value
                        ? null
                        : reader["telefono"].ToString(),
                    Email = reader["email"] == DBNull.Value
                        ? null
                        : reader["email"].ToString(),
                    Direccion = reader["direccion"] == DBNull.Value
                        ? null
                        : reader["direccion"].ToString(),
                    Activo = Convert.ToBoolean(reader["activo"]),
                    FechaAlta = Convert.ToDateTime(reader["fecha_alta"])
                };

                return persona;
            }

            return null; 
        }

        public async Task<int> CrearAsync(Persona persona)
        {
            using var conexion =
        (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_personas_crear",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("p_dni", persona.Dni);
            comando.Parameters.AddWithValue("p_nombre", persona.Nombre);
            comando.Parameters.AddWithValue("p_apellido", persona.Apellido);
            comando.Parameters.AddWithValue("p_telefono", persona.Telefono);
            comando.Parameters.AddWithValue("p_email", persona.Email);
            comando.Parameters.AddWithValue("p_direccion", persona.Direccion);
            comando.Parameters.AddWithValue("p_activo", persona.Activo);
            comando.Parameters.AddWithValue("p_fecha_alta", persona.FechaAlta);

            int filasAfectadas = await comando.ExecuteNonQueryAsync();

            return filasAfectadas;
        }

        public async Task<bool> ActualizarAsync(Persona persona)
        {
            using var conexion =
        (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_personas_actualizar",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("p_id_persona", persona.IdPersona);
            comando.Parameters.AddWithValue("p_dni", persona.Dni);
            comando.Parameters.AddWithValue("p_nombre", persona.Nombre);
            comando.Parameters.AddWithValue("p_apellido", persona.Apellido);
            comando.Parameters.AddWithValue("p_telefono", persona.Telefono);
            comando.Parameters.AddWithValue("p_email", persona.Email);
            comando.Parameters.AddWithValue("p_direccion", persona.Direccion);
            comando.Parameters.AddWithValue("p_activo", persona.Activo);
            comando.Parameters.AddWithValue("p_fecha_alta", persona.FechaAlta);

            int filasAfectadas = await comando.ExecuteNonQueryAsync();

            return filasAfectadas > 0; 
        }

        public async Task<bool> CambiarEstadoAsync(int id, bool activo)
        {
            using var conexion =
        (MySqlConnection)await _conexion.obtenerConexionAsync();

            using var comando = new MySqlCommand(
                "sp_personas_cambiar_estado",
                conexion
            );

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("p_id_persona", id);
            comando.Parameters.AddWithValue("p_activo", activo);

            int filasAfectadas = await comando.ExecuteNonQueryAsync();

            return filasAfectadas > 0; 
        }
    }
}
