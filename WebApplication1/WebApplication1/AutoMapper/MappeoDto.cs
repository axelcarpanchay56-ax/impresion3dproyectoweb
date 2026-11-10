using AutoMapper;
using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;

namespace impresion3d.Api.AutoMapper
{
    public class MappeoDto : Profile
    {
        public MappeoDto()
        {
            // Módulo 1: Personas, Clientes, Usuarios y Roles
            CreateMap<Persona, PersonaDto>();
            CreateMap<CrearPersonaDto, Persona>();
            CreateMap<ActualizarPersonaDto, Persona>();
            CreateMap<RegistroUsuarioDto, Persona>();

            CreateMap<Clientes, ClienteDto>()
                .ForMember(dest => dest.IdPersona, opt => opt.MapFrom(src => src.id_persona));
            CreateMap<CrearClienteDto, Clientes>()
                .ForMember(dest => dest.id_persona, opt => opt.MapFrom(src => src.IdPersona));

            CreateMap<Roles, RolDto>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.nombre))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.descripcion));

            CreateMap<Usuarios, UsuarioDto>()
                .ForMember(dest => dest.IdPersona, opt => opt.MapFrom(src => src.id_persona))
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.usuario))
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => src.activo))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.fecha_creacion))
                .ForMember(dest => dest.UltimoAcceso, opt => opt.MapFrom(src => src.ultimo_acceso));
            CreateMap<CrearUsuarioDto, Usuarios>()
                .ForMember(dest => dest.id_persona, opt => opt.MapFrom(src => src.IdPersona))
                .ForMember(dest => dest.usuario, opt => opt.MapFrom(src => src.Usuario))
                .ForMember(dest => dest.password_hash, opt => opt.MapFrom(src => src.PasswordHash));

            CreateMap<Usuario_Rol, UsuarioRolDto>()
                .ForMember(dest => dest.IdUsuario, opt => opt.MapFrom(src => src.id_usuario))
                .ForMember(dest => dest.IdRol, opt => opt.MapFrom(src => src.id_rol));
            CreateMap<AsignarUsuarioRolDto, Usuario_Rol>()
                .ForMember(dest => dest.id_usuario, opt => opt.MapFrom(src => src.IdUsuario))
                .ForMember(dest => dest.id_rol, opt => opt.MapFrom(src => src.IdRol));

            // Módulo 2: Catálogo e Inventario
            CreateMap<Productos, ProductoDto>();
            CreateMap<CrearProductoDto, Productos>();
            CreateMap<ActualizarProductoDto, Productos>();

            CreateMap<ProductoMaterial, ProductoMaterialDto>();
            CreateMap<AsignarProductoMaterialDto, ProductoMaterial>();
            CreateMap<ActualizarProductoMaterialDto, ProductoMaterial>();

            CreateMap<MovimientoMaterial, MovimientoMaterialDto>();
            CreateMap<CrearMovimientoMaterialDto, MovimientoMaterial>();
            CreateMap<ActualizarMovimientoMaterialDto, MovimientoMaterial>();

            // Módulo 3: Impresión 3D
            CreateMap<Modelo3D, Modelo3DDto>();
            CreateMap<Modelo3DCrearDto, Modelo3D>();
            CreateMap<Modelo3DActualizacionDto, Modelo3D>();

            CreateMap<Materiales, MaterialesDto>();
            CreateMap<MaterialesCrearDto, Materiales>();
            CreateMap<MaterialesActualizarDto, Materiales>();

            CreateMap<Impresoras, ImpresoraDto>();
            CreateMap<ImpresoraCrearDto, Impresoras>();
            CreateMap<ImpresoraActualizarDto, Impresoras>();

            CreateMap<ImpresoraMaterial, ImpresoraMaterialDto>();
            CreateMap<ImpresoraMaterialCrearDto, ImpresoraMaterial>();
            CreateMap<ImpresoraMaterialActualizarDto, ImpresoraMaterial>();

            CreateMap<EstadoImpresion, EstadoImpresionDto>();
            CreateMap<EstadoImpresionCrearDto, EstadoImpresion>();
            CreateMap<EstadoImpresionActualizarDto, EstadoImpresion>();

            CreateMap<TrabajoImpresion, TrabajoImpresionDto>();
            CreateMap<TrabajoImpresionCrearDto, TrabajoImpresion>();
            CreateMap<TrabajoImpresionActualizarDto, TrabajoImpresion>();

            CreateMap<ConsumoMaterial, ConsumoMaterialDto>();
            CreateMap<ConsumoMaterialCrearDto, ConsumoMaterial>();
            CreateMap<ConsumoMaterialActualizarDto, ConsumoMaterial>();

            // Módulo 4: Pedidos
            CreateMap<Pedidos, PedidosDto>();
            CreateMap<PedidosCrearDto, Pedidos>();
            CreateMap<PedidosActualizarDto, Pedidos>();

            CreateMap<DetallePedido, DetallePedidoDto>();
            CreateMap<DetallePedidoCrearDto, DetallePedido>();
            CreateMap<DetallePedidoActualizarDto, DetallePedido>();

            CreateMap<EstadoPedido, EstadoPedidoDto>();
            CreateMap<EstadoPedidoCrearDto, EstadoPedido>();
            CreateMap<EstadoPedidoActualizarDto, EstadoPedido>();
        }
    }
}
