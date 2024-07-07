using Parking.Model.DTOs.Account;

namespace Parking.Model.Interfaces.Services;

public interface ITokenService
{
    UserToken GenerateToken(string email);
}
