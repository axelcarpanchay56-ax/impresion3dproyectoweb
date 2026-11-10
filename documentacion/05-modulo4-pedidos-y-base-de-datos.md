# 05. Módulo 4: Pedidos, Detalles y Base de Datos (`impresion3dbd.sql`)

Este módulo administra el registro de los pedidos que realizan los clientes desde la tienda web, el desglose de cada artículo pedido (`detalle_pedido`), los estados del pedido (`estado_pedido`) y la estructura unificada de la base de datos en `impresion3dbd.sql`.

---

## 1. Entidades del Módulo 4 (`impresion3d.Core/Entidades/`)

1. **`Pedidos.cs` (Tabla `pedidos`)**:
   - `id_pedido`, `id_cliente`, `fecha_pedido`, `id_estado` y `total`.
2. **`DetallePedido.cs` (Tabla `detalle_pedido`)**:
   - `id_detalle`, `id_pedido`, `id_producto`, `cantidad`, `precio_unitario` y `subtotal`.
3. **`EstadoPedido.cs` (Tabla `estado_pedido`)**:
   - `id_estado_pedido` y `nombre` (`1 = Pendiente`, `2 = En proceso`, `3 = Finalizado`, `4 = Cancelado`).

---

## 2. Servicios, Repositorios, Validadores y Controladores del Módulo 4

- **Servicios (`impresion3d.Core/Servicios/`)**:
  - `ServicioPedidos.cs`, `ServicioDetallePedido.cs` y `ServicioEstadoPedido.cs`.
- **Repositorios Dapper (`impresion3d.Infrastructure/Repositorios/`)**:
  - `PedidosRepositorio.cs`: ejecuta `sp_pedido_crear`, `sp_pedido_actualizar`, `sp_pedido_cambiar_estado`, `sp_pedido_eliminar`, `sp_pedido_obtener_por_id`, `sp_pedido_obtener_por_cliente` y `sp_pedido_obtener_todos`.
  - `DetallePedidoRepositorio.cs`: ejecuta `sp_detalle_pedido_crear`, `sp_detalle_pedido_actualizar`, `sp_detalle_pedido_eliminar`, `sp_detalle_pedido_obtener_por_id`, `sp_detalle_pedido_obtener_por_pedido` y `sp_detalle_pedido_obtener_todos`.
  - `EstadoPedidoRepositorio.cs`: ejecuta `sp_estado_pedido_crear`, `sp_estado_pedido_actualizar`, `sp_estado_pedido_eliminar`, `sp_estado_pedido_obtener_por_id` y `sp_estado_pedido_obtener_todos`.
- **Validadores (`WebApplication1/Validadores/`)**:
  - `PedidosCrearValidator.cs`, `DetallePedidoCrearValidator.cs` y `EstadoPedidoCrearValidator.cs`.
- **Controladores (`WebApplication1/Controllers/`)**:
  - `PedidosController.cs` (`/api/pedidos`), `DetallePedidoController.cs` (`/api/detallepedido`) y `EstadoPedidoController.cs` (`/api/estadopedido`).

---

## 3. Cómo se Conectan los Pedidos con WhatsApp

Cuando un cliente arma su carrito en **Mi Pedido** y toca **«Enviar Pedido por WhatsApp»**:
1. `main.js` envía una petición `POST /api/pedidos` con el total del carrito y luego registra cada ítem en `POST /api/detallepedido`.
2. De este modo, el administrador puede ver el historial de pedidos y sus estados dentro de la **Pestaña 4 (Modelos 3D & Pedidos)** del Panel de Gestión.
3. Inmediatamente después, se abre WhatsApp hacia el número **`+54 3838 40-1302`** con el resumen detallado de productos, cantidades, subtotales, total estimado y datos del cliente, evitando comisiones de pasarelas de pago o billeteras virtuales.

---

## 4. El Archivo Único de Base de Datos (`impresion3dbd.sql`)

En la raíz del repositorio se encuentra **`impresion3dbd.sql`**, que concentra absolutamente toda la base de datos `db_impresiones3d` en un solo archivo:

- **Creación de Base de Datos**: Incluye `CREATE DATABASE IF NOT EXISTS db_impresiones3d` y `USE db_impresiones3d`.
- **Las 18 Tablas del Sistema**:
  1. `personas`
  2. `clientes`
  3. `usuarios`
  4. `roles`
  5. `usuario_rol`
  6. `productos`
  7. `materiales`
  8. `modelo3d`
  9. `producto_material`
  10. `movimiento_material`
  11. `impresoras`
  12. `impresora_material`
  13. `trabajo_impresion`
  14. `estado_impresion`
  15. `consumo_material`
  16. `pedidos`
  17. `detalle_pedido`
  18. `estado_pedido`
- **Datos Semilla Seguros**:
  - Roles `Administrador` y `Cliente`.
  - Usuario `admin` con teléfono `+54 3838 40-1302` y contraseña encriptada con **BCrypt**.
  - Estados iniciales de pedidos y estados de impresión.
- **Los 87 Procedimientos Almacenados (Stored Procedures)**:
  - Incluye todas las rutinas `sp_*` de los 4 módulos para altas, bajas, modificaciones, búsquedas y listados consumidos por Dapper.
