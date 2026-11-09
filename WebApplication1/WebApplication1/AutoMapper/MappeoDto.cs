using AutoMapper;
using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;

namespace impresion3d.Api.AutoMapper
{
    public class MappeoDto : Profile
    {
        public MappeoDto()
        {
            CreateMap<Persona, PersonaDto>();
            CreateMap<CrearPersonaDto, Persona>();
            CreateMap<ActualizarPersonaDto, Persona>();
        }
    }
} 