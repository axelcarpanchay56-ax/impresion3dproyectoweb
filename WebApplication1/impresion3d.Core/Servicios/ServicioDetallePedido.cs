using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Core.Servicios
{
    public class ServicioDetallePedido
    {
        private readonly IDetallePedidoRepositorio _repositorio;

        public ServicioDetallePedido(IDetallePedidoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<DetallePedido>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<DetallePedido> ObtenerPorIdAsync(int id)
        {
            var detalle = await _repositorio.ObtenerPorIdAsync(id);
            if (detalle == null)
            {
                throw new KeyNotFoundException($"No se encontró el detalle de pedido con ID {id}.");
            }
            return detalle;
        }

        public async Task<List<DetallePedido>> ObtenerPorPedidoAsync(int id_pedido)
        {
            return await _repositorio.ObtenerPorPedidoAsync(id_pedido);
        }

        public async Task<int> CrearAsync(DetallePedido detalle)
        {
            if (detalle.id_pedido <= 0)
                throw new ArgumentException("El ID de pedido es inválido.");
            if (detalle.id_producto <= 0)
                throw new ArgumentException("El ID de producto es inválido.");
            if (detalle.cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero.");
            if (detalle.precio_unitario < 0)
                throw new ArgumentException("El precio unitario no puede ser negativo.");

            if (detalle.subtotal == 0 && detalle.precio_unitario > 0)
            {
                detalle.subtotal = detalle.cantidad * detalle.precio_unitario;
            }

            return await _repositorio.CrearAsync(detalle);
        }

        public async Task<bool> ActualizarAsync(DetallePedido detalle)
        {
            var existe = await _repositorio.ObtenerPorIdAsync(detalle.id_detalle_pedido);
            if (existe == null)
            {
                throw new KeyNotFoundException($"No se encontró el detalle de pedido con ID {detalle.id_detalle_pedido}.");
            }

            if (detalle.id_pedido <= 0)
                throw new ArgumentException("El ID de pedido es inválido.");
            if (detalle.id_producto <= 0)
                throw new ArgumentException("El ID de producto es inválido.");
            if (detalle.cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero.");
            if (detalle.precio_unitario < 0)
                throw new ArgumentException("El precio unitario no puede ser negativo.");

            if (detalle.subtotal == 0 && detalle.precio_unitario > 0)
            {
                detalle.subtotal = detalle.cantidad * detalle.precio_unitario;
            }

            return await _repositorio.ActualizarAsync(detalle);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var existe = await _repositorio.ObtenerPorIdAsync(id);
            if (existe == null)
            {
                throw new KeyNotFoundException($"No se encontró el detalle de pedido con ID {id}.");
            }

            return await _repositorio.EliminarAsync(id);
        }
    }
}
