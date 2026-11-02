using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using System;

namespace impresion3d.Core.Servicios
{
    public class ServicioImpresoras
    {
        private readonly IImpresorasRepositorio _repositorio;

        public ServicioImpresoras(IImpresorasRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Impresoras>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<Impresoras> ObtenerPorIdAsync(int id)
        {
            var impresora = await _repositorio.ObtenerPorIdAsync(id);
            if (impresora == null)
            {
                throw new KeyNotFoundException($"No se encontró la impresora con ID {id}.");
            }
            return impresora;
        }

        public async Task<int> CrearAsync(Impresoras impresora)
        {
            if (string.IsNullOrWhiteSpace(impresora.nombre))
            {
                throw new ArgumentException("El nombre de la impresora es obligatorio.");
            }

            return await _repositorio.CrearAsync(impresora);
        }

        public async Task<bool> ActualizarAsync(Impresoras impresora)
        {
            var existe = await _repositorio.ObtenerPorIdAsync(impresora.id_impresora);
            if (existe == null)
            {
                throw new KeyNotFoundException($"No se encontró la impresora con ID {impresora.id_impresora}.");
            }

            return await _repositorio.ActualizarAsync(impresora);
        }

        public async Task<bool> CambiarEstadoAsync(int id, bool estado)
        {
            var existe = await _repositorio.ObtenerPorIdAsync(id);
            if (existe == null)
            {
                throw new KeyNotFoundException($"No se encontró la impresora con ID {id}.");
            }

            return await _repositorio.CambiarEstadoAsync(id, estado);
        }
    }
}