using FluentValidation;
using HotelReservation.Application.DTO.Room;

namespace HotelReservation.Application.UseCases.Room.Validation;

public class UpdateRoomValidator:AbstractValidator<RoomUpdateRequestDTO>
{
    public UpdateRoomValidator()
    {
        RuleFor(q => q.RoomType).NotEmpty().WithMessage("Oda Tipi Zorunludur");
        RuleFor(q=>q.Capacity).NotEmpty().WithMessage("Kapasite Zorunludur");
        RuleFor(q=>q.IsAvailable).NotEmpty().WithMessage("Müsaitlik Durumu Zorunludur");
        RuleFor(q=>q.PricePerNight).NotEmpty().WithMessage("Ücret Bilgisi Zorunludur");
        RuleFor(q=>q.HotelGuid).NotEmpty().WithMessage("Otel Bilgisi Zorunludur");
        RuleFor(q=>q.RoomGuid).NotEmpty().WithMessage("Oda Bilgisi Zorunludur");
    }
}