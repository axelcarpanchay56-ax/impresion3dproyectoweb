using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Core.Servicios
{
    public class ServicioPedidos
    {
        private readonly IPedidosRepositorio _repositorio;

        public ServicioPedidos(IPedidosRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Pedidos>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<Pedidos> ObtenerPorIdAsync(int id)
        {
            var pedido = await _repositorio.ObtenerPorIdAsync(id);
            if (pedido == null)
            {
                throw new KeyNotFoundException($"No se encontró el pedido con ID {id}.");
            }
            return pedido;
        }

        public async Task<List<Pedidos>> ObtenerPorClienteAsync(int id_cliente)
        {
            return await _repositorio.ObtenerPorClienteAsync(id_cliente);
        }

        public async Task<int> CrearAsync(Pedidos pedido)
        {
            if (pedido.id_cliente <= 0)
                throw new ArgumentException("El ID de cliente es inválido.");
            if (pedido.id_estado <= 0)
                throw new ArgumentException("El ID de estado es inválido.");
            if (pedido.total < 0)
                throw new ArgumentException("El total no puede ser negativo.");

            if (pedido.fecha_hora == default)
            {
                pedido.fecha_hora = DateTime.Now;
            }

            return await _repositorio.CrearAsync(pedido);
        }

        public async Task<bool> ActualizarAsync(Pedidos pedido)
        {
            var existe = await _repositorio.ObtenerPorIdAsync(pedido.id_pedido);
            if (existe == null)
            {
                throw new KeyNotFoundException($"No se encontró el pedido con ID {pedido.id_pedido}.");
            }

            if (pedido.id_cliente <= 0)
                throw new ArgumentException("El ID de cliente es inválido.");
            if (pedido.id_estado <= 0)
                throw new ArgumentException("El ID de estado es inválido.");
            if (pedido.total < 0)
                throw new ArgumentException("El total no puede ser negativo.");

            return await _repositorio.ActualizarAsync(pedido);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var existe = await _repositorio.ObtenerPorIdAsync(id);
            if (existe == null)
            {
                throw new KeyNotFoundException($"No se encontró el pedido con ID {id}.");
            }

            return await _repositorio.EliminarAsync(id);
        }
    }
}
