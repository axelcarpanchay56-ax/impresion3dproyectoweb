// ==========================================================================
// Configuración API .NET 10 & Catálogo Base Innova 3D
// ==========================================================================
const API_BASE_URL =
    window.location.port === "5063" || window.location.port === "7209"
        ? "/api"
        : "http://localhost:5063/api";

// Si querés restringir el panel a un único nombre de usuario específico,
// podés indicarlo acá (null permite cualquier usuario con rol Administrador).
const ALLOWED_ADMIN_USERNAME = null;

const WHATSAPP_NUMBER = "543838401302";

const CATEGORIAS = [
    { id: "todos", nombre: "Todos", icon: "🌐", desc: "Ver todo el catálogo" },
    {
        id: "soportes",
        nombre: "Soportes & Bases",
        icon: "🎧",
        desc: "Organiza tu setup",
    },
    {
        id: "personajes",
        nombre: "Personajes & Bustos",
        icon: "👤",
        desc: "Figuras de tus favoritos",
    },
    { id: "mates", nombre: "Mates 3D", icon: "🧉", desc: "Disfrutá con onda" },
    {
        id: "oficina",
        nombre: "Art. de Oficina",
        icon: "✏️",
        desc: "Organizadores creativos",
    },
    {
        id: "hogar",
        nombre: "Art. para el Hogar",
        icon: "🏠",
        desc: "Decoración y funcionalidad",
    },
    {
        id: "llaveros",
        nombre: "Llaveros",
        icon: "🔑",
        desc: "Llevá tu pasión a todos lados",
    },
    {
        id: "juguetes",
        nombre: "Juguetes",
        icon: "🎲",
        desc: "Articulados e interactivos",
    },
    {
        id: "medida",
        nombre: "A medida",
        icon: "✨",
        desc: "Contanos tu proyecto",
    },
];

// Catálogo de muestra (se utiliza como respaldo cuando la BD todavía está vacía)
const PRODUCTOS_MUESTRA = [
    {
        id: 1,
        categoria: "soportes",
        nombre: "Soporte Gamer de Auriculares",
        emoji: "🎧",
        precio: 9500,
        desc: "Soporte ergonómico para auriculares, ideal para tu escritorio o setup.",
        esMuestra: true,
    },
    {
        id: 2,
        categoria: "soportes",
        nombre: "Soporte con Organizador de Cables",
        emoji: "🎛️",
        precio: 7800,
        desc: "Optimiza espacio y organiza cables de manera sencilla.",
        esMuestra: true,
    },
    {
        id: 3,
        categoria: "mates",
        nombre: "Mate 3D Personalizado",
        emoji: "🧉",
        precio: 11500,
        desc: "Varios modelos. Incluye bombilla y polímero para su uso seguro.",
        esMuestra: true,
    },
    {
        id: 4,
        categoria: "mates",
        nombre: "Set de Mates Completo",
        emoji: "🍵",
        precio: 22000,
        desc: "Con base, bombilla, yerbero y porta-mate en impresión reforzada.",
        esMuestra: true,
    },
    {
        id: 5,
        categoria: "personajes",
        nombre: "Figura de Personaje Favorito",
        emoji: "🦸‍♂️",
        precio: 14500,
        desc: "Para fanáticos de los cómics, anime y películas en alta resolución.",
        esMuestra: true,
    },
    {
        id: 6,
        categoria: "personajes",
        nombre: "Busto Decorativo de Colección",
        emoji: "🗿",
        precio: 16000,
        desc: "Grandes detalles de personalidades, historia, gaming y más.",
        esMuestra: true,
    },
    {
        id: 7,
        categoria: "oficina",
        nombre: "Organizador de Escritorio",
        emoji: "📦",
        precio: 8900,
        desc: "Múltiples compartimentos para lapiceras, tarjetas y celular.",
        esMuestra: true,
    },
    {
        id: 8,
        categoria: "oficina",
        nombre: "Portanombre Personalizado",
        emoji: "📝",
        precio: 6500,
        desc: "Ideal para oficinas, escritorios profesionales o comercios.",
        esMuestra: true,
    },
    {
        id: 9,
        categoria: "hogar",
        nombre: "Maceta Geométrica",
        emoji: "🌱",
        precio: 7200,
        desc: "Diseño ideal para interiores y plantas pequeñas.",
        esMuestra: true,
    },
    {
        id: 10,
        categoria: "llaveros",
        nombre: "Llavero Personalizado",
        emoji: "🔑",
        precio: 2500,
        desc: "Tu marca, nombre o logo. Ventas por unidad y por mayor.",
        esMuestra: true,
    },
    {
        id: 11,
        categoria: "juguetes",
        nombre: "Juguete Articulado 3D",
        emoji: "🦎",
        precio: 8200,
        desc: "Articulados super divertidos y resistentes (Dragones, Lagartos, etc.).",
        esMuestra: true,
    },
    {
        id: 12,
        categoria: "medida",
        nombre: "Servicio de Impresión Personalizada",
        emoji: "⚙️",
        precio: 0,
        desc: "Imprimimos piezas funcionales e industriales según tu plano.",
        esMuestra: true,
    },
];

const FAQS = [
    {
        q: "¿Cuánto tarda una impresión?",
        a: "Depende del tamaño y complejidad de la pieza. Un producto estándar del catálogo tarda entre 24 y 48 hs hábiles, mientras que proyectos grandes personalizados pueden requerir de 3 a 5 días.",
    },
    {
        q: "¿Hacen impresiones personalizadas?",
        a: "¡Sí! Nos enviás el enlace, un archivo STL/OBJ o la idea que tengas y lo cotizamos. Nos encargamos de sugerir los mejores materiales y rellenos.",
    },
    {
        q: "¿A qué provincias envían?",
        a: "Realizamos envíos a toda la Argentina a través de Correo Argentino, Oca o el medio express que prefieras. Los envíos están embalados de forma sumamente segura.",
    },
    {
        q: "¿Qué materiales utilizan?",
        a: "Imprimimos principalmente en PLA y PLA+ (derivado de origen vegetal y biodegradable, ideal para decoración y mates), PETG (resistente al exterior y químicos) y Resina (para el nivel de detalle de figuras de colección).",
    },
    {
        q: "¿Cómo realizo una consulta de producto?",
        a: "Elegís el producto en el catálogo y tocás 'Consultar'. Te abrirá un chat de WhatsApp con un texto predefinido del artículo para responderte enseguida con precio y disponibilidad.",
    },
];

// Estado en memoria
let productosActivos = [];
let productosAdmin = [];
let filtroActual = "todos";
let apiConectada = false;
let sesionAdmin = cargarSesionAdmin();
let carritoPedido = cargarCarrito();

// ==========================================================================
// Inicialización
// ==========================================================================
document.addEventListener("DOMContentLoaded", async () => {
    // 1. Mostrar Skeleton Loaders de estructura inmediatamente
    renderSkeletonCategorias(8);
    renderSkeletonFiltros(9);
    renderSkeletonProductos(6);
    renderFaq();
    setupEventListeners();
    setupAdminListeners();
    setupCartListeners();
    actualizarUIEstadoAdmin();
    actualizarBadgeCarrito();

    // 2. Cargar Categorías y Filtros con transición suave
    setTimeout(() => {
        renderCategorias();
        renderFiltros();
    }, 280);

    // 3. Sincronizar Catálogo y Materiales desde la API .NET
    await Promise.all([
        cargarProductosDesdeAPI(),
        cargarMaterialesDesdeAPI(),
    ]);
});

// ==========================================================================
// Skeleton Loaders de Estructura
// ==========================================================================
function renderSkeletonCategorias(count = 8) {
    const container = document.getElementById("categories-container");
    if (!container) return;
    container.innerHTML = Array.from({ length: count })
        .map(
            () => `
        <div class="category-card skeleton-card">
            <div class="skeleton-circle"></div>
            <div class="skeleton-line medium" style="margin: 0 auto 0.6rem;"></div>
            <div class="skeleton-line short" style="margin: 0 auto;"></div>
        </div>
    `,
        )
        .join("");
}

function renderSkeletonFiltros(count = 8) {
    const container = document.getElementById("filter-chips-container");
    if (!container) return;
    container.innerHTML = Array.from({ length: count })
        .map(() => `<span class="skeleton-pill chip-skeleton"></span>`)
        .join("");
}

function renderSkeletonProductos(count = 6) {
    const container = document.getElementById("products-container");
    if (!container) return;
    container.innerHTML = Array.from({ length: count })
        .map(
            () => `
        <article class="product-card skeleton-card">
            <div class="skeleton-box skeleton-media"></div>
            <div class="product-details">
                <div class="skeleton-line short"></div>
                <div class="skeleton-line medium"></div>
                <div class="skeleton-line muted"></div>
                <div class="skeleton-line short" style="margin-bottom: 1.5rem;"></div>
                <div class="product-action">
                    <div class="skeleton-line short" style="width: 35%; margin: 0;"></div>
                    <div class="skeleton-pill"></div>
                </div>
            </div>
        </article>
    `,
        )
        .join("");
}

function renderSkeletonAdminCards(containerId, count = 4) {
    const container = document.getElementById(containerId);
    if (!container) return;
    container.innerHTML = Array.from({ length: count })
        .map(
            () => `
        <div class="admin-item-card skeleton-card">
            <div class="admin-item-top">
                <div class="skeleton-box" style="width: 44px; height: 44px; border-radius: 10px;"></div>
                <div class="skeleton-pill" style="width: 70px; height: 22px;"></div>
            </div>
            <div class="skeleton-line medium"></div>
            <div class="skeleton-line muted"></div>
            <div class="skeleton-line short"></div>
        </div>
    `,
        )
        .join("");
}

// ==========================================================================
// Helpers de Metadatos de Producto (Categoría, Emoji, Imagen URL)
// ==========================================================================
function serializarDescripcionProducto(categoria, emoji, imgUrl, descLimpia) {
    const cat = (categoria || "medida").trim();
    const icon = (emoji || "🧊").trim();
    const img = (imgUrl || "").trim();
    return `[cat:${cat}|icon:${icon}|img:${img}] ${descLimpia.trim()}`;
}

