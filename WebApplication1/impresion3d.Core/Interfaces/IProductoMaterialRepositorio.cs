using impresion3d.Core.Entidades;

namespace impresion3d.Core.Interfaces
{
    public interface IProductoMaterialRepositorio
    {
        Task<List<ProductoMaterial>> ObtenerPorProductoAsync(int idProducto);

        Task<int> AsignarAsync(ProductoMaterial productoMaterial);

        Task<bool> ActualizarAsync(ProductoMaterial productoMaterial);

        Task<bool> EliminarAsync(int idProducto, int idMaterial);
    }
} 
