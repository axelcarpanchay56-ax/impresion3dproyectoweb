using impresion3d.Core.Entidades;

namespace impresion3d.Core.Interfaces
{
    public interface IModelo3DRepositorio
    {
        Task<List<Modelo3D>> ObtenerTodosAsync();

        Task<Modelo3D?> ObtenerPorIdAsync(int id);

        Task<int> CrearAsync(Modelo3D modelo3D);

        Task<bool> ActualizarAsync(Modelo3D modelo3D);

        Task<bool> EliminarAsync(int id);
    }
} 
