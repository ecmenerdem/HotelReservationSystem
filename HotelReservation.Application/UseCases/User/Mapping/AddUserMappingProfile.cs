using AutoMapper;
using HotelReservation.Application.DTO.User;

namespace HotelReservation.Application.UseCases.User.Mapping;

public class AddUserMappingProfile:Profile
{
    public AddUserMappingProfile()
    {
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
    }
}