function parsearProductoAPI(p) {
    const id = p.idProducto ?? p.IdProducto ?? p.id_producto ?? p.id;
    const idModelo = p.id_modelo3d ?? p.idModelo3d ?? 0;
    const nombre = p.nombre ?? p.Nombre ?? "Artículo 3D";
    const rawDesc = p.descripcion ?? p.Descripcion ?? "";
    const precio = Number(p.precio_venta ?? p.precioVenta ?? p.precio ?? 0);
    const activo = p.activo ?? p.Activo ?? true;
    const fechaAlta = p.fecha_alta ?? p.fechaAlta ?? new Date().toISOString();

    let categoria = "";
    let emoji = "";
    let imgUrl = "";
    let desc = rawDesc;

    const match = rawDesc.match(/^\[cat:([^|]*)\|icon:([^|]*)\|img:([^\]]*)\]\s*(.*)$/s);
    if (match) {
        categoria = match[1] || "medida";
        emoji = match[2] || "🧊";
        imgUrl = match[3] || "";
        desc = match[4] || "";
    } else {
        // Inferencia automática si fue cargado directo en la BD
        const texto = `${nombre} ${rawDesc}`.toLowerCase();
        if (texto.includes("soporte") || texto.includes("auricular") || texto.includes("cable")) {
            categoria = "soportes";
            emoji = "🎧";
        } else if (texto.includes("mate") || texto.includes("yerb")) {
            categoria = "mates";
            emoji = "🧉";
        } else if (texto.includes("busto") || texto.includes("figura") || texto.includes("personaje")) {
            categoria = "personajes";
            emoji = "🦸‍♂️";
        } else if (texto.includes("oficina") || texto.includes("escritorio") || texto.includes("portanombre")) {
            categoria = "oficina";
            emoji = "📦";
        } else if (texto.includes("maceta") || texto.includes("hogar") || texto.includes("decora")) {
            categoria = "hogar";
            emoji = "🌱";
        } else if (texto.includes("llavero")) {
            categoria = "llaveros";
            emoji = "🔑";
        } else if (texto.includes("juguete") || texto.includes("articulado") || texto.includes("drag")) {
            categoria = "juguetes";
            emoji = "🦎";
        } else {
            categoria = "medida";
            emoji = "🧊";
        }
    }

    return {
        id,
        id_modelo3d: idModelo,
        nombre,
        categoria,
        emoji,
        imgUrl,
        desc: desc || "Impresión 3D personalizada de alta precisión.",
        precio,
        activo,
        fecha_alta: fechaAlta,
        esMuestra: false,
    };
}

// ==========================================================================
// Sincronización con API .NET 10
// ==========================================================================
async function cargarProductosDesdeAPI() {
    renderSkeletonProductos(6);

    try {
        const res = await fetch(`${API_BASE_URL}/productos`);
        if (!res.ok) throw new Error(`HTTP ${res.status}`);

        const data = await res.json();
        apiConectada = true;
        actualizarBadgeAPI(true);

        const mapeados = Array.isArray(data) ? data.map(parsearProductoAPI) : [];
        productosAdmin = mapeados;

        const activosBD = mapeados.filter((p) => p.activo);
        if (activosBD.length > 0) {
            productosActivos = activosBD;
        } else {
            // Si aún no hay artículos cargados en la BD, mostramos el catálogo base
            productosActivos = PRODUCTOS_MUESTRA;
        }
    } catch {
        apiConectada = false;
        actualizarBadgeAPI(false);
        productosActivos = PRODUCTOS_MUESTRA;
    }

    renderProductos(filtroActual);
}

async function cargarMaterialesDesdeAPI() {
    try {
        const res = await fetch(`${API_BASE_URL}/materiales`);
        if (!res.ok) return;

        const materiales = await res.json();
        if (!Array.isArray(materiales) || materiales.length === 0) return;

        const activos = materiales.filter((m) => m.activo !== false);
        if (activos.length === 0) return;

        const selectMat = document.getElementById("material");
        if (selectMat) {
            selectMat.innerHTML = activos
                .map((m) => {
                    const detalle = [m.tipo, m.color].filter(Boolean).join(" - ");
                    return `<option value="${escapeHtml(m.nombre)}">${escapeHtml(m.nombre)}${detalle ? ` (${escapeHtml(detalle)})` : ""}</option>`;
                })
                .join("");
        }
    } catch {
        // Mantiene las opciones por defecto del HTML (PLA, PETG, Resina)
    }
}

function actualizarBadgeAPI(online) {
    const badge = document.getElementById("api-status-badge");
    if (!badge) return;
    badge.className = `api-badge ${online ? "online" : "offline"}`;
    badge.innerHTML = `<span class="api-dot"></span> ${online ? "En línea" : "Sin conexión"}`;
}

// ==========================================================================
// Renderizado del Sitio Público
// ==========================================================================
function renderCategorias() {
    const container = document.getElementById("categories-container");
    if (!container) return;

    const cateData = CATEGORIAS.filter((c) => c.id !== "todos");

    container.innerHTML = cateData
        .map(
            (c) => `
    <a href="#catalogo" class="category-card" data-cat-link="${c.id}">
      <span class="category-icon">${c.icon}</span>
      <h3>${c.nombre}</h3>
      <p>${c.desc}</p>
    </a>
  `,
        )
        .join("");

    document.querySelectorAll("[data-cat-link]").forEach((card) => {
        card.addEventListener("click", () => {
            const catId = card.getAttribute("data-cat-link");
            filtroActual = catId;
            setActiveChip(catId);
            renderProductosConSkeleton(catId);
        });
    });
}

function renderFiltros() {
    const container = document.getElementById("filter-chips-container");
    if (!container) return;

    container.innerHTML = CATEGORIAS.map(
        (c) => `
    <button class="chip ${c.id === filtroActual ? "active" : ""}" data-filter="${c.id}">
      ${c.icon} ${c.nombre}
    </button>
  `,
    ).join("");

    document.querySelectorAll("[data-filter]").forEach((chip) => {
        chip.addEventListener("click", () => {
            const filterValue = chip.getAttribute("data-filter");
            filtroActual = filterValue;
            setActiveChip(filterValue);
            renderProductosConSkeleton(filterValue);
        });
    });
}

function setActiveChip(catId) {
    document.querySelectorAll("[data-filter]").forEach((chip) => {
        if (chip.getAttribute("data-filter") === catId) {
            chip.classList.add("active");
        } else {
            chip.classList.remove("active");
        }
    });
}

function renderProductosConSkeleton(filter) {
    renderSkeletonProductos(3);
    setTimeout(() => {
        renderProductos(filter);
    }, 180);
}

function renderProductos(filter) {
    const container = document.getElementById("products-container");
    if (!container) return;

    const listaBase =
        productosActivos.length > 0 ? productosActivos : PRODUCTOS_MUESTRA;

    const filteredData =
        filter === "todos"
            ? listaBase
            : listaBase.filter((p) => p.categoria === filter);

    if (filteredData.length === 0) {
        container.innerHTML = `<p style="text-align: center; grid-column: 1/-1; color: var(--text-muted); padding: 2rem;">Pronto sumaremos más productos a esta categoría...</p>`;
        return;
    }

    container.innerHTML = filteredData
        .map((p) => {
            const categoriaInfo = CATEGORIAS.find((c) => c.id === p.categoria);
            const precioFormateado =
                p.precio > 0
                    ? `$${p.precio.toLocaleString("es-AR")}`
                    : "Personalizable";

            const mensajeWSP = encodeURIComponent(
                `¡Hola Innova 3D! Quería consultar por el producto "${p.nombre}"${p.precio > 0 ? ` (${precioFormateado})` : ""}. ¿Me dirías disponibilidad y tiempos estimados? Muchas gracias.`,
            );

            const mediaContent = p.imgUrl
                ? `<img src="${escapeHtml(p.imgUrl)}" alt="${escapeHtml(p.nombre)}" class="product-photo" onerror="this.outerHTML='<span>${p.emoji || "🧊"}</span>'" />`
                : `<span>${p.emoji || "🧊"}</span>`;

            return `
      <article class="product-card">
        <div class="product-media">
          ${mediaContent}
          <span class="product-badge">${categoriaInfo ? categoriaInfo.nombre : "3D"}</span>
        </div>
        <div class="product-details">
          <span class="product-category">${categoriaInfo ? categoriaInfo.nombre : "Gama 3D"}</span>
          <h3 class="product-title">${escapeHtml(p.nombre)}</h3>
          <p class="product-desc">${escapeHtml(p.desc)}</p>
          <div class="product-action">
            <span class="product-price-tag">${precioFormateado}</span>
            <div class="product-buttons-group">
              <button type="button" class="btn-add-cart" data-add-cart="${p.id}">
                <i class="fa-solid fa-cart-plus"></i> Agregar
              </button>
              <a href="https://wa.me/${WHATSAPP_NUMBER}?text=${mensajeWSP}" target="_blank" class="btn-consult btn-wa-mini" title="Consultar directo por WhatsApp">
                <i class="fa-brands fa-whatsapp"></i>
              </a>
            </div>
          </div>
        </div>
      </article>
    `;
        })
        .join("");

    container.querySelectorAll("[data-add-cart]").forEach((btn) => {
        btn.addEventListener("click", () => {
            const id = Number(btn.getAttribute("data-add-cart"));
            const prod = listaBase.find((x) => Number(x.id) === id);
            if (prod) agregarAlCarrito(prod);
        });
    });
}

function renderFaq() {
    const container = document.getElementById("faq-container");
    if (!container) return;

    container.innerHTML = FAQS.map(
        (faq, index) => `
    <div class="faq-item">
      <div class="faq-header" data-index="${index}">
        <h3>${faq.q}</h3>
        <i class="fa-solid fa-plus faq-icon"></i>
      </div>
      <div class="faq-content">
        <p>${faq.a}</p>
      </div>
    </div>
  `,
    ).join("");

    document.querySelectorAll(".faq-header").forEach((header) => {
        header.addEventListener("click", () => {
            const faqItem = header.parentElement;
            const content = faqItem.querySelector(".faq-content");
            const isActive = faqItem.classList.contains("active");

            document.querySelectorAll(".faq-item").forEach((item) => {
                item.classList.remove("active");
                item.querySelector(".faq-content").style.maxHeight = null;
            });

            if (!isActive) {
                faqItem.classList.add("active");
                content.style.maxHeight = content.scrollHeight + "px";
            }
        });
    });
}

