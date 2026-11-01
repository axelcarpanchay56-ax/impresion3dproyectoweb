using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;

namespace impresion3d.Core.Servicios
{
    public class ServicioImpresoraMaterial
    {
        private readonly IImpresoraMaterialRepositorio _repositorio;

        public ServicioImpresoraMaterial(IImpresoraMaterialRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<ImpresoraMaterial>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<ImpresoraMaterial> ObtenerPorIdAsync(int id_impresora, int id_material)
        {
            var item = await _repositorio.ObtenerPorIdAsync(id_impresora, id_material);
            if (item == null)
            {
                throw new KeyNotFoundException($"No se encontró la asociación entre la impresora {id_impresora} y el material {id_material}.");
            }
            return item;
        }

        public async Task<List<ImpresoraMaterial>> ObtenerPorImpresoraAsync(int id_impresora)
        {
            return await _repositorio.ObtenerPorImpresoraAsync(id_impresora);
        }

        public async Task<bool> CrearAsync(ImpresoraMaterial entidad)
        {
            if (entidad.id_impresora <= 0)
                throw new ArgumentException("El ID de la impresora es obligatorio.");
            if (entidad.id_material <= 0)
                throw new ArgumentException("El ID del material es obligatorio.");

            return await _repositorio.CrearAsync(entidad);
        }

        public async Task<bool> ActualizarAsync(ImpresoraMaterial entidad)
        {
            var existe = await _repositorio.ObtenerPorIdAsync(entidad.id_impresora, entidad.id_material);
            if (existe == null)
            {
                throw new KeyNotFoundException($"No se encontró la asociación entre la impresora {entidad.id_impresora} y el material {entidad.id_material}.");
            }

            return await _repositorio.ActualizarAsync(entidad);
        }

        public async Task<bool> EliminarAsync(int id_impresora, int id_material)
        {
            var existe = await _repositorio.ObtenerPorIdAsync(id_impresora, id_material);
            if (existe == null)
            {
                throw new KeyNotFoundException($"No se encontró la asociación entre la impresora {id_impresora} y el material {id_material}.");
            }

            return await _repositorio.EliminarAsync(id_impresora, id_material);
        }
    }
}
