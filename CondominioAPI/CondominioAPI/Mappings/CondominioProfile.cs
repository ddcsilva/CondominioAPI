using AutoMapper;
using CondominioAPI.Application.DTOs;
using CondominioAPI.Domain.Entities;

namespace CondominioAPI.Application.Mappings
{
    public class CondominioProfile : Profile
    {
        public CondominioProfile()
        {
            CreateMap<Condominio, CondominioDTO>().ReverseMap();
        }
    }
}
