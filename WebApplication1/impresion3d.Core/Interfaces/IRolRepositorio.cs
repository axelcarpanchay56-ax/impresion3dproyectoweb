using impresion3d.Core.Entidades;

namespace impresion3d.Core.Interfaces
{
    public interface IRolRepositorio
    {
        Task<List<Roles>> ObtenerTodosAsync();
    }
} 