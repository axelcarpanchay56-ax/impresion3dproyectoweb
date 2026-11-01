<<<<<<< HEAD
using impresion3d.Core.Entidades;
=======
﻿using impresion3d.Core.Entidades;
>>>>>>> 30d4d536c35ec2ce3d6c51844ada30d439c75d94

namespace impresion3d.Core.Interfaces
{
    public interface IModelo3DRepositorio
    {
        Task<List<Modelo3D>> ObtenerTodosAsync();
<<<<<<< HEAD
        Task<Modelo3D?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(Modelo3D entidad);
        Task<bool> ActualizarAsync(Modelo3D entidad);
        Task<bool> EliminarAsync(int id);
    }
}
=======

        Task<Modelo3D?> ObtenerPorIdAsync(int id);

        Task<int> CrearAsync(Modelo3D modelo3D);

        Task<bool> ActualizarAsync(Modelo3D modelo3D);

        Task<bool> EliminarAsync(int id);
    }
} 
>>>>>>> 30d4d536c35ec2ce3d6c51844ada30d439c75d94