function setupEventListeners() {
    const form = document.getElementById("contact-form");
    if (form) {
        form.addEventListener("submit", async (e) => {
            e.preventDefault();

            const nombre = document.getElementById("nombre").value.trim();
            const material = document.getElementById("material").value;
            const link = document.getElementById("link").value.trim();
            const mensaje = document.getElementById("mensaje").value.trim();

            // Si la API está activa, registramos también la solicitud de modelo 3D en segundo plano
            if (apiConectada && link) {
                try {
                    await fetch(`${API_BASE_URL}/modelo3d`, {
                        method: "POST",
                        headers: { "Content-Type": "application/json" },
                        body: JSON.stringify({
                            nombre: `Cotización Web - ${nombre}`.slice(0, 50),
                            archivo: link.slice(0, 250),
                            formato: "STL",
                            descripcion: `Material: ${material} | ${mensaje || "Sin detalles adicionales"}`.slice(0, 250),
                        }),
                    });
                } catch {
                    // Continúa normalmente hacia WhatsApp
                }
            }

            const textoForm =
                `*¡Nueva Cotización Innova 3D!* \n\n` +
                `*Nombre:* ${nombre}\n` +
                `*Material sugerido:* ${material}\n` +
                `*Link del modelo:* ${link}\n` +
                `*Detalles:* ${mensaje || "A coordinar"}`;

            const urlWSP = `https://wa.me/${WHATSAPP_NUMBER}?text=${encodeURIComponent(textoForm)}`;
            window.open(urlWSP, "_blank");
            form.reset();
            mostrarToast("Abriendo consulta en WhatsApp...");
        });
    }

    const sections = document.querySelectorAll("section");
    const navLinks = document.querySelectorAll("nav a");

    window.addEventListener("scroll", () => {
        let current = "";
        sections.forEach((section) => {
            const sectionTop = section.offsetTop;
            if (window.scrollY >= sectionTop - 150) {
                current = section.getAttribute("id");
            }
        });

        navLinks.forEach((link) => {
            link.classList.remove("active");
            if (current && link.getAttribute("href").includes(current)) {
                link.classList.add("active");
            }
        });
    });
}

// ==========================================================================
// Autenticación JWT (Iniciar Sesión / Registrarse) y Panel de Gestión
// ==========================================================================
function cargarSesionAdmin() {
    try {
        const raw = sessionStorage.getItem("innova3d_user_session");
        return raw ? JSON.parse(raw) : null;
    } catch {
        return null;
    }
}

function guardarSesionAdmin(sesion) {
    sesionAdmin = sesion;
    if (sesion) {
        sessionStorage.setItem("innova3d_user_session", JSON.stringify(sesion));
    } else {
        sessionStorage.removeItem("innova3d_user_session");
    }
    actualizarUIEstadoAdmin();
}

function esUsuarioAdministrador(sesion) {
    if (!sesion || !sesion.token) return false;
    if (
        ALLOWED_ADMIN_USERNAME &&
        sesion.usuario?.toLowerCase() !== ALLOWED_ADMIN_USERNAME.toLowerCase()
    ) {
        return false;
    }
    if (sesion.esAdmin === true) return true;
    const roles = Array.isArray(sesion.roles) ? sesion.roles : [];
    return roles.some((r) => String(r).toLowerCase() === "administrador");
}

function actualizarUIEstadoAdmin() {
    const guestActions = document.getElementById("auth-guest-actions");
    const userActions = document.getElementById("auth-user-actions");
    const userDisplay = document.getElementById("nav-user-display");
    const btnOpenAdminPanel = document.getElementById("btn-open-admin-panel");
    const currentUserSpan = document.getElementById("admin-current-user");
    const inputCotizarNombre = document.getElementById("nombre");

    if (!guestActions || !userActions) return;

    if (sesionAdmin && sesionAdmin.token) {
        guestActions.style.display = "none";
        userActions.style.display = "inline-flex";

        const nombreMostrar = sesionAdmin.nombreCompleto || sesionAdmin.usuario;
        if (userDisplay) userDisplay.textContent = nombreMostrar;
        if (currentUserSpan) currentUserSpan.textContent = sesionAdmin.usuario;

        if (inputCotizarNombre && !inputCotizarNombre.value) {
            inputCotizarNombre.value = nombreMostrar;
        }

        const esAdmin = esUsuarioAdministrador(sesionAdmin);
        if (btnOpenAdminPanel) {
            btnOpenAdminPanel.style.display = esAdmin ? "inline-flex" : "none";
        }
    } else {
        guestActions.style.display = "inline-flex";
        userActions.style.display = "none";
        if (btnOpenAdminPanel) {
            btnOpenAdminPanel.style.display = "none";
        }
    }
}

function cambiarModoAuthModal(modo) {
    const title = document.getElementById("auth-modal-title");
    const loginView = document.getElementById("auth-view-login");
    const registerView = document.getElementById("auth-view-register");

    document.querySelectorAll("[data-auth-mode]").forEach((btn) => {
        if (btn.getAttribute("data-auth-mode") === modo) {
            btn.classList.add("active");
        } else {
            btn.classList.remove("active");
        }
    });

    if (modo === "register") {
        if (title) title.textContent = "Crear Cuenta en Innova 3D";
        loginView?.classList.remove("active");
        registerView?.classList.add("active");
    } else {
        if (title) title.textContent = "Iniciar Sesión";
        registerView?.classList.remove("active");
        loginView?.classList.add("active");
    }
}

function abrirModal(id) {
    const modal = document.getElementById(id);
    if (modal) {
        modal.classList.add("open");
        modal.setAttribute("aria-hidden", "false");
    }
}

function cerrarModal(id) {
    const modal = document.getElementById(id);
    if (modal) {
        modal.classList.remove("open");
        modal.setAttribute("aria-hidden", "true");
    }
}

function setupAdminListeners() {
    document.getElementById("btn-open-login")?.addEventListener("click", () => {
        cambiarModoAuthModal("login");
        abrirModal("auth-modal");
    });

    document.getElementById("btn-open-register")?.addEventListener("click", () => {
        cambiarModoAuthModal("register");
        abrirModal("auth-modal");
    });

    document.querySelectorAll("[data-auth-mode]").forEach((btn) => {
        btn.addEventListener("click", () => {
            cambiarModoAuthModal(btn.getAttribute("data-auth-mode"));
        });
    });

    document.getElementById("btn-open-admin-panel")?.addEventListener("click", () => {
        if (esUsuarioAdministrador(sesionAdmin)) {
            abrirPanelAdministrador();
        }
    });

    document.querySelectorAll("[data-close-modal]").forEach((btn) => {
        btn.addEventListener("click", () => {
            cerrarModal(btn.getAttribute("data-close-modal"));
        });
    });

    document.querySelectorAll(".admin-modal-overlay").forEach((overlay) => {
        overlay.addEventListener("click", (e) => {
            if (e.target === overlay) {
                overlay.classList.remove("open");
            }
        });
    });

    // Formularios de Login y Registro
    document
        .getElementById("auth-login-form")
        ?.addEventListener("submit", handleUserLogin);
    document
        .getElementById("auth-register-form")
        ?.addEventListener("submit", handleUserRegister);

    // Logout (desde Header o desde Panel Admin)
    const cerrarSesionHandler = () => {
        guardarSesionAdmin(null);
        cerrarModal("admin-panel-modal");
        mostrarToast("Sesión cerrada correctamente.");
    };

    document
        .getElementById("btn-header-logout")
        ?.addEventListener("click", cerrarSesionHandler);
    document
        .getElementById("btn-admin-logout")
        ?.addEventListener("click", cerrarSesionHandler);

    // Pestañas del Panel
    document.querySelectorAll("[data-admin-tab]").forEach((tab) => {
        tab.addEventListener("click", () => {
            const target = tab.getAttribute("data-admin-tab");
            document
                .querySelectorAll("[data-admin-tab]")
                .forEach((t) => t.classList.remove("active"));
            document
                .querySelectorAll(".admin-tab-content")
                .forEach((c) => c.classList.remove("active"));

            tab.classList.add("active");
            const panel = document.getElementById(`tab-${target}`);
            if (panel) panel.classList.add("active");

            if (target === "productos") cargarAdminProductos();
            if (target === "materiales") cargarAdminMateriales();
            if (target === "impresoras") cargarAdminImpresoras();
            if (target === "modelos") cargarAdminModelosYPedidos();
            if (target === "taller") cargarAdminTaller();
            if (target === "clientes") cargarAdminClientesYRoles();
        });
    });

    // Formularios CRUD
    document
        .getElementById("admin-producto-form")
        ?.addEventListener("submit", handleGuardarProducto);
    document
        .getElementById("admin-prod-mat-form")
        ?.addEventListener("submit", handleAsignarProductoMaterial);
    document
        .getElementById("btn-cancel-prod")
        ?.addEventListener("click", resetFormularioProducto);
    document
        .getElementById("btn-refresh-prod")
        ?.addEventListener("click", cargarAdminProductos);
    document
        .getElementById("btn-seed-catalog")
        ?.addEventListener("click", handleSeedCatalogoInicial);

    document
        .getElementById("admin-material-form")
        ?.addEventListener("submit", handleGuardarMaterial);
    document
        .getElementById("admin-mov-mat-form")
        ?.addEventListener("submit", handleGuardarMovimientoMaterial);
    document
        .getElementById("btn-refresh-mat")
        ?.addEventListener("click", cargarAdminMateriales);

    document
        .getElementById("admin-impresora-form")
        ?.addEventListener("submit", handleGuardarImpresora);
    document
        .getElementById("admin-imp-mat-form")
        ?.addEventListener("submit", handleGuardarImpresoraMaterial);
    document
        .getElementById("btn-refresh-imp")
        ?.addEventListener("click", cargarAdminImpresoras);

    document
        .getElementById("admin-modelo-form")
        ?.addEventListener("submit", handleGuardarModelo3D);
    document
        .getElementById("btn-refresh-mod")
        ?.addEventListener("click", cargarAdminModelosYPedidos);

    document
        .getElementById("admin-trabajo-form")
        ?.addEventListener("submit", handleGuardarTrabajoImpresion);
    document
        .getElementById("admin-consumo-form")
        ?.addEventListener("submit", handleGuardarConsumoMaterial);
    document
        .getElementById("btn-refresh-taller")
        ?.addEventListener("click", cargarAdminTaller);

    document
        .getElementById("admin-buscar-persona-form")
        ?.addEventListener("submit", handleBuscarPersonaAdmin);
    document
        .getElementById("btn-per-ver-todos")
        ?.addEventListener("click", () => cargarAdminClientesYRoles(""));
    document
        .getElementById("admin-usuariorol-form")
        ?.addEventListener("submit", handleAsignarRolUsuario);
    document
        .getElementById("btn-refresh-clientes")
        ?.addEventListener("click", () => cargarAdminClientesYRoles(""));
}

