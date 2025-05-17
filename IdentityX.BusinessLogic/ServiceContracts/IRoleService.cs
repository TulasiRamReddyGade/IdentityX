using IdentityX.BusinessLogic.Models;

namespace IdentityX.BusinessLogic.ServiceContracts;

public interface IRoleService
{
    public Task<ResultModel<RoleModel>> CreateRoleAsync(RoleModel roleModel);
    public Task<ResultModel<RoleModel>> GetRoleByIdAsync(Guid roleId);
    
    public Task<ResultModel<List<RoleModel>>> GetRolesByApplicationId(Guid applicationId);
}