using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Core.Servicios
{
    public class ServicioMovimientoMaterial
    {
        private readonly IMovimientoMaterialRepositorio _repositorio;

        public ServicioMovimientoMaterial(
            IMovimientoMaterialRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<MovimientoMaterial>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<MovimientoMaterial?> ObtenerPorIdAsync(int id)
        {
            return await _repositorio.ObtenerPorIdAsync(id);
        }

        public async Task<int> CrearAsync(
            MovimientoMaterial movimientoMaterial)
        {
            return await _repositorio.CrearAsync(movimientoMaterial);
        }

        public async Task<bool> ActualizarAsync(
            MovimientoMaterial movimientoMaterial)
        {
            return await _repositorio.ActualizarAsync(movimientoMaterial);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _repositorio.EliminarAsync(id);
        }
    }
} 