async function extraerMensajeError(res, fallback) {
    try {
        const text = await res.text();
        if (!text) return fallback;
        try {
            const json = JSON.parse(text);
            if (Array.isArray(json.errores) && json.errores.length > 0) {
                return json.errores.join(" ");
            }
            if (Array.isArray(json.Errores) && json.Errores.length > 0) {
                return json.Errores.join(" ");
            }
            return json.mensaje || json.Mensaje || text;
        } catch {
            return text;
        }
    } catch {
        return fallback;
    }
}

async function handleUserLogin(e) {
    e.preventDefault();
    const errorBox = document.getElementById("auth-login-error");
    const submitBtn = document.getElementById("auth-login-submit");
    const usuario = document.getElementById("login-username").value.trim();
    const password = document.getElementById("login-password").value;

    errorBox.style.display = "none";
    submitBtn.disabled = true;
    submitBtn.innerHTML = `<i class="fa-solid fa-circle-notch fa-spin"></i> Ingresando...`;

    try {
        const res = await fetch(`${API_BASE_URL}/usuarios/login`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                usuario: usuario,
                passwordHash: password,
            }),
        });

        if (!res.ok) {
            const msg = await extraerMensajeError(
                res,
                "Usuario o contraseña incorrectos.",
            );
            throw new Error(msg);
        }

        const data = await res.json();
        const token = data.token ?? data.Token;
        const nombreUsuario = data.usuario ?? data.Usuario ?? usuario;
        const roles = data.roles ?? data.Roles ?? [];
        const esAdmin =
            data.esAdmin ??
            data.EsAdmin ??
            roles.some((r) => String(r).toLowerCase() === "administrador");

        const nuevaSesion = {
            token,
            usuario: nombreUsuario,
            roles,
            esAdmin,
        };

        guardarSesionAdmin(nuevaSesion);
        document.getElementById("auth-login-form").reset();
        cerrarModal("auth-modal");

        if (esUsuarioAdministrador(nuevaSesion)) {
            mostrarToast(`¡Bienvenido al Panel de Gestión, ${nombreUsuario}!`);
            abrirPanelAdministrador();
        } else {
            mostrarToast(`¡Hola de nuevo, ${nombreUsuario}!`);
        }
    } catch (err) {
        errorBox.textContent =
            err.message || "Error al conectar con el servidor.";
        errorBox.style.display = "block";
    } finally {
        submitBtn.disabled = false;
        submitBtn.innerHTML = `<i class="fa-solid fa-right-to-bracket"></i> Iniciar Sesión`;
    }
}

async function handleUserRegister(e) {
    e.preventDefault();
    const errorBox = document.getElementById("auth-register-error");
    const submitBtn = document.getElementById("auth-register-submit");

    const payload = {
        nombre: document.getElementById("reg-nombre").value.trim(),
        apellido: document.getElementById("reg-apellido").value.trim(),
        dni: document.getElementById("reg-dni").value.trim(),
        telefono: document.getElementById("reg-telefono").value.trim() || null,
        email: document.getElementById("reg-email").value.trim() || null,
        direccion: document.getElementById("reg-direccion").value.trim() || null,
        usuario: document.getElementById("reg-usuario").value.trim(),
        password: document.getElementById("reg-password").value,
    };

    errorBox.style.display = "none";
    submitBtn.disabled = true;
    submitBtn.innerHTML = `<i class="fa-solid fa-circle-notch fa-spin"></i> Registrando...`;

    try {
        const res = await fetch(`${API_BASE_URL}/usuarios/registro`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(payload),
        });

        if (!res.ok) {
            const msg = await extraerMensajeError(
                res,
                "No se pudo completar el registro.",
            );
            throw new Error(msg);
        }

        const data = await res.json();
        const token = data.token ?? data.Token;
        const nombreUsuario = data.usuario ?? data.Usuario ?? payload.usuario;
        const nombreCompleto =
            data.nombreCompleto ??
            data.NombreCompleto ??
            `${payload.nombre} ${payload.apellido}`;
        const roles = data.roles ?? data.Roles ?? ["Cliente"];

        if (token) {
            guardarSesionAdmin({
                token,
                usuario: nombreUsuario,
                nombreCompleto,
                roles,
                esAdmin: false,
            });
        }

        document.getElementById("auth-register-form").reset();
        cerrarModal("auth-modal");
        mostrarToast(`¡Cuenta creada con éxito! Bienvenido, ${payload.nombre}.`);
    } catch (err) {
        errorBox.textContent =
            err.message || "No pudimos crear tu cuenta en este momento. Intentá nuevamente.";
        errorBox.style.display = "block";
    } finally {
        submitBtn.disabled = false;
        submitBtn.innerHTML = `<i class="fa-solid fa-user-plus"></i> Crear Cuenta`;
    }
}

// ==========================================================================
// Carrito de Pedido Directo a WhatsApp (Sin Billeteras ni Pasarelas Externas)
// ==========================================================================
function cargarCarrito() {
    try {
        const raw = localStorage.getItem("innova3d_cart_items");
        return raw ? JSON.parse(raw) : [];
    } catch {
        return [];
    }
}

function guardarCarrito() {
    localStorage.setItem("innova3d_cart_items", JSON.stringify(carritoPedido));
    actualizarBadgeCarrito();
}

function actualizarBadgeCarrito() {
    const badge = document.getElementById("cart-badge-count");
    if (!badge) return;
    const totalUnidades = carritoPedido.reduce(
        (acc, item) => acc + (Number(item.cantidad) || 1),
        0,
    );
    badge.textContent = String(totalUnidades);
}

function agregarAlCarrito(prod) {
    const idx = carritoPedido.findIndex(
        (item) =>
            Number(item.id) === Number(prod.id) &&
            item.nombre === prod.nombre,
    );

    if (idx >= 0) {
        carritoPedido[idx].cantidad += 1;
    } else {
        carritoPedido.push({
            id: prod.id,
            nombre: prod.nombre,
            precio: Number(prod.precio || 0),
            emoji: prod.emoji || "🧊",
            esMuestra: Boolean(prod.esMuestra),
            cantidad: 1,
        });
    }

    guardarCarrito();
    mostrarToast(`"${prod.nombre}" agregado a tu pedido.`);
}

function cambiarCantidadCarrito(index, delta) {
    if (!carritoPedido[index]) return;
    carritoPedido[index].cantidad += delta;
    if (carritoPedido[index].cantidad <= 0) {
        carritoPedido.splice(index, 1);
    }
    guardarCarrito();
    renderCarritoModal();
}

function vaciarCarrito() {
    carritoPedido = [];
    guardarCarrito();
    renderCarritoModal();
}

function renderCarritoModal() {
    const container = document.getElementById("cart-items-container");
    const checkoutBox = document.getElementById("cart-checkout-section");
    const totalEl = document.getElementById("cart-total-amount");
    const nameInput = document.getElementById("cart-client-name");

    if (!container) return;

    if (sesionAdmin && nameInput && !nameInput.value) {
        nameInput.value = sesionAdmin.nombreCompleto || sesionAdmin.usuario || "";
    }

    if (carritoPedido.length === 0) {
        container.innerHTML = `
            <div style="text-align: center; padding: 2.5rem 1rem; color: var(--text-muted);">
                <i class="fa-solid fa-cart-arrow-down" style="font-size: 2.5rem; color: var(--accent); margin-bottom: 0.85rem; display: block;"></i>
                <p>Tu pedido está vacío.</p>
                <p style="font-size: 0.82rem; margin-top: 0.35rem;">Explorá el catálogo y tocá <strong>"Agregar"</strong> en los artículos que quieras pedir por WhatsApp.</p>
            </div>
        `;
        if (checkoutBox) checkoutBox.style.display = "none";
        return;
    }

    if (checkoutBox) checkoutBox.style.display = "block";

    let totalPedido = 0;

    container.innerHTML = carritoPedido
        .map((item, idx) => {
            const subtotal = Number(item.precio || 0) * Number(item.cantidad || 1);
            totalPedido += subtotal;
            const precioTexto =
                item.precio > 0
                    ? `$${subtotal.toLocaleString("es-AR")} ($${Number(item.precio).toLocaleString("es-AR")} c/u)`
                    : "A cotizar";

            return `
            <div class="cart-item-row">
                <div class="cart-item-info">
                    <div class="cart-item-emoji">${item.emoji || "🧊"}</div>
                    <div>
                        <div class="cart-item-title">${escapeHtml(item.nombre)}</div>
                        <div class="cart-item-sub">${precioTexto}</div>
                    </div>
                </div>
                <div class="cart-qty-controls">
                    <button type="button" class="btn-qty" data-cart-idx="${idx}" data-cart-delta="-1">-</button>
                    <span class="cart-qty-num">${item.cantidad}</span>
                    <button type="button" class="btn-qty" data-cart-idx="${idx}" data-cart-delta="1">+</button>
                </div>
            </div>
        `;
        })
        .join("");

    if (totalEl) {
        totalEl.textContent =
            totalPedido > 0
                ? `$${totalPedido.toLocaleString("es-AR")}`
                : "A cotizar por WhatsApp";
    }

    container.querySelectorAll("[data-cart-idx]").forEach((btn) => {
        btn.addEventListener("click", () => {
            const idx = Number(btn.getAttribute("data-cart-idx"));
            const delta = Number(btn.getAttribute("data-cart-delta"));
            cambiarCantidadCarrito(idx, delta);
        });
    });
}

