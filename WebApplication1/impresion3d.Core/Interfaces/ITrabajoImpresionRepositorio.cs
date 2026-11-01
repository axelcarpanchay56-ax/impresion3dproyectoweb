using System;
using System.Collections.Generic;
using System.Text;
using impresion3d.Core.Entidades;

namespace impresion3d.Core.Interfaces
{
    public interface ITrabajoImpresionRepositorio
    {
        Task<List<TrabajoImpresion>> ObtenerTodosAsync();
        Task<TrabajoImpresion?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(TrabajoImpresion entidad);
        Task<bool> ActualizarAsync(TrabajoImpresion entidad);
        Task<bool> EliminarAsync(int id);
    }
}
