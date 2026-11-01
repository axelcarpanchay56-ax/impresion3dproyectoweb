using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Core.Servicios
{
    public class ServicioProductoMaterial
    {
        private readonly IProductoMaterialRepositorio _repositorio;

        public ServicioProductoMaterial(
            IProductoMaterialRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<ProductoMaterial>> ObtenerPorProductoAsync(
            int idProducto)
        {
            return await _repositorio.ObtenerPorProductoAsync(idProducto);
        }

        public async Task<int> AsignarAsync(
            ProductoMaterial productoMaterial)
        {
            return await _repositorio.AsignarAsync(productoMaterial);
        }

        public async Task<bool> ActualizarAsync(
            ProductoMaterial productoMaterial)
        {
            return await _repositorio.ActualizarAsync(productoMaterial);
        }

        public async Task<bool> EliminarAsync(
            int idProducto,
            int idMaterial)
        {
            return await _repositorio.EliminarAsync(
                idProducto,
                idMaterial
            );
        }
    }
} 