using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Core.Servicios
{
    public class ServicioProducto
    {
        private readonly IProductoRepositorio _repositorio;

        public ServicioProducto(IProductoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Productos>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<Productos?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "El ID del producto debe ser mayor a 0"
                );
            }

            return await _repositorio.ObtenerPorIdAsync(id);
        }

        public async Task<int> CrearAsync(Productos producto)
        {
            if (string.IsNullOrWhiteSpace(producto.nombre))
            {
                throw new ArgumentException(
                    "El nombre del producto no puede estar vacío"
                );
            }

            return await _repositorio.CrearAsync(producto);
        }

        public async Task<bool> ActualizarAsync(Productos producto)
        {
            if (producto.IdProducto <= 0)
            {
                throw new ArgumentException(
                    "El ID del producto debe ser mayor a 0"
                );
            }

            if (string.IsNullOrWhiteSpace(producto.nombre))
            {
                throw new ArgumentException(
                    "El nombre del producto no puede estar vacío"
                );
            }

            return await _repositorio.ActualizarAsync(producto);
        }

        public async Task<bool> CambiarEstadoAsync(
            int id,
            bool activo)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "El ID del producto debe ser mayor a 0"
                );
            }

            return await _repositorio.CambiarEstadoAsync(
                id,
                activo
            );
        }
    }
} 