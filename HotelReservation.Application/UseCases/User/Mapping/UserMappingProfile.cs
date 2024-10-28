using AutoMapper;
using HotelReservation.Application.DTO.User;
using HotelReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Application.UseCases.User.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<HotelReservation.Domain.Entities.User, UserDTO>()
                .ForMember(dest => dest.GUID,
                    opt => opt.MapFrom(src => src.GUID))
                .ForMember(dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName,
                    opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Username,
                    opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber,
                    opt => opt.MapFrom(src => src.PhoneNumber))
                .ReverseMap();

            CreateMap<HotelReservation.Domain.Entities.User, UserAddRequestDTO>()
                .ForMember(dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName,
                    opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Username,
                    opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password,
                    opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber,
                    opt => opt.MapFrom(src => src.PhoneNumber))
                .ReverseMap();
            
            
            CreateMap<UserUpdateRequestDTO, Domain.Entities.User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            // UserUpdateRequestDTO içindeki null olmayan alanları User nesnesine eşler
        }
    }
}