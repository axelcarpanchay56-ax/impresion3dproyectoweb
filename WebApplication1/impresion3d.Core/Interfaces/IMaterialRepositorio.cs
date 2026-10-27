using impresion3d.Core.Entidades;

namespace impresion3d.Core.Interfaces
{
    public interface IMaterialRepositorio
    {
        Task<List<Materiales>> ObtenerTodosAsync();

        Task<Materiales?> ObtenerPorIdAsync(int id);

        Task<int> CrearAsync(Materiales material);

        Task<bool> ActualizarAsync(Materiales material);

        Task<bool> CambiarEstadoAsync(int id, bool activo);
    }
} 