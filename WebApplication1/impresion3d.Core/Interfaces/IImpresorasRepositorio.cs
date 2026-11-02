using impresion3d.Core.Entidades;

namespace impresion3d.Core.Interfaces
{
    public interface IImpresorasRepositorio
    {
        Task<List<Impresoras>> ObtenerTodosAsync();
        Task<Impresoras?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(Impresoras entidad);
        Task<bool> ActualizarAsync(Impresoras entidad);
        Task<bool> CambiarEstadoAsync(int id, bool estado);
    }
}