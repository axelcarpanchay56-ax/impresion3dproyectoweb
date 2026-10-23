using impresion3d.Core.Entidades;

namespace impresion3d.Core.Interfaces
{
    public interface IProductoRepositorio
    {
        Task<List<Productos>> ObtenerTodosAsync();

        Task<Productos?> ObtenerPorIdAsync(int id);

        Task<int> CrearAsync(Productos producto);

        Task<bool> ActualizarAsync(Productos producto);

        Task<bool> CambiarEstadoAsync(int id, bool activo);
    }
} 