using IdentityX.DataAccess.Entities;
using IdentityX.DataAccess.Identity;
using Microsoft.AspNetCore.Identity;

namespace IdentityX.DataAccess.Repository.Interfaces;

public interface IRoleRepository
{
    public Task<ApplicationRoleEntity> AddApplicationRoleAsync(ApplicationRoleEntity applicationRole);
    public Task<ApplicationRoleEntity?> GetRoleByIdAsync(Guid roleId);
    public Task<List<ApplicationRoleEntity>> GetApplicationRolesByApplicationIdAsync(Guid applicationId);
}