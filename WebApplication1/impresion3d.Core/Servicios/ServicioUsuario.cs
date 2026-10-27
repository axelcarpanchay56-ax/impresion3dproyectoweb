using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Core.Servicios
{
    public class ServicioUsuario
    {
        private readonly IUsuarioRepositorio _repositorio;

        public ServicioUsuario(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Usuarios?> LoginAsync(
            string usuario,
            string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(usuario))
            {
                throw new ArgumentException(
                    "El usuario no puede estar vacío"
                );
            }

            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentException(
                    "La contraseña no puede estar vacía"
                );
            }

            return await _repositorio.LoginAsync(
                usuario,
                passwordHash
            );
        }

        public async Task<int> CrearAsync(Usuarios usuario)
        {
            if (usuario.id_persona <= 0)
            {
                throw new ArgumentException(
                    "El ID de la persona debe ser mayor a 0"
                );
            }

            if (string.IsNullOrWhiteSpace(usuario.usuario))
            {
                throw new ArgumentException(
                    "El nombre de usuario no puede estar vacío"
                );
            }

            if (string.IsNullOrWhiteSpace(usuario.password_hash))
            {
                throw new ArgumentException(
                    "La contraseña no puede estar vacía"
                );
            }

            return await _repositorio.CrearAsync(usuario);
        }

        public async Task<bool> CambiarEstadoAsync(
            int idUsuario,
            bool activo)
        {
            if (idUsuario <= 0)
            {
                throw new ArgumentException(
                    "El ID del usuario debe ser mayor a 0"
                );
            }

            return await _repositorio.CambiarEstadoAsync(
                idUsuario,
                activo
            );
        }
    }
} 