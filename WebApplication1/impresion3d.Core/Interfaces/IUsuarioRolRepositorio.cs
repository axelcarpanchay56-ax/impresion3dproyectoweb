using impresion3d.Core.Entidades;

namespace impresion3d.Core.Interfaces
{
    public interface IUsuarioRolRepositorio
    {
        Task<int> AsignarAsync(Usuario_Rol usuarioRol);

        Task<List<Usuario_Rol>> ObtenerPorUsuarioAsync(int idUsuario);
    }
}
