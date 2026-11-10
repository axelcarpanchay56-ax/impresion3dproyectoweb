# 01. Arquitectura General del Sistema — Innova 3D

Este documento describe la estructura general del proyecto **Innova 3D**, cómo se divide en capas siguiendo principios de **Arquitectura Limpia (Clean Architecture)** en **.NET 10**, y cómo se comunica con la base de datos **MySQL 8.0** y el frontend web.

---

## 1. Estructura de Carpetas del Repositorio

```text
impresion3dproyectoweb/
├── impresion3dbd.sql                  # Script único de Base de Datos (18 tablas + datos + 87 Stored Procedures)
├── paginaImpresiones3D/               # Frontend Web (Innova 3D)
│   ├── index.html                     # Estructura principal, modales de Login/Registro, Carrito y Panel Admin
│   ├── styles.css                     # Estilos Dark/Neon Cian, tarjetas, modales y Skeleton Loaders (shimmer)
│   ├── main.js                        # Lógica de cliente, consumo de API .NET 10, Carrito a WhatsApp y CRUD Admin
│   └── logo.svg                       # Logotipo vectorial oficial de Innova 3D
├── documentacion/                     # Documentación técnica detallada de cada parte del código
└── WebApplication1/                   # Solución Backend C# (.NET 10)
    ├── impresion3dWeb.slnx            # Archivo de solución .NET
    ├── impresion3d.Core/              # Capa de Dominio y Lógica de Negocio
    │   ├── Entidades/                 # Modelos de las 18 tablas del dominio
    │   ├── Interfaces/                # Contratos de Repositorios y Servicios
    │   └── Servicios/                 # Reglas de negocio, BCrypt (ServicioPassword) y JWT (AutenticacionService)
    ├── impresion3d.Infrastructure/    # Capa de Acceso a Datos e Infraestructura
    │   ├── Contexto/                  # Conexión MySQL (MiConexion) y registro de errores (ArchivoLog)
    │   └── Repositorios/              # 17 Repositorios implementados con Dapper y Procedimientos Almacenados
    ├── impresion3d.Shared/            # Capa transversal compartida
    ├── impresion3d.Test/              # Pruebas unitarias y de integración con MSTest
    └── WebApplication1/               # Capa de Presentación / API REST (impresion3d.Api)
        ├── AutoMapper/                # Configuración central de mapeo Entidad <-> DTO (MappeoDto.cs)
        ├── Controllers/               # 19 Controladores HTTP REST
        ├── DTOs/                      # Objetos de Transferencia de Datos (Entrada / Salida)
        ├── Validadores/               # Validaciones de entrada con FluentValidation
        ├── Program.cs                 # Inyección de dependencias, JWT, CORS, Archivos Estáticos y Seed Admin
        └── appsettings.json           # Cadena de conexión, configuración JWT y Hash BCrypt del Administrador
```

---

## 2. Flujo de una Petición de Extremo a Extremo

Cada acción que realiza un usuario o el administrador en la página sigue este recorrido estandarizado:

1. **Frontend (`paginaImpresiones3D/main.js`)**:
   - Muestra un **Skeleton Loader de estructura** mientras realiza la petición `fetch()` hacia `http://localhost:5063/api/...`.
   - Si es una operación protegida del Panel de Gestión, adjunta el encabezado `Authorization: Bearer <TOKEN_JWT>`.
2. **Controlador (`WebApplication1/Controllers/*Controller.cs`)**:
   - Recibe el **DTO** (`Crear...Dto` / `Actualizar...Dto`).
   - Ejecuta el **Validador de FluentValidation** (`IValidator<T>`). Si algún dato no cumple las reglas, devuelve `HTTP 400 BadRequest` con la lista detallada de errores.
   - Transforma el DTO a una **Entidad de Dominio** usando **AutoMapper** (`IMapper`).
3. **Servicio de Negocio (`impresion3d.Core/Servicios/Servicio*.cs`)**:
   - Verifica las reglas de negocio propias del dominio (por ejemplo, que un DNI no esté duplicado, que los precios o cantidades sean mayores a cero, o encripta la contraseña con **BCrypt**).
4. **Repositorio Dapper (`impresion3d.Infrastructure/Repositorios/*Repositorio.cs`)**:
   - Abre la conexión a MySQL mediante `IMiConexion`.
   - Ejecuta el **Procedimiento Almacenado (Stored Procedure)** correspondiente usando **Dapper** (`QueryAsync`, `QueryFirstOrDefaultAsync`, `ExecuteAsync`, `ExecuteScalarAsync`) con `CommandType.StoredProcedure` y `DynamicParameters`.
   - En caso de excepción SQL (`MySqlException`), registra el detalle técnico en `ArchivoLog` y propaga el error controlado.
5. **Base de Datos (`impresion3dbd.sql`)**:
   - Ejecuta la consulta dentro del motor MySQL 8.0 manteniendo la integridad referencial.

---

## 3. Tecnologías Utilizadas y Por Qué se Eligieron

- **.NET 10 (ASP.NET Core Web API)**: Framework moderno de alto rendimiento que sirve tanto los endpoints REST (`/api/*`) como los archivos estáticos del frontend en un único puerto (`http://localhost:5063`).
- **Dapper (`Dapper` + `MySqlConnector`)**: Micro-ORM ultraligero que mapea automáticamente las columnas con guiones bajos de MySQL (`MatchNamesWithUnderscores = true`) hacia las propiedades de las clases C#.
- **AutoMapper (`MappeoDto.cs`)**: Evita convertir manualmente propiedades entre las entidades de base de datos y los DTOs expuestos al exterior.
- **FluentValidation (`Validadores/`)**: Separa las reglas de validación de los controladores, manteniendo el código limpio, legible y estandarizado.
- **BCrypt (`BCrypt.Net-Next`)**: Encripta todas las contraseñas con sal criptográfica antes de guardarlas en la base de datos, impidiendo que queden expuestas en texto plano.
- **JWT (`Microsoft.AspNetCore.Authentication.JwtBearer`)**: Protege las rutas de administración mediante tokens firmados digitalmente.
