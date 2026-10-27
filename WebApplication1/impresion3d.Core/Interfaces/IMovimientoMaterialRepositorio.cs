using impresion3d.Core.Entidades;

namespace impresion3d.Core.Interfaces
{
    public interface IMovimientoMaterialRepositorio
    {
        Task<List<MovimientoMaterial>> ObtenerTodosAsync();

        Task<MovimientoMaterial?> ObtenerPorIdAsync(int id);

        Task<int> CrearAsync(MovimientoMaterial movimientoMaterial);

        Task<bool> ActualizarAsync(MovimientoMaterial movimientoMaterial);

        Task<bool> EliminarAsync(int id);
    }
} 