function setupCartListeners() {
    document.getElementById("btn-open-cart")?.addEventListener("click", () => {
        renderCarritoModal();
        abrirModal("cart-modal");
    });

    document.getElementById("btn-clear-cart")?.addEventListener("click", () => {
        vaciarCarrito();
        mostrarToast("Carrito vaciado.");
    });

    document
        .getElementById("cart-whatsapp-form")
        ?.addEventListener("submit", handleEnviarPedidoWhatsApp);
}

async function handleEnviarPedidoWhatsApp(e) {
    e.preventDefault();
    if (carritoPedido.length === 0) return;

    const clienteNombre = document
        .getElementById("cart-client-name")
        .value.trim();
    const notas = document.getElementById("cart-client-notes").value.trim();

    let totalEstimado = 0;
    const lineasProductos = carritoPedido.map((item) => {
        const sub = Number(item.precio || 0) * Number(item.cantidad || 1);
        totalEstimado += sub;
        const subTexto =
            item.precio > 0 ? `$${sub.toLocaleString("es-AR")}` : "A cotizar";
        return `• ${item.cantidad}x *${item.nombre}* (${subTexto})`;
    });

    // Registrar el pedido en la base de datos si la API está conectada
    if (apiConectada) {
        try {
            const totalRegistrar = totalEstimado > 0 ? totalEstimado : 1;
            const resPedido = await fetch(`${API_BASE_URL}/pedidos`, {
                method: "POST",
                headers: getAuthHeaders(),
                body: JSON.stringify({
                    id_cliente: 1,
                    id_estado: 1,
                    fecha_hora: new Date().toISOString(),
                    total_estimado: totalRegistrar,
                    subtotal: totalRegistrar,
                    descuento: 0,
                    total: totalRegistrar,
                }),
            });

            if (resPedido.ok) {
                const pedData = await resPedido.json();
                const idPedido = pedData.id ?? pedData.id_pedido;
                if (idPedido) {
                    for (const item of carritoPedido) {
                        if (!item.esMuestra && item.id > 0 && item.precio > 0) {
                            await fetch(`${API_BASE_URL}/detallepedido`, {
                                method: "POST",
                                headers: getAuthHeaders(),
                                body: JSON.stringify({
                                    id_pedido: idPedido,
                                    id_producto: item.id,
                                    cantidad: item.cantidad,
                                    precio_unitario: item.precio,
                                    subtotal: item.precio * item.cantidad,
                                }),
                            });
                        }
                    }
                }
            }
        } catch {
            // Continúa directamente hacia WhatsApp aunque falle el registro secundario
        }
    }

    const totalTexto =
        totalEstimado > 0
            ? `$${totalEstimado.toLocaleString("es-AR")}`
            : "A confirmar";

    const textoWhatsApp =
        `*¡Hola Innova 3D! Quiero realizar el siguiente pedido:*\n\n` +
        `*Cliente:* ${clienteNombre}` +
        `${sesionAdmin?.email ? ` (${sesionAdmin.email})` : ""}\n\n` +
        `*Detalle del Pedido:*\n` +
        `${lineasProductos.join("\n")}\n\n` +
        `*Total Estimado:* ${totalTexto}\n` +
        `${notas ? `*Aclaraciones / Envío:* ${notas}\n` : ""}\n` +
        `¿Me confirman disponibilidad y datos para coordinar? ¡Muchas gracias!`;

    const urlWSP = `https://wa.me/${WHATSAPP_NUMBER}?text=${encodeURIComponent(textoWhatsApp)}`;
    window.open(urlWSP, "_blank");

    vaciarCarrito();
    document.getElementById("cart-whatsapp-form")?.reset();
    cerrarModal("cart-modal");
    mostrarToast("Redirigiendo tu pedido al WhatsApp de Innova 3D...");
}

function abrirPanelAdministrador() {
    abrirModal("admin-panel-modal");
    cargarAdminProductos();
}

function getAuthHeaders() {
    const headers = { "Content-Type": "application/json" };
    if (sesionAdmin && sesionAdmin.token) {
        headers["Authorization"] = `Bearer ${sesionAdmin.token}`;
    }
    return headers;
}

// ==========================================================================
// Pestaña 1: Gestión de Artículos / Productos
// ==========================================================================
async function cargarAdminProductos() {
    renderSkeletonAdminCards("admin-productos-list", 4);
    await cargarProductosDesdeAPI();

    const container = document.getElementById("admin-productos-list");
    const countSpan = document.getElementById("admin-prod-count");
    if (!container) return;

    if (countSpan) countSpan.textContent = productosAdmin.length;

    if (productosAdmin.length === 0) {
        container.innerHTML = `
            <div style="grid-column: 1/-1; text-align: center; padding: 2.5rem; color: var(--text-muted);">
                <i class="fa-solid fa-box-open" style="font-size: 2.5rem; color: var(--accent); margin-bottom: 1rem; display: block;"></i>
                <p>Todavía no cargaste artículos en tu tienda.</p>
                <p style="font-size: 0.82rem; margin-top: 0.4rem;">Podés agregar uno desde el formulario izquierdo o tocar <strong>"Cargar Artículos de Ejemplo"</strong>.</p>
            </div>
        `;
        return;
    }

    container.innerHTML = productosAdmin
        .map((p) => {
            const catInfo = CATEGORIAS.find((c) => c.id === p.categoria);
            const iconHtml = p.imgUrl
                ? `<img src="${escapeHtml(p.imgUrl)}" alt="${escapeHtml(p.nombre)}" onerror="this.outerHTML='${p.emoji || "🧊"}'" />`
                : p.emoji || "🧊";

            return `
            <div class="admin-item-card ${p.activo ? "" : "inactive-item"}">
                <div class="admin-item-top">
                    <div class="admin-item-icon">${iconHtml}</div>
                    <span class="status-pill ${p.activo ? "active" : "inactive"}">
                        ${p.activo ? "Activo" : "Pausado"}
                    </span>
                </div>
                <div class="admin-item-title">${escapeHtml(p.nombre)}</div>
                <div class="admin-item-meta">
                    ${catInfo ? `${catInfo.icon} ${catInfo.nombre}` : "General"} · ID #${p.id}
                </div>
                <div class="admin-item-price">
                    $${p.precio.toLocaleString("es-AR")}
                </div>
                <p class="admin-item-meta" style="line-height: 1.4;">
                    ${escapeHtml(p.desc)}
                </p>
                <div class="admin-item-actions">
                    <button type="button" class="btn-action-chip" data-edit-prod="${p.id}">
                        <i class="fa-solid fa-pen"></i> Editar
                    </button>
                    <button type="button" class="btn-action-chip ${p.activo ? "danger" : ""}" data-toggle-prod="${p.id}" data-nuevo-estado="${!p.activo}">
                        <i class="fa-solid ${p.activo ? "fa-eye-slash" : "fa-eye"}"></i>
                        ${p.activo ? "Pausar" : "Activar"}
                    </button>
                </div>
            </div>
        `;
        })
        .join("");

    container.querySelectorAll("[data-edit-prod]").forEach((btn) => {
        btn.addEventListener("click", () => {
            const id = Number(btn.getAttribute("data-edit-prod"));
            const prod = productosAdmin.find((x) => x.id === id);
            if (prod) prepararEdicionProducto(prod);
        });
    });

    container.querySelectorAll("[data-toggle-prod]").forEach((btn) => {
        btn.addEventListener("click", async () => {
            const id = Number(btn.getAttribute("data-toggle-prod"));
            const nuevoEstado = btn.getAttribute("data-nuevo-estado") === "true";
            await cambiarEstadoProducto(id, nuevoEstado);
        });
    });
}

function prepararEdicionProducto(prod) {
    document.getElementById("producto-form-title").innerHTML = `
        <i class="fa-solid fa-pen-to-square" style="color: var(--accent)"></i>
        Editando Artículo #${prod.id}
    `;
    document.getElementById("prod-id").value = prod.id;
    document.getElementById("prod-activo").value = String(prod.activo);
    document.getElementById("prod-fecha-alta").value = prod.fecha_alta;
    document.getElementById("prod-nombre").value = prod.nombre;
    document.getElementById("prod-categoria").value = prod.categoria || "soportes";
    document.getElementById("prod-emoji").value = prod.emoji || "🧊";
    document.getElementById("prod-precio").value = prod.precio || 1000;
    document.getElementById("prod-modelo3d").value = prod.id_modelo3d || 0;
    document.getElementById("prod-img").value = prod.imgUrl || "";
    document.getElementById("prod-desc").value = prod.desc || "";
    document.getElementById("btn-cancel-prod").style.display = "inline-flex";
}

function resetFormularioProducto() {
    document.getElementById("admin-producto-form").reset();
    document.getElementById("prod-id").value = "";
    document.getElementById("prod-activo").value = "true";
    document.getElementById("prod-fecha-alta").value = "";
    document.getElementById("prod-emoji").value = "🧊";
    document.getElementById("prod-modelo3d").value = "0";
    document.getElementById("producto-form-title").innerHTML = `
        <i class="fa-solid fa-plus-circle" style="color: var(--accent)"></i>
        Nuevo Artículo
    `;
    document.getElementById("btn-cancel-prod").style.display = "none";
}

async function handleGuardarProducto(e) {
    e.preventDefault();

    const id = document.getElementById("prod-id").value;
    const nombre = document.getElementById("prod-nombre").value.trim();
    const categoria = document.getElementById("prod-categoria").value;
    const emoji = document.getElementById("prod-emoji").value.trim() || "🧊";
    const precio = Number(document.getElementById("prod-precio").value);
    const idModelo = Number(document.getElementById("prod-modelo3d").value || 0);
    const imgUrl = document.getElementById("prod-img").value.trim();
    const descLimpia = document.getElementById("prod-desc").value.trim();

    const descripcionSerializada = serializarDescripcionProducto(
        categoria,
        emoji,
        imgUrl,
        descLimpia,
    );

    try {
        let res;
        if (id) {
            const activo = document.getElementById("prod-activo").value === "true";
            const fechaAlta =
                document.getElementById("prod-fecha-alta").value ||
                new Date().toISOString();

            res = await fetch(`${API_BASE_URL}/productos/${id}`, {
                method: "PUT",
                headers: getAuthHeaders(),
                body: JSON.stringify({
                    id_modelo3d: idModelo,
                    nombre,
                    descripcion: descripcionSerializada,
                    precio_venta: precio,
                    activo,
                    fecha_alta: fechaAlta,
                }),
            });
        } else {
            res = await fetch(`${API_BASE_URL}/productos`, {
                method: "POST",
                headers: getAuthHeaders(),
                body: JSON.stringify({
                    id_modelo3d: idModelo,
                    nombre,
                    descripcion: descripcionSerializada,
                    precio_venta: precio,
                }),
            });
        }

        if (!res.ok) {
            const errText = await res.text();
            throw new Error(errText || "No se pudo guardar el producto.");
        }

        mostrarToast(
            id
                ? "Artículo actualizado correctamente."
                : "Nuevo artículo agregado al catálogo.",
        );
        resetFormularioProducto();
        await cargarAdminProductos();
    } catch (err) {
        mostrarToast(`Error: ${err.message}`);
    }
}

