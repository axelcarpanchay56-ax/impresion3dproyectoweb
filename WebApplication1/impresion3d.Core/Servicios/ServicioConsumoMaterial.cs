using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Core.Servicios
{
    public class ServicioConsumoMaterial
    {
        private readonly IConsumoMaterialRepositorio _repositorio;

        public ServicioConsumoMaterial(IConsumoMaterialRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<ConsumoMaterial>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<ConsumoMaterial> ObtenerPorIdAsync(int id)
        {
            var item = await _repositorio.ObtenerPorIdAsync(id);
            if (item == null)
            {
                throw new KeyNotFoundException($"No se encontró el consumo de material con ID {id}.");
            }
            return item;
        }

        public async Task<List<ConsumoMaterial>> ObtenerPorTrabajoAsync(int id_trabajo)
        {
            return await _repositorio.ObtenerPorTrabajoAsync(id_trabajo);
        }

        public async Task<int> CrearAsync(ConsumoMaterial entidad)
        {
            if (entidad.id_trabajo <= 0)
                throw new ArgumentException("El ID del trabajo de impresión es inválido.");
            if (entidad.id_material <= 0)
                throw new ArgumentException("El ID del material es inválido.");
            if (entidad.cantidad <= 0)
                throw new ArgumentException("La cantidad de material consumido debe ser mayor a cero.");

            if (entidad.fecha_hora == default)
            {
                entidad.fecha_hora = DateTime.Now;
            }

            return await _repositorio.CrearAsync(entidad);
        }

        public async Task<bool> ActualizarAsync(ConsumoMaterial entidad)
        {
            var existe = await _repositorio.ObtenerPorIdAsync(entidad.id_consumo_material);
            if (existe == null)
            {
                throw new KeyNotFoundException($"No se encontró el consumo de material con ID {entidad.id_consumo_material}.");
            }

            if (entidad.id_trabajo <= 0)
                throw new ArgumentException("El ID del trabajo de impresión es inválido.");
            if (entidad.id_material <= 0)
                throw new ArgumentException("El ID del material es inválido.");
            if (entidad.cantidad <= 0)
                throw new ArgumentException("La cantidad consumida debe ser mayor a cero.");

            return await _repositorio.ActualizarAsync(entidad);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var existe = await _repositorio.ObtenerPorIdAsync(id);
            if (existe == null)
            {
                throw new KeyNotFoundException($"No se encontró el consumo de material con ID {id}.");
            }

            return await _repositorio.EliminarAsync(id);
        }
    }
}
