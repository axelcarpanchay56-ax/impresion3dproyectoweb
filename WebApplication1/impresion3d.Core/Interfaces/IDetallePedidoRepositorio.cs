using System.Collections.Generic;
using System.Threading.Tasks;
using impresion3d.Core.Entidades;

namespace impresion3d.Core.Interfaces
{
    public interface IDetallePedidoRepositorio
    {
        Task<List<DetallePedido>> ObtenerTodosAsync();
        Task<DetallePedido?> ObtenerPorIdAsync(int id);
        Task<List<DetallePedido>> ObtenerPorPedidoAsync(int id_pedido);
        Task<int> CrearAsync(DetallePedido entidad);
        Task<bool> ActualizarAsync(DetallePedido entidad);
        Task<bool> EliminarAsync(int id);
    }
}