async function cambiarEstadoProducto(id, activo) {
    try {
        const res = await fetch(
            `${API_BASE_URL}/productos/${id}/estado?activo=${activo}`,
            {
                method: "PATCH",
                headers: getAuthHeaders(),
            },
        );
        if (!res.ok) throw new Error("No se pudo cambiar el estado.");

        mostrarToast(
            activo
                ? "Artículo activado en el catálogo."
                : "Artículo pausado del catálogo público.",
        );
        await cargarAdminProductos();
    } catch (err) {
        mostrarToast(`Error: ${err.message}`);
    }
}

async function handleSeedCatalogoInicial() {
    const btn = document.getElementById("btn-seed-catalog");
    if (btn) btn.disabled = true;
    mostrarToast("Cargando artículos de ejemplo...");

    try {
        for (const item of PRODUCTOS_MUESTRA) {
            const desc = serializarDescripcionProducto(
                item.categoria,
                item.emoji,
                "",
                item.desc,
            );
            await fetch(`${API_BASE_URL}/productos`, {
                method: "POST",
                headers: getAuthHeaders(),
                body: JSON.stringify({
                    id_modelo3d: 0,
                    nombre: item.nombre,
                    descripcion: desc,
                    precio_venta: item.precio > 0 ? item.precio : 5000,
                }),
            });
        }
        mostrarToast("¡Artículos de ejemplo publicados con éxito!");
        await cargarAdminProductos();
    } catch {
        mostrarToast("No se pudieron cargar los artículos de ejemplo.");
    } finally {
        if (btn) btn.disabled = false;
    }
}

// ==========================================================================
// Pestaña 2: Gestión de Materiales & Movimientos de Stock
// ==========================================================================
async function handleAsignarProductoMaterial(e) {
    e.preventDefault();
    const payload = {
        IdProducto: Number(document.getElementById("pm-id-prod").value),
        IdMaterial: Number(document.getElementById("pm-id-mat").value),
        cantidad_estimada: Number(document.getElementById("pm-cantidad").value || 85),
    };

    try {
        const res = await fetch(`${API_BASE_URL}/productomaterial`, {
            method: "POST",
            headers: getAuthHeaders(),
            body: JSON.stringify(payload),
        });
        if (!res.ok) {
            const msg = await extraerMensajeError(res, "No se pudo asociar el material al artículo.");
            throw new Error(msg);
        }
        document.getElementById("admin-prod-mat-form").reset();
        mostrarToast("Material asociado al artículo correctamente.");
    } catch (err) {
        mostrarToast(err.message);
    }
}

async function cargarAdminMateriales() {
    renderSkeletonAdminCards("admin-materiales-list", 3);
    const container = document.getElementById("admin-materiales-list");
    const countSpan = document.getElementById("admin-mat-count");
    const movCountSpan = document.getElementById("admin-mov-count");
    if (!container) return;

    try {
        const [resMat, resMov] = await Promise.all([
            fetch(`${API_BASE_URL}/materiales`),
            fetch(`${API_BASE_URL}/movimientomaterial`),
        ]);
        if (!resMat.ok) throw new Error("Error al obtener materiales");
        const materiales = await resMat.json();
        const movimientos = resMov.ok ? await resMov.json() : [];

        if (countSpan) countSpan.textContent = materiales.length;
        if (movCountSpan) movCountSpan.textContent = movimientos.length;
        await cargarMaterialesDesdeAPI();

        if (materiales.length === 0 && movimientos.length === 0) {
            container.innerHTML = `<p style="grid-column: 1/-1; text-align: center; color: var(--text-muted); padding: 2rem;">No hay materiales registrados aún.</p>`;
            return;
        }

        const htmlMateriales = materiales.map(
            (m) => `
            <div class="admin-item-card">
                <div class="admin-item-top">
                    <div class="admin-item-icon">🧵</div>
                    <span class="status-pill ${m.activo ? "active" : "inactive"}">
                        #${m.id_material} · ${escapeHtml(m.tipo || "Filamento")}
                    </span>
                </div>
                <div class="admin-item-title">${escapeHtml(m.nombre)}</div>
                <div class="admin-item-meta">
                    Color: <strong>${escapeHtml(m.color || "Estándar")}</strong> · Ø ${m.diametro || 1.75}mm (${escapeHtml(m.unidad_medida || "kg")})
                </div>
                <div class="admin-item-actions">
                    <button type="button" class="btn-action-chip danger" data-del-mat="${m.id_material}">
                        <i class="fa-solid fa-trash"></i> Eliminar
                    </button>
                </div>
            </div>
        `,
        );

        const htmlMovimientos = movimientos.map(
            (mv) => `
            <div class="admin-item-card">
                <div class="admin-item-top">
                    <div class="admin-item-icon">${String(mv.tipo_movimiento).toLowerCase() === "entrada" ? "📥" : "📤"}</div>
                    <span class="status-pill ${String(mv.tipo_movimiento).toLowerCase() === "entrada" ? "active" : "inactive"}">
                        ${escapeHtml(mv.tipo_movimiento)} · Mat #${mv.id_material}
                    </span>
                </div>
                <div class="admin-item-title">Cantidad: ${mv.cantidad}</div>
                <div class="admin-item-meta">${escapeHtml(mv.observacion || "Movimiento de stock")}</div>
                <div class="admin-item-actions">
                    <button type="button" class="btn-action-chip danger" data-del-mov="${mv.id_movimiento}">
                        <i class="fa-solid fa-trash"></i> Borrar
                    </button>
                </div>
            </div>
        `,
        );

        container.innerHTML = [...htmlMateriales, ...htmlMovimientos].join("");

        container.querySelectorAll("[data-del-mat]").forEach((btn) => {
            btn.addEventListener("click", async () => {
                const id = btn.getAttribute("data-del-mat");
                await fetch(`${API_BASE_URL}/materiales/${id}`, {
                    method: "DELETE",
                    headers: getAuthHeaders(),
                });
                mostrarToast("Material eliminado.");
                cargarAdminMateriales();
            });
        });

        container.querySelectorAll("[data-del-mov]").forEach((btn) => {
            btn.addEventListener("click", async () => {
                const id = btn.getAttribute("data-del-mov");
                await fetch(`${API_BASE_URL}/movimientomaterial/${id}`, {
                    method: "DELETE",
                    headers: getAuthHeaders(),
                });
                mostrarToast("Movimiento eliminado.");
                cargarAdminMateriales();
            });
        });
    } catch {
        container.innerHTML = `<p style="grid-column: 1/-1; text-align: center; color: var(--text-muted);">No se pudieron cargar los materiales en este momento.</p>`;
    }
}

async function handleGuardarMovimientoMaterial(e) {
    e.preventDefault();
    const payload = {
        id_material: Number(document.getElementById("mov-id-mat").value),
        tipo_movimiento: document.getElementById("mov-tipo").value,
        cantidad: Number(document.getElementById("mov-cantidad").value || 1),
        fecha_hora: new Date().toISOString(),
        observacion: document.getElementById("mov-obs").value.trim() || "Ajuste de stock en taller",
    };

    try {
        const res = await fetch(`${API_BASE_URL}/movimientomaterial`, {
            method: "POST",
            headers: getAuthHeaders(),
            body: JSON.stringify(payload),
        });
        if (!res.ok) {
            const msg = await extraerMensajeError(res, "Error al registrar movimiento de stock.");
            throw new Error(msg);
        }
        document.getElementById("admin-mov-mat-form").reset();
        mostrarToast("Movimiento de stock registrado.");
        await cargarAdminMateriales();
    } catch (err) {
        mostrarToast(err.message);
    }
}

async function handleGuardarMaterial(e) {
    e.preventDefault();
    const payload = {
        nombre: document.getElementById("mat-nombre").value.trim(),
        tipo: document.getElementById("mat-tipo").value,
        color: document.getElementById("mat-color").value.trim(),
        diametro: Number(document.getElementById("mat-diametro").value || 1.75),
        unidad_medida: document.getElementById("mat-unidad").value.trim() || "kg",
        activo: true,
    };

    try {
        const res = await fetch(`${API_BASE_URL}/materiales`, {
            method: "POST",
            headers: getAuthHeaders(),
            body: JSON.stringify(payload),
        });
        if (!res.ok) throw new Error("Error al registrar material.");
        document.getElementById("admin-material-form").reset();
        mostrarToast("Material registrado y actualizado en el cotizador.");
        await cargarAdminMateriales();
    } catch (err) {
        mostrarToast(`Error: ${err.message}`);
    }
}

