# 06. Frontend Web — Innova 3D (`paginaImpresiones3D/`)

Este documento explica cómo funciona cada archivo del frontend de **Innova 3D** y cómo se integra visual y funcionalmente con el backend `.NET 10`.

---

## 1. Archivos que Componen el Frontend

### 1.1 `logo.svg`
- Logotipo vectorial SVG con estética oscura y trazos en cian neón (`#00f0ff`), representando un cubo 3D isométrico y el cabezal extrusor de una impresora 3D.

### 1.2 `index.html`
Contiene toda la estructura semántica de la página con un lenguaje amigable para el cliente final (sin tecnicismos):
- **Barra de Navegación (`<header>`)**:
  - Enlaces a las secciones (`Inicio`, `Por qué elegirnos`, `Categorías`, `Catálogo`, `Cotizador`, `Preguntas`).
  - Botón **Mi Pedido** (`#btn-open-cart`) con contador dinámico de unidades (`#cart-badge-count`).
  - Botones públicos **Iniciar Sesión** y **Registrarse** (`#auth-guest-actions`).
  - El botón **Panel de Gestión** (`#btn-open-admin-panel`) permanece oculto (`display: none`) para visitantes y clientes normales, y **solo se muestra cuando inicia sesión un usuario con rol `Administrador`**.
- **Secciones Públicas**:
  - **Hero (`#inicio`)**: Presentación de la marca y accesos rápidos al catálogo y cotizador.
  - **Ventajas (`#elegirnos`)**: Puntos fuertes del servicio de impresión 3D.
  - **Categorías (`#categorias`)** y **Catálogo (`#catalogo`)**: Grillas dinámicas con filtrado por categoría en vivo.
  - **Cotizador (`#cotizar`)**: Formulario para proyectos personalizados que carga en tiempo real los materiales disponibles desde `/api/materiales`.
  - **Preguntas Frecuentes (`#faq`)**: Acordeón interactivo.
  - **Pie de Página (`<footer>`)**: Datos de contacto actualizados con el WhatsApp **`+54 3838 40-1302`** e Instagram `@innova._3d`.
- **Ventanas Modales**:
  1. **Modal de Cuenta (`#auth-modal`)**: Pestañas *Iniciar Sesión* y *Registrarse*.
  2. **Modal del Carrito (`#cart-modal`)**: Lista de artículos agregados, control de cantidades (`+` / `-`), cálculo de total y envío directo del pedido por WhatsApp.
  3. **Modal del Panel de Gestión (`#admin-panel-modal`)**: Panel administrativo dividido en **6 pestañas** que conectan con los 19 controladores del backend.

---

### 1.3 `styles.css`
Define la identidad visual oscura (`--bg-main: #07090e`, `--accent: #00f0ff`) e incluye:
- **Skeleton Loaders de Estructura (`.skeleton-card`, `.skeleton-box`, `.skeleton-line`, `.skeleton-circle`, `.skeleton-pill`)**:
  - Utilizan una animación `@keyframes skeletonShimmer` que simula la silueta exacta de las tarjetas de categorías, filtros y productos mientras se cargan los datos, reemplazando los spinners antiguos.
- **Estilos de Carrito y Modales (`.cart-modal-card`, `.cart-item-row`, `.btn-whatsapp-checkout`, `.admin-modal-card`)**:
  - Diseño adaptable (responsive) para celulares y escritorio.

---

### 1.4 `main.js`
Es el motor JavaScript del frontend. Sus bloques principales son:

1. **Configuración general**:
   - `API_BASE_URL`: Detecta automáticamente `/api` cuando corre en el servidor `.NET` (`http://localhost:5063`).
   - `WHATSAPP_NUMBER`: Configurado en `"543838401302"` (`+54 3838 40-1302`).
2. **Skeleton Loaders (`renderSkeletonCategorias`, `renderSkeletonFiltros`, `renderSkeletonProductos`, `renderSkeletonAdminCards`)**:
   - Dibujan la estructura gris animada antes de cada consulta y la reemplazan suavemente por las tarjetas reales.
3. **Catálogo y Cotizador (`cargarProductosDesdeAPI`, `cargarMaterialesDesdeAPI`, `renderProductos`)**:
   - Obtiene los productos desde `GET /api/productos` y los materiales desde `GET /api/materiales`.
   - Si el catálogo aún no tiene productos cargados en la base de datos, muestra los artículos de muestra iniciales y permite al administrador publicarlos con un solo clic (*«Cargar Artículos de Ejemplo»*).
4. **Carrito «Mi Pedido» directo a WhatsApp (`agregarAlCarrito`, `renderizarModalCarrito`, `handleEnviarPedidoWhatsApp`)**:
   - Guarda los productos elegidos en `localStorage` (`innova3d_cart_items`).
   - Al confirmar el pedido, lo registra en `/api/pedidos` y `/api/detallepedido` y abre WhatsApp con el detalle completo listo para enviar al administrador.
5. **Autenticación y Roles (`handleUserLogin`, `handleUserRegister`, `esUsuarioAdministrador`)**:
   - Conecta los formularios con `POST /api/usuarios/login` y `POST /api/usuarios/registro`.
   - Guarda la sesión y el Token JWT para autorizar las peticiones del administrador.
6. **Las 6 Pestañas del Panel de Gestión**:
   - **Pestaña 1**: `cargarAdminProductos`, `handleGuardarProducto`, `handleAsignarProductoMaterial` (`/api/productos` + `/api/productomaterial`).
   - **Pestaña 2**: `cargarAdminMateriales`, `handleGuardarMaterial`, `handleGuardarMovimientoMaterial` (`/api/materiales` + `/api/movimientomaterial`).
   - **Pestaña 3**: `cargarAdminImpresoras`, `handleGuardarImpresora`, `handleGuardarImpresoraMaterial` (`/api/impresoras` + `/api/impresoramaterial`).
   - **Pestaña 4**: `cargarAdminModelosYPedidos`, `handleGuardarModelo3D` (`/api/modelo3d` + `/api/impresion` + `/api/pedidos` + `/api/estadopedido`).
   - **Pestaña 5**: `cargarAdminTaller`, `handleGuardarTrabajoImpresion`, `handleGuardarConsumoMaterial` (`/api/trabajoimpresion` + `/api/estadoimpresion` + `/api/consumomaterial`).
   - **Pestaña 6**: `cargarAdminClientesYRoles`, `handleBuscarPersonaAdmin`, `handleAsignarRolUsuario` (`/api/personas` + `/api/clientes` + `/api/roles` + `/api/usuariorol`).
