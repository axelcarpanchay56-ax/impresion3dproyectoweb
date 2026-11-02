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

        public async Task<Modelo3D> ObtenerPorIdAsync(int id)
        {
            var modelo = await _repositorio.ObtenerPorIdAsync(id);
            if (modelo == null)
            {
                throw new KeyNotFoundException($"No se encontró el modelo 3D con ID {id}.");
            }
            return modelo;
        }

        public async Task<int> CrearAsync(Modelo3D modelo)
        {
            if (string.IsNullOrWhiteSpace(modelo.nombre))
            {
                throw new ArgumentException("El nombre del modelo 3D es obligatorio.");
            }

            return await _repositorio.CrearAsync(modelo);
        }

        public async Task<bool> ActualizarAsync(Modelo3D modelo)
        {
            var existe = await _repositorio.ObtenerPorIdAsync(modelo.id_modelo3d);
            if (existe == null)
            {
                throw new KeyNotFoundException($"No se encontró el modelo 3D con ID {modelo.id_modelo3d}.");
            }

            return await _repositorio.ActualizarAsync(modelo);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var existe = await _repositorio.ObtenerPorIdAsync(id);
            if (existe == null)
            {
                throw new KeyNotFoundException($"No se encontró el modelo 3D con ID {id}.");
            }

            return await _repositorio.EliminarAsync(id);
        }
    }
}
