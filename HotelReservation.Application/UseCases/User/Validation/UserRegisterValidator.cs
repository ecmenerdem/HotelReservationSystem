using FluentValidation;
using HotelReservation.Application.Contracts.Persistence;
using HotelReservation.Application.DTO.User;
using HotelReservation.Domain.Repositories;

namespace HotelReservation.Application.UseCases.User.Validation;

public class UserRegisterValidator:AbstractValidator<UserAddRequestDTO>
{
    private static IUserService _userService;
    public static void Initialize(IUserService userService)
    {
        _userService=userService;
    }
    public UserRegisterValidator()
    {
      
        RuleFor(q=>q.FirstName).NotEmpty().WithMessage("Ad Boş Olamaz");
        RuleFor(q=>q.LastName).NotEmpty().WithMessage("Soyad Boş Olamaz");
        RuleFor(q=>q.Username).NotEmpty().WithMessage("Kullanıcı Adı Boş Olamaz");
        RuleFor(q => q.Username).MustAsync(CheckUniqueUserName).WithMessage("Farklı Bir Kullanıcı Adı Giriniz.");
        RuleFor(q=>q.Password).NotEmpty().WithMessage("Şifre Boş Olamaz");
        RuleFor(q=>q.Email).NotEmpty().WithMessage("E-Posta Boş Olamaz");
        RuleFor(q => q.Email).MustAsync(CheckUniqueEMail).WithMessage("Farklı Bir E-Posta Adresi Giriniz.");
        RuleFor(q=>q.PhoneNumber).NotEmpty().WithMessage("Telefon Numarası Boş Olamaz");
    }

    private async Task<bool> CheckUniqueEMail(string eMail, CancellationToken arg2)
    {
        var existingUser = await _userService.GetUserByEMailAsync(eMail);
        return existingUser.Data is null;
    }

    private async Task<bool> CheckUniqueUserName(string username, CancellationToken cancellationToken)
    {
        var existingUser = await _userService.GetUserByUsernameAsync(username);
        return existingUser.Data is null;
    }
}