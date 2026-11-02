using impresion3d.Core.Entidades;

namespace impresion3d.Core.Interfaces
{
    public interface IEstadoImpresionRepositorio
    {
        Task<List<EstadoImpresion>> ObtenerTodosAsync();
        Task<EstadoImpresion?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(EstadoImpresion entidad);
        Task<bool> ActualizarAsync(EstadoImpresion entidad);
        Task<bool> EliminarAsync(int id);
    }
}
