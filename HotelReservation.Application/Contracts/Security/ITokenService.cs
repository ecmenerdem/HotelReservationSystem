using System.Security.Claims;

namespace HotelReservation.Application.Contracts.Security;

public interface ITokenService
{
    string GenerateToken(IEnumerable<Claim> claims);
}