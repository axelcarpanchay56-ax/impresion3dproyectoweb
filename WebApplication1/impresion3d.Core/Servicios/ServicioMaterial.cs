using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Core.Servicios
{
    public class ServicioMaterial
    {
        private readonly IMaterialRepositorio _repositorio;

        public ServicioMaterial(IMaterialRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Materiales>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<Materiales?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "El ID del material debe ser mayor a 0"
                );
            }

            return await _repositorio.ObtenerPorIdAsync(id);
        }

        public async Task<int> CrearAsync(Materiales material)
        {
            if (string.IsNullOrWhiteSpace(material.nombre))
            {
                throw new ArgumentException(
                    "El nombre del material no puede estar vacío"
                );
            }

            return await _repositorio.CrearAsync(material);
        }

        public async Task<bool> ActualizarAsync(Materiales material)
        {
            if (material.IdMaterial <= 0)
            {
                throw new ArgumentException(
                    "El ID del material debe ser mayor a 0"
                );
            }

            if (string.IsNullOrWhiteSpace(material.nombre))
            {
                throw new ArgumentException(
                    "El nombre del material no puede estar vacío"
                );
            }

            return await _repositorio.ActualizarAsync(material);
        }

        public async Task<bool> CambiarEstadoAsync(
            int id,
            bool activo)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "El ID del material debe ser mayor a 0"
                );
            }

            return await _repositorio.CambiarEstadoAsync(
                id,
                activo
            );
        }
    }
} 