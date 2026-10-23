using impresion3d.Core.Entidades;

namespace impresion3d.Core.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<Usuarios?> LoginAsync(string usuario, string passwordHash);

        Task<int> CrearAsync(Usuarios usuario);

        Task<bool> CambiarEstadoAsync(int idUsuario, bool activo);
    }
}
