using FluentValidation;
using FluentValidation.Results;
using HotelReservation.Application.Contracts.Validation;

namespace HotelReservation.Infrastructure.Validation.FluentValidation;

public class FluentValidator:IGenericValidator
{
    public async Task ValidateAsync<T>(T entity, Type type=null)
    {
        //verilen tip ile validator oluşturma durumu kontrol ediliyor.
        if (!typeof(IValidator).IsAssignableFrom(type))
            throw new Exception("Hata: Validator tipi geçersiz!");

        var validator = (IValidator)Activator.CreateInstance(type);

        //valid veya valid olmama durumunun tamamı result olarak dönülür.
        ValidationResult validationResult = await validator.ValidateAsync(new ValidationContext<object>(entity));
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
    }
}