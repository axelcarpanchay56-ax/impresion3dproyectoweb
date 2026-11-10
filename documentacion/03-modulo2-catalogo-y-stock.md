# 03. Módulo 2: Catálogo de Productos, Materiales, Modelos 3D y Stock

Este módulo gestiona todo el catálogo comercial de **Innova 3D**, los materiales de impresión (PLA, PETG, ABS, TPU, Resina), los archivos digitales de modelos 3D (`.STL`, `.3MF`, `.OBJ`, `.STEP`), la relación de qué material utiliza cada producto y el historial de movimientos de stock.

---

## 1. Entidades del Módulo (`impresion3d.Core/Entidades/`)

1. **`Productos.cs` (Tabla `productos`)**:
   - `IdProducto`, `id_modelo3d`, `nombre`, `descripcion`, `precio_venta`, `activo` y `fecha_alta`.
2. **`Materiales.cs` (Tabla `materiales`)**:
   - `id_material`, `nombre`, `tipo`, `color`, `diametro`, `unidad_medida`, `activo`.
3. **`Modelo3D.cs` (Tabla `modelo3d`)**:
   - `id_modelo3d`, `nombre`, `formato`, `archivo`, `descripcion`, `fecha_creacion`.
4. **`ProductoMaterial.cs` (Tabla `producto_material`)**:
   - Vincula un artículo (`IdProducto`) con un material (`IdMaterial`) e indica la `cantidad_estimada` en gramos necesaria para fabricarlo.
5. **`MovimientoMaterial.cs` (Tabla `movimiento_material`)**:
   - Registra cada ingreso o egreso de filamento/resina en el taller: `IdMovimientoMaterial`, `id_material`, `tipo_movimiento` (`Entrada` / `Salida`), `cantidad`, `fecha_hora` y `observacion`.

---

## 2. Servicios de Negocio (`impresion3d.Core/Servicios/`)

- **`ServicioProducto.cs`**:
  - Valida que el producto tenga nombre, precio mayor a 0 y gestiona el alta, modificación, listado y cambio de estado (`activo = true/false`).
- **`ServicioMateriales.cs`**:
  - Valida los datos técnicos del filamento o resina (nombre, tipo, color, diámetro en milímetros) antes de invocar al repositorio.
- **`ServicioModelo3D.cs`**:
  - Verifica que el nombre, formato (`STL`, `3MF`, `OBJ`, `STEP`) y ruta de archivo sean válidos.
- **`ServicioProductoMaterial.cs`**:
  - Controla la asignación y actualización de gramos estimados de material por producto.
- **`ServicioMovimientoMaterial.cs`**:
  - Verifica que la cantidad del movimiento de stock sea positiva y que el tipo de movimiento esté especificado antes de registrarlo.

---

## 3. Repositorios Dapper (`impresion3d.Infrastructure/Repositorios/`)

Todos los repositorios utilizan **Dapper** y llaman a los procedimientos almacenados de MySQL:
- **`ProductoRepositorio.cs`**: `sp_producto_crear`, `sp_producto_actualizar`, `sp_producto_cambiar_estado`, `sp_producto_obtener_por_id`, `sp_producto_listar`.
- **`MaterialesRepositorio.cs`**: `sp_material_crear`, `sp_material_actualizar`, `sp_material_eliminar`, `sp_material_obtener_por_id`, `sp_material_obtener_todos`.
- **`Modelo3DRepositorio.cs`**: `sp_modelo3d_crear`, `sp_modelo3d_actualizar`, `sp_modelo3d_eliminar`, `sp_modelo3d_obtener_por_id`, `sp_modelo3d_obtener_todos`.
- **`ProductoMaterialRepositorio.cs`**: `sp_producto_material_asignar`, `sp_producto_material_actualizar`, `sp_producto_material_eliminar`, `sp_producto_material_obtener_por_producto`.
- **`MovimientoMaterialRepositorio.cs`**: `sp_movimiento_material_crear`, `sp_movimiento_material_actualizar`, `sp_movimiento_material_eliminar`, `sp_movimiento_material_obtener_por_id`, `sp_movimiento_material_obtener_todos`.

---

## 4. Validadores FluentValidation (`WebApplication1/Validadores/`)

- **`ProductoCrearValidator.cs`**: Valida nombre obligatorio y `precio_venta > 0`.
- **`MaterialesCrearValidator.cs`**: Valida nombre, tipo, color, unidad de medida y `diametro > 0`.
- **`Modelo3DCrearValidator.cs`**: Valida nombre, formato y ruta del archivo 3D.
- **`ProductoMaterialAsignarValidator.cs`**: Valida `IdProducto > 0`, `IdMaterial > 0` y `cantidad_estimada > 0`.
- **`MovimientoMaterialCrearValidator.cs`**: Valida `id_material > 0`, `tipo_movimiento` no vacío y `cantidad > 0`.

---

## 5. Controladores REST (`WebApplication1/Controllers/`)

- **`ProductosController.cs` (`/api/productos`)**:
  - Alimenta el catálogo público de la tienda y la Pestaña 1 del Panel de Gestión.
- **`ProductoMaterialController.cs` (`/api/productomaterial`)**:
  - Permite asociar materiales a los artículos desde la Pestaña 1 del Panel de Gestión.
- **`MaterialesController.cs` (`/api/materiales`)**:
  - Alimenta el selector dinámico de materiales del **Cotizador** público y la Pestaña 2 del Panel de Gestión.
- **`MovimientoMaterialController.cs` (`/api/movimientomaterial`)**:
  - Gestiona las entradas y salidas de stock en la Pestaña 2 del Panel de Gestión.
- **`Modelo3DController.cs` (`/api/modelo3d`)** e **`ImpresionController.cs` (`/api/impresion`)**:
  - Administran el banco de archivos 3D en la Pestaña 4 del Panel de Gestión.
