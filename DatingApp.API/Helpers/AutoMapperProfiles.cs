using System;
using System.Linq;
using AutoMapper;
using DatingApp.API.DTOs;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        //Esta extension se encarga de mappear automaticamente los atributos de calases (DTO y clase original) en base a ocnvension
        public AutoMapperProfiles(){

            CreateMap<Usuario, UsuarioEnListaDTO>()
            .ForMember(dto => dto.PhotoUrl, opt => opt.MapFrom(src => src.FotosPublicas.FirstOrDefault(p => p.EsPrincipal).Url))
            .ForMember(dto => dto.Edad, opt => opt.MapFrom(src => src.FechaNacimiento.CalcularEdad()));
            CreateMap<Usuario, UsuarioDetallesDTO>()
            .ForMember(dto => dto.PhotoUrl, opt => opt.MapFrom(src => src.FotosPublicas.FirstOrDefault(p => p.EsPrincipal).Url))
            .ForMember(dto => dto.Edad, opt => opt.MapFrom(src => src.FechaNacimiento.CalcularEdad()));
            CreateMap<Photo, PhotoDetallesDTO>();
        }
        
    }
}