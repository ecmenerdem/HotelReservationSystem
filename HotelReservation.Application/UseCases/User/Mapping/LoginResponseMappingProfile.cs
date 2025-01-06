using AutoMapper;
using HotelReservation.Application.DTO.User;

namespace HotelReservation.Application.UseCases.User.Mapping;

public class LoginResponseMappingProfile:Profile
{
    public LoginResponseMappingProfile()
    {
        CreateMap<HotelReservation.Domain.Entities.User, LoginResponseDTO>()
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
            .ForMember(dest => dest.GroupID,
                opt => opt.MapFrom(src => src.GroupID))
            .ForMember(dest => dest.GroupName,
                opt => opt.MapFrom(src => src.Group.Name))
            .ReverseMap();

    }
}