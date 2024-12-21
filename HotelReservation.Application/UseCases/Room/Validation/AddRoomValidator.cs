using FluentValidation;
using HotelReservation.Application.DTO.Room;

namespace HotelReservation.Application.UseCases.Room.Validation;

public class AddRoomValidator:AbstractValidator<RoomAddRequestDTO>
{
    public AddRoomValidator()
    {
        RuleFor(q=>q.HotelGuid).NotEmpty().WithMessage("Otel Bilgisi Zorunludur.");
        RuleFor(q=>q.Capacity).NotEmpty().WithMessage("Kapasite Bilgisi Zorunludur.");
        RuleFor(q=>q.RoomType).NotEmpty().WithMessage("Oda Tipi Zorunludur");
        RuleFor(q=>q.PricePerNight).NotEmpty().WithMessage("Fiyat Bilgisi Zorunludur.");
    }
}