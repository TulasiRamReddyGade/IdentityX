using IdentityX.BusinessLogic.Models;
using IdentityX.DataAccess.Identity;

namespace IdentityX.BusinessLogic.ServiceContracts;

public interface IAuthenicationService
{
    public Task<ResultModel<AuthenticationModel>> RegisterAsync(UserModel userModel);
    public Task<ResultModel<AuthenticationModel>> LoginAsync(UserModel userModel);
}