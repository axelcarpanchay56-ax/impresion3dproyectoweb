using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Core.Servicios
{
    public class ServicioEstadoPedido
    {
        private readonly IEstadoPedidoRepositorio _repositorio;

        public ServicioEstadoPedido(IEstadoPedidoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<EstadoPedido>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<EstadoPedido> ObtenerPorIdAsync(int id)
        {
            var estado = await _repositorio.ObtenerPorIdAsync(id);
            if (estado == null)
            {
                throw new KeyNotFoundException($"No se encontró el estado de pedido con ID {id}.");
            }
            return estado;
        }

        public async Task<int> CrearAsync(EstadoPedido estado)
        {
            if (string.IsNullOrWhiteSpace(estado.nombre))
                throw new ArgumentException("El nombre del estado de pedido es obligatorio.");

            return await _repositorio.CrearAsync(estado);
        }

        public async Task<bool> ActualizarAsync(EstadoPedido estado)
        {
            var existe = await _repositorio.ObtenerPorIdAsync(estado.id_estado_pedido);
            if (existe == null)
            {
                throw new KeyNotFoundException($"No se encontró el estado de pedido con ID {estado.id_estado_pedido}.");
            }

            if (string.IsNullOrWhiteSpace(estado.nombre))
                throw new ArgumentException("El nombre del estado de pedido es obligatorio.");

            return await _repositorio.ActualizarAsync(estado);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var existe = await _repositorio.ObtenerPorIdAsync(id);
            if (existe == null)
            {
                throw new KeyNotFoundException($"No se encontró el estado de pedido con ID {id}.");
            }

            return await _repositorio.EliminarAsync(id);
        }
    }
}
