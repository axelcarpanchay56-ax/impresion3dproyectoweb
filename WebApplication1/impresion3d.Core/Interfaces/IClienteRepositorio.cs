using impresion3d.Core.Entidades;

namespace impresion3d.Core.Interfaces
{
    public interface IClienteRepositorio
    {
        Task<List<Clientes>> ObtenerTodosAsync();

        Task<int> CrearAsync(Clientes cliente);
    }
} 