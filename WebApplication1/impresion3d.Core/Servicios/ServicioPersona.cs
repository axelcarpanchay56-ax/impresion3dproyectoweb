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
        public async Task<List<Persona>> BuscarAsync(string buscar)
        {
            if (string.IsNullOrWhiteSpace(buscar))
            {
                throw new ArgumentException(
                    "El término de búsqueda no puede estar vacío"
                );
            }

            if (buscar.Trim().Length < 2)
            {
                throw new ArgumentException(
                    "El término de búsqueda debe tener al menos 2 caracteres"
                );
            }

            return await _repositorio.BuscarAsync(
                buscar.Trim()
            );
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

            bool existeDni =
                await _repositorio.ExisteDniAsync(persona.Dni);

            if (existeDni)
            {
                throw new ArgumentException(
                    "Ya existe una persona con ese DNI"
                );
            }

            if (!string.IsNullOrWhiteSpace(persona.Email))
            {
                bool existeEmail =
                    await _repositorio.ExisteEmailAsync(persona.Email);

                if (existeEmail)
                {
                    throw new ArgumentException(
                        "Ya existe una persona con ese email"
                    );
                }
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
        public async Task<bool> EliminarAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "El ID debe ser mayor a 0"
                );
            }

            var persona =
                await _repositorio.ObtenerPorIdAsync(id);

            if (persona == null)
            {
                return false;
            }

            return await _repositorio.EliminarAsync(id);
        } 
    }
} 