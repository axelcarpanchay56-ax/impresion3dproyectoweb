using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Core.Servicios
{
    public class ServicioCliente
    {
        private readonly IClienteRepositorio _repositorio;

        public ServicioCliente(IClienteRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Clientes>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<int> CrearAsync(Clientes cliente)
        {
            if (cliente.id_persona <= 0)
            {
                throw new ArgumentException(
                    "El ID de la persona debe ser mayor a 0"
                );
            }

            return await _repositorio.CrearAsync(cliente);
        }
    }
} 