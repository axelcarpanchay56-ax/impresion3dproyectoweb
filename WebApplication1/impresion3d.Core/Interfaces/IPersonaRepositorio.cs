using impresion3d.Core.Entidades;

namespace impresion3d.Core.Interfaces
{
    public interface IPersonaRepositorio
    {
        Task<List<Persona>> ObtenerTodasAsync();

        Task<Persona?> ObtenerPorIdAsync(int id);

        Task<Persona?> ObtenerPorDniAsync(string dni);

        Task<bool> ExisteDniAsync(string dni);

        Task<int> CrearAsync(Persona persona);

        Task<bool> ActualizarAsync(Persona persona);

        Task<bool> CambiarEstadoAsync(int id, bool activo);

        Task<bool> ExisteEmailAsync(string email);

        Task<List<Persona>> BuscarAsync(string buscar);

        Task<bool> EliminarAsync(int id); 
    }
}
