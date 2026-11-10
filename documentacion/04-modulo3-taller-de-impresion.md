# 04. Módulo 3: Taller de Impresión 3D, Trabajos y Consumo

Este módulo controla la operación interna del taller de **Innova 3D**: el parque de impresoras 3D (FDM de filamento y SLA de resina), qué materiales son compatibles con cada máquina, la cola de trabajos de impresión, los estados de cada trabajo y el consumo real de filamento.

---

## 1. Entidades del Módulo (`impresion3d.Core/Entidades/`)

1. **`Impresoras.cs` (Tabla `impresoras`)**:
   - `id_impresora`, `nombre`, `marca`, `modelo`, `tecnologia` (`FDM` / `SLA`), `volumen_x`, `volumen_y`, `volumen_z` (en mm), `estado` (`true` = Operativa, `false` = En mantenimiento) y `fecha_alta`.
2. **`ImpresoraMaterial.cs` (Tabla `impresora_material`)**:
   - Define qué material (`id_material`) es compatible con qué impresora (`id_impresora`) junto con una `observacion` técnica (ej. temperatura o diámetro de boquilla).
3. **`TrabajoImpresion.cs` (Tabla `trabajo_impresion`)**:
   - Representa una orden de fabricación en una impresora: `id_trabajo_impresion`, `id_pedido`, `id_producto`, `id_impresora`, `id_modelo`, `id_material`, `cantidad`, `id_estado`, `fecha_inicio`, `fecha_fin`, `tiempo_estimado`, `peso_estimado` y `observacion`.
4. **`EstadoImpresion.cs` (Tabla `estado_impresion`)**:
   - Catálogo de estados de un trabajo de impresión (`1 = Pendiente`, `2 = Imprimiendo`, `3 = Finalizada`, `4 = Fallida`).
5. **`ConsumoMaterial.cs` (Tabla `consumo_material`)**:
   - Registra el peso real en gramos (`cantidad`) de un material (`id_material`) consumido por un trabajo de impresión (`id_trabajo`) y su `fecha_hora`.

---

## 2. Servicios de Negocio (`impresion3d.Core/Servicios/`)

- **`ServicioImpresoras.cs`**:
  - Verifica dimensiones de volumen (`volumen_x`, `volumen_y`, `volumen_z` > 0), marca, modelo y tecnología antes de dar de alta o cambiar el estado operativo de una máquina.
- **`ServicioImpresoraMaterial.cs`**:
  - Gestiona la vinculación entre impresoras y materiales compatibles.
- **`ServicioTrabajoImpresion.cs`**:
  - Valida que el pedido, producto, impresora, modelo, material y estado existan y que la cantidad de unidades sea mayor a cero.
- **`ServicioEstadoImpresion.cs`**:
  - Administra los estados posibles de los trabajos del taller.
- **`ServicioConsumoMaterial.cs`**:
  - Controla el registro de gramos reales consumidos en cada impresión.

---

## 3. Repositorios Dapper (`impresion3d.Infrastructure/Repositorios/`)

- **`ImpresorasRepositorio.cs`**:
  - Ejecuta `sp_impresora_crear`, `sp_impresora_actualizar`, `sp_impresora_cambiar_estado`, `sp_impresora_eliminar`, `sp_impresora_obtener_por_id` y `sp_impresora_obtener_todos`.
- **`ImpresoraMaterialRepositorio.cs`**:
  - Ejecuta `sp_impresora_material_crear`, `sp_impresora_material_actualizar`, `sp_impresora_material_eliminar`, `sp_impresora_material_obtener_por_id`, `sp_impresora_material_obtener_por_impresora` y `sp_impresora_material_obtener_todos`.
- **`TrabajoImpresionRepositorio.cs`**:
  - Ejecuta `sp_trabajo_impresion_crear`, `sp_trabajo_impresion_actualizar`, `sp_trabajo_impresion_eliminar`, `sp_trabajo_impresion_obtener_por_id` y `sp_trabajo_impresion_obtener_todos`.
- **`EstadoImpresionRepositorio.cs`**:
  - Ejecuta `sp_estado_impresion_crear`, `sp_estado_impresion_actualizar`, `sp_estado_impresion_eliminar`, `sp_estado_impresion_obtener_por_id` y `sp_estado_impresion_obtener_todos`.
- **`ConsumoMaterialRepositorio.cs`**:
  - Ejecuta `sp_consumo_material_crear`, `sp_consumo_material_actualizar`, `sp_consumo_material_eliminar`, `sp_consumo_material_obtener_por_id`, `sp_consumo_material_obtener_por_trabajo` y `sp_consumo_material_obtener_todos`.

---

## 4. Validadores y Controladores (`WebApplication1/`)

- **Validadores (`Validadores/`)**:
  - `ImpresoraCrearValidator.cs`, `ImpresoraMaterialCrearValidator.cs`, `TrabajoImpresionCrearValidator.cs`, `EstadoImpresionCrearValidator.cs` y `ConsumoMaterialCrearValidator.cs`.
- **Controladores (`Controllers/`)**:
  - `ImpresorasController.cs` (`/api/impresoras`) e `ImpresoraMaterialController.cs` (`/api/impresoramaterial`): conectados a la **Pestaña 3 (Impresoras 3D)** del Panel de Gestión.
  - `TrabajoImpresionController.cs` (`/api/trabajoimpresion`), `EstadoImpresionController.cs` (`/api/estadoimpresion`) y `ConsumoMaterialController.cs` (`/api/consumomaterial`): conectados a la **Pestaña 5 (Trabajos & Consumo)** del Panel de Gestión.
