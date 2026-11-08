using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Core.Servicios
{
    public class ServicioMateriales
    {
        private readonly IMaterialesRepositorio _repositorio;

        public ServicioMateriales(IMaterialesRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Materiales>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<Materiales> ObtenerPorIdAsync(int id)
        {
            var material = await _repositorio.ObtenerPorIdAsync(id);
            if (material == null)
            {
                throw new KeyNotFoundException($"No se encontró el material con ID {id}.");
            }
            return material;
        }

        public async Task<int> CrearAsync(Materiales material)
        {
            if (string.IsNullOrWhiteSpace(material.nombre))
            {
                throw new ArgumentException("El nombre del material es obligatorio.");
            }

            return await _repositorio.CrearAsync(material);
        }

        public async Task<bool> ActualizarAsync(Materiales material)
        {
            var existe = await _repositorio.ObtenerPorIdAsync(material.id_material);
            if (existe == null)
            {
                throw new KeyNotFoundException($"No se encontró el material con ID {material.id_material}.");
            }

            return await _repositorio.ActualizarAsync(material);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var existe = await _repositorio.ObtenerPorIdAsync(id);
            if (existe == null)
            {
                throw new KeyNotFoundException($"No se encontró el material con ID {id}.");
            }

            return await _repositorio.EliminarAsync(id);
        }
    }
}
