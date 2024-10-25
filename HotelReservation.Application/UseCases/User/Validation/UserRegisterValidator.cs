using FluentValidation;
using HotelReservation.Application.DTO.User;

namespace HotelReservation.Application.UseCases.User.Validation;

public class UserRegisterValidator:AbstractValidator<UserAddRequestDTO>
{
    public UserRegisterValidator()
    {
        RuleFor(q=>q.FirstName).NotEmpty().WithMessage("Ad Boş Olamaz");
        RuleFor(q=>q.LastName).NotEmpty().WithMessage("Soyad Boş Olamaz");
        RuleFor(q=>q.Username).NotEmpty().WithMessage("Kullanıcı Adı Boş Olamaz");
        RuleFor(q=>q.Password).NotEmpty().WithMessage("Şifre Boş Olamaz");
        RuleFor(q=>q.Email).NotEmpty().WithMessage("E-Posta Boş Olamaz");
        RuleFor(q=>q.PhoneNumber).NotEmpty().WithMessage("Telefon Numarası Boş Olamaz");
    }
}