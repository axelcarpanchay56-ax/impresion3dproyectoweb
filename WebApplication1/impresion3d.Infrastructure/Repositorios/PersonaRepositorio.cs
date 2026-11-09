using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using System.Data;
using MySqlConnector;
using Dapper; 


namespace impresion3d.Infrastructure.Repositorios
{
    public class PersonaRepositorio : IPersonaRepositorio
    {
        private readonly IMiConexion _conexion;

        public PersonaRepositorio(IMiConexion conexion)
        {
            _conexion = conexion;

            DefaultTypeMap.MatchNamesWithUnderscores = true; 
        } 
        public async Task<List<Persona>> ObtenerTodasAsync()
        {

            {
                using var conexion =
                    await _conexion.ObtenerConexionAsync();

                var personas = await conexion.QueryAsync<Persona>(
                    "sp_personas_obtener_todas",
                    commandType: CommandType.StoredProcedure
                );

                return personas.ToList();
            }
        }

        public async Task<Persona?> ObtenerPorIdAsync(int id)
        {
            using var conexion =
        await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add(
                "p_id_persona",
                id,
                DbType.Int32,
                ParameterDirection.Input
            );

            var persona =
                await conexion.QueryFirstOrDefaultAsync<Persona>(
                    "sp_personas_obtener_por_id",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return persona; 

        }

        public async Task<Persona?> ObtenerPorDniAsync(string dni)
        {
            using var conexion =
         await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add(
                "p_dni",
                dni,
                DbType.String,
                ParameterDirection.Input
            );

            var persona =
                await conexion.QueryFirstOrDefaultAsync<Persona>(
                    "sp_personas_obtener_por_dni",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return persona; 
        }
        public async Task<bool> ExisteDniAsync(string dni)
        {
            using var conexion =
        await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add(
                "p_dni",
                dni,
                DbType.String,
                ParameterDirection.Input
            );

            var resultado =
                await conexion.ExecuteScalarAsync<bool>(
                    "sp_personas_existe_dni",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return resultado; 
        }
        public async Task<bool> ExisteEmailAsync(string email)
        {
            using var conexion =
       await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add(
                "p_email",
                email,
                DbType.String,
                ParameterDirection.Input
            );

            var resultado =
                await conexion.ExecuteScalarAsync<bool>(
                    "sp_personas_existe_email",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return resultado; 
        } 

        public async Task<int> CrearAsync(Persona persona)
        {
            using var conexion =
         await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add("p_dni", persona.Dni);
            parametros.Add("p_nombre", persona.Nombre);
            parametros.Add("p_apellido", persona.Apellido);
            parametros.Add("p_telefono", persona.Telefono);
            parametros.Add("p_email", persona.Email);
            parametros.Add("p_direccion", persona.Direccion);
            parametros.Add("p_activo", persona.Activo);
            parametros.Add("p_fecha_alta", persona.FechaAlta);

            int filasAfectadas =
                await conexion.ExecuteAsync(
                    "sp_personas_crear",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return filasAfectadas; 
        }
        public async Task<List<Persona>> BuscarAsync(string buscar)
        {
            using var conexion =
       await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add(
                "p_buscar",
                buscar,
                DbType.String,
                ParameterDirection.Input
            );

            var personas =
                await conexion.QueryAsync<Persona>(
                    "sp_personas_buscar",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return personas.ToList(); 
        }
        public async Task<bool> EliminarAsync(int id)
        {
            using var conexion =
       await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add(
                "p_id_persona",
                id,
                DbType.Int32,
                ParameterDirection.Input
            );

            int filasAfectadas =
                await conexion.ExecuteAsync(
                    "sp_personas_eliminar",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return filasAfectadas > 0; 
        } 
        public async Task<bool> ActualizarAsync(Persona persona)
        {
            using var conexion =
       await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add("p_id_persona", persona.IdPersona);
            parametros.Add("p_dni", persona.Dni);
            parametros.Add("p_nombre", persona.Nombre);
            parametros.Add("p_apellido", persona.Apellido);
            parametros.Add("p_telefono", persona.Telefono);
            parametros.Add("p_email", persona.Email);
            parametros.Add("p_direccion", persona.Direccion);
            parametros.Add("p_activo", persona.Activo);
            parametros.Add("p_fecha_alta", persona.FechaAlta);

            int filasAfectadas =
                await conexion.ExecuteAsync(
                    "sp_personas_actualizar",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return filasAfectadas > 0; 
        }

        public async Task<bool> CambiarEstadoAsync(int id, bool activo)
        {
            using var conexion =
       await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add("p_id_persona", id);
            parametros.Add("p_activo", activo);

            int filasAfectadas =
                await conexion.ExecuteAsync(
                    "sp_personas_cambiar_estado",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return filasAfectadas > 0; 
        }
    }
}
