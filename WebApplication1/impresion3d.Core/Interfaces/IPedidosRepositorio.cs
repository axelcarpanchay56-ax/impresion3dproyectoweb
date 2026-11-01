using System.Collections.Generic;
using System.Threading.Tasks;
using impresion3d.Core.Entidades;

namespace impresion3d.Core.Interfaces
{
    public interface IPedidosRepositorio
    {
        Task<List<Pedidos>> ObtenerTodosAsync();
        Task<Pedidos?> ObtenerPorIdAsync(int id);
        Task<List<Pedidos>> ObtenerPorClienteAsync(int id_cliente);
        Task<int> CrearAsync(Pedidos entidad);
        Task<bool> ActualizarAsync(Pedidos entidad);
        Task<bool> EliminarAsync(int id);
    }
}
