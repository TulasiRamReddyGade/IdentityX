using IdentityX.BusinessLogic.Models;
using IdentityX.DataAccess.Identity;

namespace IdentityX.BusinessLogic.ServiceContracts;

public interface IJwtService
{
    public AuthenticationModel CreateJwtToken(JwtModel jwtModel);
}