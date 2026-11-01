using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Core.Servicios
{
    public class ServicioUsuarioRol
    {
        private readonly IUsuarioRolRepositorio _repositorio;

        public ServicioUsuarioRol(IUsuarioRolRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<int> AsignarAsync(Usuario_Rol usuarioRol)
        {
            if (usuarioRol.id_usuario <= 0)
            {
                throw new ArgumentException(
                    "El ID del usuario debe ser mayor a 0"
                );
            }

            if (usuarioRol.id_rol <= 0)
            {
                throw new ArgumentException(
                    "El ID del rol debe ser mayor a 0"
                );
            }

            return await _repositorio.AsignarAsync(usuarioRol);
        }

        public async Task<List<Usuario_Rol>> ObtenerPorUsuarioAsync(
            int idUsuario)
        {
            if (idUsuario <= 0)
            {
                throw new ArgumentException(
                    "El ID del usuario debe ser mayor a 0"
                );
            }

            return await _repositorio.ObtenerPorUsuarioAsync(idUsuario);
        }
    }
}