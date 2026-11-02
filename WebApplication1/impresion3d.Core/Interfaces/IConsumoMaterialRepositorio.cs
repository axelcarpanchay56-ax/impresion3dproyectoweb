using System.Collections.Generic;
using System.Threading.Tasks;
using impresion3d.Core.Entidades;

namespace impresion3d.Core.Interfaces
{
    public interface IConsumoMaterialRepositorio
    {
        Task<List<ConsumoMaterial>> ObtenerTodosAsync();
        Task<ConsumoMaterial?> ObtenerPorIdAsync(int id);
        Task<List<ConsumoMaterial>> ObtenerPorTrabajoAsync(int id_trabajo);
        Task<int> CrearAsync(ConsumoMaterial entidad);
        Task<bool> ActualizarAsync(ConsumoMaterial entidad);
        Task<bool> EliminarAsync(int id);
    }
}
