using System.Collections.Generic;
using System.Threading.Tasks;
using impresion3d.Core.Entidades;

namespace impresion3d.Core.Interfaces
{
    public interface IImpresoraMaterialRepositorio
    {
        Task<List<ImpresoraMaterial>> ObtenerTodosAsync();
        Task<ImpresoraMaterial?> ObtenerPorIdAsync(int id_impresora, int id_material);
        Task<List<ImpresoraMaterial>> ObtenerPorImpresoraAsync(int id_impresora);
        Task<bool> CrearAsync(ImpresoraMaterial entidad);
        Task<bool> ActualizarAsync(ImpresoraMaterial entidad);
        Task<bool> EliminarAsync(int id_impresora, int id_material);
    }
}
