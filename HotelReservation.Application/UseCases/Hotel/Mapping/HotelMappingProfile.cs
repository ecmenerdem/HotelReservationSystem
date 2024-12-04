using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelReservation.Application.DTO.Hotel;

namespace HotelReservation.Application.UseCases.Hotel.Mapping
{
    public class HotelMappingProfile : Profile
    {
        public HotelMappingProfile()
        {
            CreateMap<Domain.Entities.Hotel, HotelDTO>()
                    .ForMember(dest => dest.ID,
                        opt => opt.MapFrom(src => src.ID))
                    .ForMember(dest => dest.GUID,
                        opt => opt.MapFrom(src => src.GUID))
                    .ForMember(dest => dest.Name,
                        opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Address,
                        opt => opt.MapFrom(src => src.Address))
                    .ForMember(dest => dest.City,
                        opt => opt.MapFrom(src => src.City)) 
                    .ForMember(dest => dest.Description,
                        opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.PhoneNumber,
                        opt => opt.MapFrom(src => src.PhoneNumber))
                    .ForMember(dest => dest.Email,
                        opt => opt.MapFrom(src => src.Email))
                    .ReverseMap();
        }
    }
}
