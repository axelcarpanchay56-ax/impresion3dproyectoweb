using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Core.Servicios
{
    public class ServicioPersona
    {
        private readonly IPersonaRepositorio _repositorio;

        public ServicioPersona(IPersonaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Persona>> AllAsync()
        {
            return await _repositorio.ObtenerTodasAsync();
        }

        public async Task<Persona?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "El ID debe ser mayor a 0"
                );
            }

            return await _repositorio.ObtenerPorIdAsync(id);
        }

        public async Task<Persona?> GetByDniAsync(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
            {
                throw new ArgumentException(
                    "El DNI no puede estar vacío"
                );
            }

            return await _repositorio.ObtenerPorDniAsync(dni);
        }
        public async Task<int> CrearAsync(Persona persona)
        {
            if (string.IsNullOrWhiteSpace(persona.Dni))
            {
                throw new ArgumentException(
                    "El DNI no puede estar vacío"
                );
            }

            if (string.IsNullOrWhiteSpace(persona.Nombre))
            {
                throw new ArgumentException(
                    "El nombre no puede estar vacío"
                );
            }

            if (string.IsNullOrWhiteSpace(persona.Apellido))
            {
                throw new ArgumentException(
                    "El apellido no puede estar vacío"
                );
            }

            return await _repositorio.CrearAsync(persona);
        }

        public async Task<bool> ActualizarAsync(Persona persona)
        {
            if (persona.IdPersona <= 0)
            {
                throw new ArgumentException(
                    "El ID de la persona debe ser mayor a 0"
                );
            }

            if (string.IsNullOrWhiteSpace(persona.Dni))
            {
                throw new ArgumentException(
                    "El DNI no puede estar vacío"
                );
            }

            if (string.IsNullOrWhiteSpace(persona.Nombre))
            {
                throw new ArgumentException(
                    "El nombre no puede estar vacío"
                );
            }

            if (string.IsNullOrWhiteSpace(persona.Apellido))
            {
                throw new ArgumentException(
                    "El apellido no puede estar vacío"
                );
            }

            return await _repositorio.ActualizarAsync(persona);
        }

        public async Task<bool> CambiarEstadoAsync(int id, bool activo)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "El ID debe ser mayor a 0"
                );
            }

            return await _repositorio.CambiarEstadoAsync(id, activo);
        } 
    }
} 