// ==========================================================================
// Pestaña 3: Gestión de Impresoras 3D & Compatibilidad Impresora-Material
// ==========================================================================
async function cargarAdminImpresoras() {
    renderSkeletonAdminCards("admin-impresoras-list", 3);
    const container = document.getElementById("admin-impresoras-list");
    const countSpan = document.getElementById("admin-imp-count");
    if (!container) return;

    try {
        const [resImp, resImpMat] = await Promise.all([
            fetch(`${API_BASE_URL}/impresoras`),
            fetch(`${API_BASE_URL}/impresoramaterial`),
        ]);
        if (!resImp.ok) throw new Error("Error al obtener impresoras");
        const impresoras = await resImp.json();
        const compatibilidades = resImpMat.ok ? await resImpMat.json() : [];

        if (countSpan) countSpan.textContent = impresoras.length;

        if (impresoras.length === 0) {
            container.innerHTML = `<p style="grid-column: 1/-1; text-align: center; color: var(--text-muted); padding: 2rem;">No hay impresoras registradas en el taller.</p>`;
            return;
        }

        const htmlImpresoras = impresoras.map(
            (imp) => `
            <div class="admin-item-card ${imp.estado ? "" : "inactive-item"}">
                <div class="admin-item-top">
                    <div class="admin-item-icon">🖨️</div>
                    <span class="status-pill ${imp.estado ? "active" : "inactive"}">
                        #${imp.id_impresora} · ${imp.estado ? "Operativa" : "Mantenimiento"}
                    </span>
                </div>
                <div class="admin-item-title">${escapeHtml(imp.nombre)}</div>
                <div class="admin-item-meta">
                    ${escapeHtml(imp.marca)} ${escapeHtml(imp.modelo)} · <strong>${escapeHtml(imp.tecnologia)}</strong>
                </div>
                <div class="admin-item-meta">
                    Volumen: ${imp.volumen_x}x${imp.volumen_y}x${imp.volumen_z} mm
                </div>
                <div class="admin-item-actions">
                    <button type="button" class="btn-action-chip" data-toggle-imp="${imp.id_impresora}" data-imp-estado="${!imp.estado}">
                        <i class="fa-solid fa-power-off"></i>
                        ${imp.estado ? "Pasar a Mant." : "Habilitar"}
                    </button>
                </div>
            </div>
        `,
        );

        const htmlCompatibilidad = compatibilidades.map(
            (cm) => `
            <div class="admin-item-card">
                <div class="admin-item-top">
                    <div class="admin-item-icon">🔗</div>
                    <span class="status-pill active">Imp #${cm.id_impresora} ↔ Mat #${cm.id_material}</span>
                </div>
                <div class="admin-item-title">Perfil Compatible</div>
                <div class="admin-item-meta">${escapeHtml(cm.observacion || "Perfil estándar habilitado")}</div>
            </div>
        `,
        );

        container.innerHTML = [...htmlImpresoras, ...htmlCompatibilidad].join("");

        container.querySelectorAll("[data-toggle-imp]").forEach((btn) => {
            btn.addEventListener("click", async () => {
                const id = btn.getAttribute("data-toggle-imp");
                const estado = btn.getAttribute("data-imp-estado") === "true";
                await fetch(
                    `${API_BASE_URL}/impresoras/${id}/estado?estado=${estado}`,
                    {
                        method: "PATCH",
                        headers: getAuthHeaders(),
                    },
                );
                mostrarToast("Estado de impresora actualizado.");
                cargarAdminImpresoras();
            });
        });
    } catch {
        container.innerHTML = `<p style="grid-column: 1/-1; text-align: center; color: var(--text-muted);">No se pudieron cargar las impresoras en este momento.</p>`;
    }
}

async function handleGuardarImpresoraMaterial(e) {
    e.preventDefault();
    const payload = {
        id_impresora: Number(document.getElementById("im-id-imp").value),
        id_material: Number(document.getElementById("im-id-mat").value),
        observacion: document.getElementById("im-obs").value.trim() || "Compatible",
    };

    try {
        const res = await fetch(`${API_BASE_URL}/impresoramaterial`, {
            method: "POST",
            headers: getAuthHeaders(),
            body: JSON.stringify(payload),
        });
        if (!res.ok) {
            const msg = await extraerMensajeError(res, "Error al vincular compatibilidad.");
            throw new Error(msg);
        }
        document.getElementById("admin-imp-mat-form").reset();
        mostrarToast("Compatibilidad Impresora-Material vinculada.");
        await cargarAdminImpresoras();
    } catch (err) {
        mostrarToast(err.message);
    }
}

async function handleGuardarImpresora(e) {
    e.preventDefault();
    const vol = Number(document.getElementById("imp-vol").value || 250);
    const payload = {
        nombre: document.getElementById("imp-nombre").value.trim(),
        marca: document.getElementById("imp-marca").value.trim(),
        modelo: document.getElementById("imp-modelo").value.trim(),
        tecnologia: document.getElementById("imp-tec").value,
        volumen_x: vol,
        volumen_y: vol,
        volumen_z: vol,
        estado: true,
        fecha_alta: new Date().toISOString(),
    };

    try {
        const res = await fetch(`${API_BASE_URL}/impresoras`, {
            method: "POST",
            headers: getAuthHeaders(),
            body: JSON.stringify(payload),
        });
        if (!res.ok) throw new Error("Error al registrar impresora.");
        document.getElementById("admin-impresora-form").reset();
        mostrarToast("Impresora 3D agregada al parque.");
        await cargarAdminImpresoras();
    } catch (err) {
        mostrarToast(`Error: ${err.message}`);
    }
}

// ==========================================================================
// Pestaña 4: Modelos 3D, Pedidos & Estados de Pedido
// ==========================================================================
async function cargarAdminModelosYPedidos() {
    renderSkeletonAdminCards("admin-modelos-list", 3);
    const container = document.getElementById("admin-modelos-list");
    const modCount = document.getElementById("admin-mod-count");
    const pedCount = document.getElementById("admin-ped-count");
    if (!container) return;

    try {
        const [resMod, resPed, resEstadosPed, resImpresion] = await Promise.all([
            fetch(`${API_BASE_URL}/modelo3d`),
            fetch(`${API_BASE_URL}/pedidos`),
            fetch(`${API_BASE_URL}/estadopedido`),
            fetch(`${API_BASE_URL}/impresion`),
        ]);

        const modelos = resMod.ok ? await resMod.json() : (resImpresion.ok ? await resImpresion.json() : []);
        const pedidos = resPed.ok ? await resPed.json() : [];
        const estadosPedido = resEstadosPed.ok ? await resEstadosPed.json() : [];

        const mapaEstados = {};
        estadosPedido.forEach((ep) => {
            mapaEstados[ep.id_estado_pedido] = ep.nombre;
        });

        if (modCount) modCount.textContent = modelos.length;
        if (pedCount) pedCount.textContent = pedidos.length;

        if (modelos.length === 0 && pedidos.length === 0) {
            container.innerHTML = `<p style="grid-column: 1/-1; text-align: center; color: var(--text-muted); padding: 2rem;">No hay modelos 3D ni pedidos registrados.</p>`;
            return;
        }

        const htmlModelos = modelos.map(
            (m) => `
            <div class="admin-item-card">
                <div class="admin-item-top">
                    <div class="admin-item-icon">📐</div>
                    <span class="status-pill active">${escapeHtml(m.formato)} · ID #${m.id_modelo3d}</span>
                </div>
                <div class="admin-item-title">${escapeHtml(m.nombre)}</div>
                <div class="admin-item-meta">${escapeHtml(m.archivo)}</div>
                <div class="admin-item-meta">${escapeHtml(m.descripcion)}</div>
                <div class="admin-item-actions">
                    <button type="button" class="btn-action-chip danger" data-del-mod="${m.id_modelo3d}">
                        <i class="fa-solid fa-trash"></i> Eliminar
                    </button>
                </div>
            </div>
        `,
        );

        const htmlPedidos = pedidos.map(
            (p) => `
            <div class="admin-item-card">
                <div class="admin-item-top">
                    <div class="admin-item-icon">🧾</div>
                    <span class="status-pill active">Pedido #${p.id_pedido}</span>
                </div>
                <div class="admin-item-title">Cliente ID #${p.id_cliente}</div>
                <div class="admin-item-price">$${Number(p.total || 0).toLocaleString("es-AR")}</div>
                <div class="admin-item-meta">Estado: <strong>${escapeHtml(mapaEstados[p.id_estado] || `ID ${p.id_estado}`)}</strong></div>
            </div>
        `,
        );

        container.innerHTML = [...htmlModelos, ...htmlPedidos].join("");

        container.querySelectorAll("[data-del-mod]").forEach((btn) => {
            btn.addEventListener("click", async () => {
                const id = btn.getAttribute("data-del-mod");
                await fetch(`${API_BASE_URL}/modelo3d/${id}`, {
                    method: "DELETE",
                    headers: getAuthHeaders(),
                });
                mostrarToast("Modelo 3D eliminado.");
                cargarAdminModelosYPedidos();
            });
        });
    } catch {
        container.innerHTML = `<p style="grid-column: 1/-1; text-align: center; color: var(--text-muted);">Error al consultar modelos y pedidos.</p>`;
    }
}

async function handleGuardarModelo3D(e) {
    e.preventDefault();
    const payload = {
        nombre: document.getElementById("mod-nombre").value.trim(),
        formato: document.getElementById("mod-formato").value,
        archivo: document.getElementById("mod-archivo").value.trim(),
        descripcion: document.getElementById("mod-desc").value.trim(),
    };

    try {
        const res = await fetch(`${API_BASE_URL}/modelo3d`, {
            method: "POST",
            headers: getAuthHeaders(),
            body: JSON.stringify(payload),
        });
        if (!res.ok) throw new Error("Error al guardar modelo 3D.");
        document.getElementById("admin-modelo-form").reset();
        mostrarToast("Modelo 3D registrado correctamente.");
        await cargarAdminModelosYPedidos();
    } catch (err) {
        mostrarToast(`Error: ${err.message}`);
    }
}

