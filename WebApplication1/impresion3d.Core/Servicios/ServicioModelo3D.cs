using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Core.Servicios
{
    public class ServicioModelo3D
    {
        private readonly IModelo3DRepositorio _repositorio;

        public ServicioModelo3D(IModelo3DRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Modelo3D>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<Modelo3D?> ObtenerPorIdAsync(int id)
        {
            return await _repositorio.ObtenerPorIdAsync(id);
        }

        public async Task<int> CrearAsync(Modelo3D modelo3D)
        {
            return await _repositorio.CrearAsync(modelo3D);
        }

        public async Task<bool> ActualizarAsync(Modelo3D modelo3D)
        {
            return await _repositorio.ActualizarAsync(modelo3D);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _repositorio.EliminarAsync(id);
        }
    }
} 
