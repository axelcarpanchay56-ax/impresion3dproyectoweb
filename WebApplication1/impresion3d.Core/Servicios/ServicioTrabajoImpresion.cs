using System.Collections.Generic;
using System.Threading.Tasks;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Core.Servicios
{
    public class ServicioTrabajoImpresion
    {
        private readonly ITrabajoImpresionRepositorio _repositorio;

        public ServicioTrabajoImpresion(ITrabajoImpresionRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<TrabajoImpresion>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<TrabajoImpresion> ObtenerPorIdAsync(int id)
        {
            var trabajo = await _repositorio.ObtenerPorIdAsync(id);
            if (trabajo == null)
            {
                throw new KeyNotFoundException($"No se encontró el trabajo de impresión con ID {id}.");
            }
            return trabajo;
        }

        public async Task<int> CrearAsync(TrabajoImpresion trabajo)
        {
            if (trabajo.id_pedido <= 0)
                throw new ArgumentException("ID de pedido inválido.");
            if (trabajo.id_producto <= 0)
                throw new ArgumentException("ID de producto inválido.");
            if (trabajo.id_impresora <= 0)
                throw new ArgumentException("ID de impresora inválido.");
            if (trabajo.id_modelo <= 0)
                throw new ArgumentException("ID de modelo 3D inválido.");
            if (trabajo.id_material <= 0)
                throw new ArgumentException("ID de material inválido.");
            if (trabajo.cantidad <= 0)
                throw new ArgumentException("Cantidad inválida.");
            if (trabajo.id_estado <= 0)
                throw new ArgumentException("ID de estado de impresión inválido.");

            return await _repositorio.CrearAsync(trabajo);
        }

        public async Task<bool> ActualizarAsync(TrabajoImpresion trabajo)
        {
            var existe = await _repositorio.ObtenerPorIdAsync(trabajo.id_trabajo_impresion);
            if (existe == null)
            {
                throw new KeyNotFoundException($"No se encontró el trabajo de impresión con ID {trabajo.id_trabajo_impresion}.");
            }

            return await _repositorio.ActualizarAsync(trabajo);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var existe = await _repositorio.ObtenerPorIdAsync(id);
            if (existe == null)
            {
                throw new KeyNotFoundException($"No se encontró el trabajo de impresión con ID {id}.");
            }

            return await _repositorio.EliminarAsync(id);
        }
    }
}
