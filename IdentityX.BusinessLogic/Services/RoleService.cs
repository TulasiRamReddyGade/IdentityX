using IdentityX.BusinessLogic.Models;
using IdentityX.BusinessLogic.RESULT;
using IdentityX.BusinessLogic.ServiceContracts;
using IdentityX.DataAccess.Entities;
using IdentityX.DataAccess.Identity;
using IdentityX.DataAccess.Repository.Interfaces;

namespace IdentityX.BusinessLogic.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    public RoleService(IRoleRepository repository)
    {
        _roleRepository = repository;
    }


    public async Task<ResultModel<RoleModel>> CreateRoleAsync(RoleModel roleModel)
    {
        ApplicationRoleEntity applicationRole = new ApplicationRoleEntity()
        {
            RoleName = roleModel.RoleName,
            ApplicationId = roleModel.ApplicationId,
            NormalizedRoleName = roleModel.RoleName.ToUpper(),
            
        };
        var result = await _roleRepository.AddApplicationRoleAsync(applicationRole);
        var resultRolModel = new RoleModel()
        {
            RoleId = result.Id,
            ApplicationId = applicationRole.ApplicationId,
            RoleName = roleModel.RoleName,
            NormalizedRoleName = roleModel.RoleName.ToUpper(),
            Active = result.Active,
            CreateOn = result.CreatedOn,
            UpdateOn = result.UpdatedOn,
        };
        return Result<RoleModel>.Success(resultRolModel, $"Created role for given application Id : {roleModel.ApplicationId} ");
    }

    public async Task<ResultModel<RoleModel>> GetRoleByIdAsync(Guid roleId)
    {
        var result = await _roleRepository.GetRoleByIdAsync(roleId);

        if (result != null)
        {
            RoleModel resultRoleModel = new RoleModel()
            {
                ApplicationId = result.ApplicationId,
                RoleId = result.Id,
                RoleName = result.RoleName,
                NormalizedRoleName = result.NormalizedRoleName,
                CreateOn = result.CreatedOn,
                UpdateOn = result.UpdatedOn,
                Active = result.Active,
            };
            return Result<RoleModel>.Success(resultRoleModel,"Successfully retrieved role");
        }
        else
        {
            return Result<RoleModel>.Failure(null, "Role not found!");
        }
    }

    public async Task<ResultModel<List<RoleModel>>> GetRolesByApplicationId(Guid applicationId)
    {
        var result = await _roleRepository.GetApplicationRolesByApplicationIdAsync(applicationId);
        var resultRoleModelList = result.Select(r => new RoleModel()
        {
            ApplicationId = r.ApplicationId,
            RoleId = r.Id,
            RoleName = r.RoleName,
            NormalizedRoleName = r.NormalizedRoleName,
            Active = r.Active,
            UpdateOn = r.UpdatedOn,
            CreateOn = r.CreatedOn,

        }).ToList();
        return Result<List<RoleModel>>.Success(resultRoleModelList, "Successfully retrieved roles");
    }
}