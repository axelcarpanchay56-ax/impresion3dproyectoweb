using System.Collections.Generic;
using System.Threading.Tasks;
using impresion3d.Core.Entidades;

namespace impresion3d.Core.Interfaces
{
    public interface IMaterialesRepositorio
    {
        Task<List<Materiales>> ObtenerTodosAsync();
        Task<Materiales?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(Materiales entidad);
        Task<bool> ActualizarAsync(Materiales entidad);
        Task<bool> EliminarAsync(int id);
    }
}
