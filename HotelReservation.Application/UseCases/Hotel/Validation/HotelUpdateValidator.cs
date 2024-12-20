using FluentValidation;
using HotelReservation.Application.DTO.Hotel;

namespace HotelReservation.Application.UseCases.Hotel.Validation;

public class HotelUpdateValidator:AbstractValidator<HotelUpdateRequestDTO>
{
    public HotelUpdateValidator()
    {
        RuleFor(q=>q.GUID).NotEmpty().WithMessage("Geçerli Bir Otel Giriniz");
        RuleFor(q=>q.Name).NotEmpty().WithMessage("Otel Adı Boş Olamaz");
        RuleFor(q=>q.Address).NotEmpty().WithMessage("Adres Boş Olamaz");
    }
}