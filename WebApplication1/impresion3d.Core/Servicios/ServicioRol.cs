using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Core.Servicios
{
    public class ServicioRol
    {
        private readonly IRolRepositorio _repositorio;

        public ServicioRol(IRolRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Roles>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }
    }
} 