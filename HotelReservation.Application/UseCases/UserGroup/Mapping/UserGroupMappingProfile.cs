using AutoMapper;
using HotelReservation.Application.DTO.UserGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Application.UseCases.UserGroup.Mapping
{
    public class UserGroupMappingProfile : Profile
    {
        public UserGroupMappingProfile()
        {
            CreateMap<Domain.Entities.UserGroup, UserGroupDTO>()
               .ForMember(dest => dest.GUID, opt => opt.MapFrom(src => src.GUID))
               .ForMember(dest => dest.Adi, opt => opt.MapFrom(src => src.Name))
               .ReverseMap();
        }
    }
}
