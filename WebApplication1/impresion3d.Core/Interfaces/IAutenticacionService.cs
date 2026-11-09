using impresion3d.Core.Entidades;

namespace impresion3d.Core.Interfaces
{
    public interface IAutenticacionService
    {
        Task<Usuarios?> ValidarCredencialesAsync(
            string usuario,
            string password
        );

        Task<List<string>> ObtenerRolesAsync(
            int idUsuario
        );

        string GenerarToken(
            Usuarios usuario,
            List<string> roles
        );
    }
} 