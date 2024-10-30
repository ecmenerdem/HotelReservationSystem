using FluentValidation;
using HotelReservation.Application.DTO.User;
using Microsoft.AspNetCore.Identity.Data;

namespace HotelReservation.Application.UseCases.User.Validation;

public class LoginValidator:AbstractValidator<LoginRequestDTO>
{
    public LoginValidator()
    {
        RuleFor(q => q.KullaniciAdi).NotEmpty().WithMessage("Kullanıcı Adı Boş Olamaz");
        RuleFor(q => q.Sifre).NotEmpty().WithMessage("Şifre Boş Olamaz");
    }
}