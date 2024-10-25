using System.Security.Claims;

namespace HotelReservation.Application.Contracts.Security;

public interface ITokenService
{
    string GenerateLoginToken(Guid userGUID, string kullaniciAdi, IEnumerable<Claim> additionalClaims = null);
}