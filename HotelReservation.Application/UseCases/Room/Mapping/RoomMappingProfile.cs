using AutoMapper;
using HotelReservation.Application.DTO.Room;

namespace HotelReservation.Application.UseCases.Room.Mapping;

public class RoomMappingProfile:Profile
{
    public RoomMappingProfile()
    {
        CreateMap<Domain.Entities.Room, RoomAddRequestDTO>()
            .ForMember(dest => dest.RoomType,
                opt => opt.MapFrom(src => src.RoomType))
            .ForMember(dest => dest.Capacity,
                opt => opt.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.IsAvailable,
                opt => opt.MapFrom(src => src.IsAvailable))
            .ForMember(dest => dest.PricePerNight,
                opt => opt.MapFrom(src => src.PricePerNight))
            .ReverseMap();
        
        
        CreateMap<Domain.Entities.Room, RoomDTO>()
            .ForMember(dest => dest.RoomType,
                opt => opt.MapFrom(src => src.RoomType))
            .ForMember(dest => dest.Capacity,
                opt => opt.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.IsAvailable,
                opt => opt.MapFrom(src => src.IsAvailable))
            .ForMember(dest => dest.PricePerNight,
                opt => opt.MapFrom(src => src.PricePerNight))
            .ForMember(dest => dest.GUID,
                opt => opt.MapFrom(src => src.GUID))
            .ReverseMap();
    }
}