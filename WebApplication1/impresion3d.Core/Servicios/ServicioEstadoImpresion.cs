using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Core.Servicios
{
    public class ServicioEstadoImpresion
    {
        private readonly IEstadoImpresionRepositorio _repositorio;

        public ServicioEstadoImpresion(IEstadoImpresionRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<EstadoImpresion>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<EstadoImpresion> ObtenerPorIdAsync(int id)
        {
            var estado = await _repositorio.ObtenerPorIdAsync(id);
            if (estado == null)
            {
                throw new KeyNotFoundException($"No se encontró el estado de impresión con ID {id}.");
            }
            return estado;
        }

        public async Task<int> CrearAsync(EstadoImpresion estado)
        {
            if (string.IsNullOrWhiteSpace(estado.nombre))
            {
                throw new ArgumentException("El nombre del estado es obligatorio.");
            }

            return await _repositorio.CrearAsync(estado);
        }

        public async Task<bool> ActualizarAsync(EstadoImpresion estado)
        {
            var existe = await _repositorio.ObtenerPorIdAsync(estado.id_estado_impresion);
            if (existe == null)
            {
                throw new KeyNotFoundException($"No se encontró el estado de impresión con ID {estado.id_estado_impresion}.");
            }

            return await _repositorio.ActualizarAsync(estado);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var existe = await _repositorio.ObtenerPorIdAsync(id);
            if (existe == null)
            {
                throw new KeyNotFoundException($"No se encontró el estado de impresión con ID {id}.");
            }

            return await _repositorio.EliminarAsync(id);
        }
    }
}