// ==========================================================================
// Pestaña 5: Trabajos de Impresión, Estados de Impresión & Consumo de Material
// ==========================================================================
async function cargarAdminTaller() {
    renderSkeletonAdminCards("admin-taller-list", 3);
    const container = document.getElementById("admin-taller-list");
    const trabCount = document.getElementById("admin-trab-count");
    const consCount = document.getElementById("admin-cons-count");
    if (!container) return;

    try {
        const [resTrab, resCons, resEstImp] = await Promise.all([
            fetch(`${API_BASE_URL}/trabajoimpresion`),
            fetch(`${API_BASE_URL}/consumomaterial`),
            fetch(`${API_BASE_URL}/estadoimpresion`),
        ]);

        const trabajos = resTrab.ok ? await resTrab.json() : [];
        const consumos = resCons.ok ? await resCons.json() : [];
        const estadosImp = resEstImp.ok ? await resEstImp.json() : [];

        const mapaEstados = {};
        estadosImp.forEach((e) => {
            mapaEstados[e.id_estado_impresion] = e.nombre;
        });

        if (trabCount) trabCount.textContent = trabajos.length;
        if (consCount) consCount.textContent = consumos.length;

        if (trabajos.length === 0 && consumos.length === 0) {
            container.innerHTML = `<p style="grid-column: 1/-1; text-align: center; color: var(--text-muted); padding: 2rem;">No hay trabajos de impresión ni consumos registrados.</p>`;
            return;
        }

        const htmlTrabajos = trabajos.map(
            (t) => `
            <div class="admin-item-card">
                <div class="admin-item-top">
                    <div class="admin-item-icon">⚙️</div>
                    <span class="status-pill active">
                        Trabajo #${t.id_trabajo_impresion} · ${escapeHtml(mapaEstados[t.id_estado] || `Estado #${t.id_estado}`)}
                    </span>
                </div>
                <div class="admin-item-title">Pedido #${t.id_pedido} · Art. #${t.id_producto}</div>
                <div class="admin-item-meta">
                    Impresora #${t.id_impresora} · Mat #${t.id_material} · Cant: ${t.cantidad} (${t.peso_estimado}g)
                </div>
                <div class="admin-item-meta">${escapeHtml(t.observacion || "")}</div>
                <div class="admin-item-actions">
                    <button type="button" class="btn-action-chip danger" data-del-trab="${t.id_trabajo_impresion}">
                        <i class="fa-solid fa-trash"></i> Eliminar
                    </button>
                </div>
            </div>
        `,
        );

        const htmlConsumos = consumos.map(
            (c) => `
            <div class="admin-item-card">
                <div class="admin-item-top">
                    <div class="admin-item-icon">⚖️</div>
                    <span class="status-pill active">Consumo #${c.id_consumo_material}</span>
                </div>
                <div class="admin-item-title">Trabajo #${c.id_trabajo} · Material #${c.id_material}</div>
                <div class="admin-item-meta">Consumido: <strong>${c.cantidad} gr</strong></div>
                <div class="admin-item-actions">
                    <button type="button" class="btn-action-chip danger" data-del-cons="${c.id_consumo_material}">
                        <i class="fa-solid fa-trash"></i> Eliminar
                    </button>
                </div>
            </div>
        `,
        );

        container.innerHTML = [...htmlTrabajos, ...htmlConsumos].join("");

        container.querySelectorAll("[data-del-trab]").forEach((btn) => {
            btn.addEventListener("click", async () => {
                const id = btn.getAttribute("data-del-trab");
                await fetch(`${API_BASE_URL}/trabajoimpresion/${id}`, {
                    method: "DELETE",
                    headers: getAuthHeaders(),
                });
                mostrarToast("Trabajo de impresión eliminado.");
                cargarAdminTaller();
            });
        });

        container.querySelectorAll("[data-del-cons]").forEach((btn) => {
            btn.addEventListener("click", async () => {
                const id = btn.getAttribute("data-del-cons");
                await fetch(`${API_BASE_URL}/consumomaterial/${id}`, {
                    method: "DELETE",
                    headers: getAuthHeaders(),
                });
                mostrarToast("Registro de consumo eliminado.");
                cargarAdminTaller();
            });
        });
    } catch {
        container.innerHTML = `<p style="grid-column: 1/-1; text-align: center; color: var(--text-muted);">No se pudieron cargar los trabajos de taller.</p>`;
    }
}

async function handleGuardarTrabajoImpresion(e) {
    e.preventDefault();
    const payload = {
        id_pedido: Number(document.getElementById("trab-pedido").value),
        id_producto: Number(document.getElementById("trab-producto").value),
        id_impresora: Number(document.getElementById("trab-impresora").value),
        id_modelo: Number(document.getElementById("trab-modelo").value),
        id_material: Number(document.getElementById("trab-material").value),
        cantidad: Number(document.getElementById("trab-cantidad").value || 1),
        id_estado: Number(document.getElementById("trab-estado").value || 1),
        fecha_inicio: new Date().toISOString(),
        peso_estimado: Number(document.getElementById("trab-peso").value || 100),
        observacion: document.getElementById("trab-obs").value.trim(),
    };

    try {
        const res = await fetch(`${API_BASE_URL}/trabajoimpresion`, {
            method: "POST",
            headers: getAuthHeaders(),
            body: JSON.stringify(payload),
        });
        if (!res.ok) {
            const msg = await extraerMensajeError(res, "No se pudo programar el trabajo de impresión.");
            throw new Error(msg);
        }
        document.getElementById("admin-trabajo-form").reset();
        mostrarToast("Trabajo de impresión programado en el taller.");
        await cargarAdminTaller();
    } catch (err) {
        mostrarToast(err.message);
    }
}

async function handleGuardarConsumoMaterial(e) {
    e.preventDefault();
    const payload = {
        id_trabajo: Number(document.getElementById("cons-trabajo").value),
        id_material: Number(document.getElementById("cons-material").value),
        cantidad: Number(document.getElementById("cons-cantidad").value || 50),
        fecha_hora: new Date().toISOString(),
    };

    try {
        const res = await fetch(`${API_BASE_URL}/consumomaterial`, {
            method: "POST",
            headers: getAuthHeaders(),
            body: JSON.stringify(payload),
        });
        if (!res.ok) {
            const msg = await extraerMensajeError(res, "No se pudo registrar el consumo.");
            throw new Error(msg);
        }
        document.getElementById("admin-consumo-form").reset();
        mostrarToast("Consumo de filamento registrado.");
        await cargarAdminTaller();
    } catch (err) {
        mostrarToast(err.message);
    }
}

// ==========================================================================
// Pestaña 6: Personas, Clientes, Roles & Usuario-Rol
// ==========================================================================
async function cargarAdminClientesYRoles(terminoBusqueda = "") {
    renderSkeletonAdminCards("admin-clientes-list", 3);
    const container = document.getElementById("admin-clientes-list");
    const perCount = document.getElementById("admin-per-count");
    const cliCount = document.getElementById("admin-cli-count");
    const rolCount = document.getElementById("admin-rol-count");
    if (!container) return;

    try {
        const urlPersonas = terminoBusqueda
            ? `${API_BASE_URL}/personas/buscar?termino=${encodeURIComponent(terminoBusqueda)}`
            : `${API_BASE_URL}/personas`;

        const [resPer, resCli, resRol] = await Promise.all([
            fetch(urlPersonas),
            fetch(`${API_BASE_URL}/clientes`),
            fetch(`${API_BASE_URL}/roles`),
        ]);

        const personas = resPer.ok ? await resPer.json() : [];
        const clientes = resCli.ok ? await resCli.json() : [];
        const roles = resRol.ok ? await resRol.json() : [];

        if (perCount) perCount.textContent = personas.length;
        if (cliCount) cliCount.textContent = clientes.length;
        if (rolCount) rolCount.textContent = roles.length;

        const selectRol = document.getElementById("ur-id-rol");
        if (selectRol && roles.length > 0) {
            selectRol.innerHTML = roles
                .map(
                    (r) =>
                        `<option value="${r.idRol ?? r.id_rol}">${escapeHtml(r.nombre)}</option>`,
                )
                .join("");
        }

        if (personas.length === 0) {
            container.innerHTML = `<p style="grid-column: 1/-1; text-align: center; color: var(--text-muted); padding: 2rem;">No se encontraron personas con ese criterio.</p>`;
            return;
        }

        container.innerHTML = personas
            .map((p) => {
                const idPer = p.idPersona ?? p.id_persona;
                const activo = p.activo ?? true;
                return `
                <div class="admin-item-card ${activo ? "" : "inactive-item"}">
                    <div class="admin-item-top">
                        <div class="admin-item-icon">👤</div>
                        <span class="status-pill ${activo ? "active" : "inactive"}">
                            Persona #${idPer} · DNI ${escapeHtml(p.dni)}
                        </span>
                    </div>
                    <div class="admin-item-title">${escapeHtml(p.nombre)} ${escapeHtml(p.apellido)}</div>
                    <div class="admin-item-meta">
                        Tel: ${escapeHtml(p.telefono || "-")} · ${escapeHtml(p.email || "-")}
                    </div>
                    <div class="admin-item-meta">
                        Dirección: ${escapeHtml(p.direccion || "-")}
                    </div>
                </div>
            `;
            })
            .join("");
    } catch {
        container.innerHTML = `<p style="grid-column: 1/-1; text-align: center; color: var(--text-muted);">No se pudieron cargar los clientes y roles.</p>`;
    }
}

async function handleBuscarPersonaAdmin(e) {
    e.preventDefault();
    const termino = document.getElementById("per-buscar-input").value.trim();
    await cargarAdminClientesYRoles(termino);
}

async function handleAsignarRolUsuario(e) {
    e.preventDefault();
    const payload = {
        IdUsuario: Number(document.getElementById("ur-id-usuario").value),
        IdRol: Number(document.getElementById("ur-id-rol").value),
    };

    try {
        const res = await fetch(`${API_BASE_URL}/usuariorol`, {
            method: "POST",
            headers: getAuthHeaders(),
            body: JSON.stringify(payload),
        });
        if (!res.ok) {
            const msg = await extraerMensajeError(res, "No se pudo asignar el rol al usuario.");
            throw new Error(msg);
        }
        document.getElementById("admin-usuariorol-form").reset();
        mostrarToast("Rol asignado correctamente al usuario.");
    } catch (err) {
        mostrarToast(err.message);
    }
}

// ==========================================================================
// Utilidades
// ==========================================================================
function mostrarToast(mensaje) {
    const container = document.getElementById("toast-container");
    if (!container) return;

    const toast = document.createElement("div");
    toast.className = "toast-msg";
    toast.innerHTML = `<i class="fa-solid fa-circle-check" style="color: var(--accent)"></i> <span>${escapeHtml(mensaje)}</span>`;
    container.appendChild(toast);

    setTimeout(() => {
        toast.style.opacity = "0";
        toast.style.transition = "opacity 0.3s ease";
        setTimeout(() => toast.remove(), 300);
    }, 3200);
}

function escapeHtml(str) {
    return String(str ?? "")
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&#039;");
}
