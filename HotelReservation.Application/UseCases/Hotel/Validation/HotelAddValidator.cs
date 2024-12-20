using FluentValidation;
using HotelReservation.Application.DTO.Hotel;

namespace HotelReservation.Application.UseCases.Hotel.Validation;

public class HotelAddValidator:AbstractValidator<HotelAddRequestDTO>
{
    public HotelAddValidator()
    {
        RuleFor(q => q.Name).NotEmpty().WithMessage("Otel Adı Boş Olamaz");
        RuleFor(q=>q.Address).NotEmpty().WithMessage("Adres Boş Olamaz");
    }
}