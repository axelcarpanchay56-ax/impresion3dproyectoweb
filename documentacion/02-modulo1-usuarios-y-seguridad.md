# 02. Módulo 1: Usuarios, Clientes, Personas, Roles y Seguridad

Este módulo administra la identidad de todas las personas que interactúan con **Innova 3D**, el registro de clientes desde la web, los permisos por rol (`Administrador` y `Cliente`), la encriptación de contraseñas con **BCrypt** y la autenticación mediante **Tokens JWT**.

---

## 1. Entidades del Módulo (`impresion3d.Core/Entidades/`)

1. **`Persona.cs` (Tabla `personas`)**:
   - Guarda los datos personales básicos: `IdPersona`, `Dni`, `Nombre`, `Apellido`, `Telefono`, `Email`, `Direccion`, `Activo` y `FechaAlta`.
2. **`Clientes.cs` (Tabla `clientes`)**:
   - Vincula a una persona (`id_persona`) como cliente habilitado para realizar pedidos en la tienda (`IdCliente`).
3. **`Usuarios.cs` (Tabla `usuarios`)**:
   - Representa la cuenta de acceso al sistema: `IdUsuario`, `id_persona`, `usuario` (nombre de usuario único), `password_hash` (contraseña encriptada con BCrypt), `activo`, `fecha_creacion` y `ultimo_acceso`.
4. **`Roles.cs` (Tabla `roles`)**:
   - Define los niveles de acceso del sistema (`IdRol`, `nombre`, `descripcion`). Por defecto existen `Administrador` y `Cliente`.
5. **`Usuario_Rol.cs` (Tabla `usuario_rol`)**:
   - Tabla intermedia que asigna uno o varios roles (`id_rol`) a un usuario (`id_usuario`).

---

## 2. Seguridad y Encriptación (`impresion3d.Core/Servicios/`)

### `ServicioPassword.cs` (Encriptación BCrypt)
- **`HashPassword(string password)`**: Utiliza `BCrypt.Net.BCrypt.HashPassword(password)` para generar un hash irreversible de 60 caracteres con sal criptográfica aleatoria (ejemplo: `$2a$11$K.Aa88yJ5aW/...`).
- **`VerificarPassword(string password, string? hash)`**: Compara de forma segura la contraseña ingresada en el login contra el hash guardado en MySQL usando `BCrypt.Net.BCrypt.Verify`.
- **Dónde se aplica**:
  - En `ServicioUsuario.CrearAsync`: antes de insertar cualquier usuario nuevo en la base de datos, transforma automáticamente su contraseña en un hash BCrypt.
  - En `appsettings.json` y `Program.cs` (`InicializarAdminAsync`): la cuenta del administrador ya está preconfigurada con su `PasswordHash` en BCrypt para que la contraseña nunca aparezca en texto plano ni en la base de datos ni en el código subido a GitHub.

### `AutenticacionService.cs` (Autenticación y Tokens JWT)
- **`ValidarCredencialesAsync(string usuario, string password)`**:
  1. Busca el usuario por nombre en `IUsuarioRepositorio.ObtenerPorUsuarioAsync`.
  2. Verifica que la cuenta esté activa (`activo == true`).
  3. Valida la contraseña con `ServicioPassword.VerificarPassword`.
- **`ObtenerRolesAsync(int idUsuario)`**:
  - Consulta los roles asignados al usuario en la tabla `usuario_rol` y devuelve la lista de nombres (ej. `["Administrador"]` o `["Cliente"]`).
- **`GenerarToken(Usuarios usuario, List<string> roles)`**:
  - Crea un token **JWT (`JwtSecurityToken`)** firmado con `HmacSha256` usando la clave secreta de `appsettings.json`, incluyendo como `Claims` el `IdUsuario`, el nombre de usuario y cada uno de sus roles (`ClaimTypes.Role`).

---

## 3. Repositorios Dapper (`impresion3d.Infrastructure/Repositorios/`)

- **`PersonaRepositorio.cs`**:
  - Ejecuta `sp_persona_crear`, `sp_persona_actualizar`, `sp_persona_eliminar`, `sp_persona_obtener_por_id`, `sp_persona_obtener_por_dni`, `sp_persona_listar` y `sp_persona_buscar` mediante Dapper.
- **`ClienteRepositorio.cs`**:
  - Ejecuta `sp_cliente_crear` y `sp_cliente_obtener_todos`.
- **`UsuarioRepositorio.cs`**:
  - Ejecuta `sp_usuario_crear`, `sp_usuario_login`, `sp_usuario_obtener_por_usuario` y `sp_usuario_cambiar_estado`.
- **`RolRepositorio.cs`**:
  - Ejecuta `sp_rol_obtener_todos`.
- **`UsuarioRolRepositorio.cs`**:
  - Ejecuta `sp_usuario_rol_asignar` y `sp_usuario_rol_obtener_por_usuario`.

---

## 4. Validadores FluentValidation (`WebApplication1/Validadores/`)

- **`PersonaCrearValidator.cs` / `PersonaActualizarValidator.cs`**: Validan que el DNI tenga entre 7 y 10 caracteres, que Nombre y Apellido no estén vacíos y que el Email tenga formato válido.
- **`UsuarioRegistroValidator.cs`**: Valida el formulario unificado de registro público (`RegistroUsuarioDto`): Nombre, Apellido, DNI, nombre de usuario (mínimo 3 caracteres) y contraseña (mínimo 4 caracteres).
- **`UsuarioCrearValidator.cs`**, **`ClienteCrearValidator.cs`** y **`UsuarioRolAsignarValidator.cs`**: Validan las altas administrativas de usuarios, clientes y asignación de roles.

---

## 5. Controladores REST (`WebApplication1/Controllers/`)

- **`UsuariosController.cs` (`/api/usuarios`)**:
  - `POST /api/usuarios/login`: Valida credenciales, obtiene los roles del usuario, genera el JWT y devuelve `{ Token, Usuario, Roles, EsAdmin }`.
  - `POST /api/usuarios/registro`: Registra en un solo paso a un nuevo cliente desde la web (crea la `Persona`, la vincula en `Clientes`, crea el `Usuario` con contraseña encriptada en BCrypt, le asigna el rol `Cliente` en `Usuario_Rol` y le devuelve su token JWT para iniciar sesión automáticamente).
  - `POST /api/usuarios` y `PATCH /api/usuarios/{id}/estado`: Alta administrativa y activación/baja lógica de cuentas.
- **`PersonasController.cs` (`/api/personas`)**:
  - `GET /api/personas`, `GET /api/personas/{id}`, `GET /api/personas/dni/{dni}`, `GET /api/personas/buscar?termino=...`, `POST`, `PUT`, `DELETE`.
- **`ClientesController.cs` (`/api/clientes`)**:
  - `GET /api/clientes` y `POST /api/clientes`.
- **`RolesController.cs` (`/api/roles`)** y **`UsuarioRolController.cs` (`/api/usuariorol`)**:
  - Consulta de roles disponibles y asignación de roles a usuarios desde la pestaña *Clientes & Roles* del Panel de Gestión.
