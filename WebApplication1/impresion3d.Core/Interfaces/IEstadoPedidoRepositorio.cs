using System.Collections.Generic;
using System.Threading.Tasks;
using impresion3d.Core.Entidades;

namespace impresion3d.Core.Interfaces
{
    public interface IEstadoPedidoRepositorio
    {
        Task<List<EstadoPedido>> ObtenerTodosAsync();
        Task<EstadoPedido?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(EstadoPedido entidad);
        Task<bool> ActualizarAsync(EstadoPedido entidad);
        Task<bool> EliminarAsync(int id);
    }
}
