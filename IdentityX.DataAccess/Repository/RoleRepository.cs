using IdentityX.DataAccess.DBContext;
using IdentityX.DataAccess.Entities;
using IdentityX.DataAccess.Identity;
using IdentityX.DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityX.DataAccess.Repository;

public class RoleRepository : IRoleRepository
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ApplicationDbContext _context;
    public RoleRepository(RoleManager<ApplicationRole> roleManager,ILogger<RoleRepository> logger,ApplicationDbContext context)
    {
        _roleManager = roleManager;
        _context = context;
    }

    public async Task<ApplicationRoleEntity> AddApplicationRoleAsync(ApplicationRoleEntity applicationRole)
    {
        var result = await _context.ApplicationRoles.AddAsync(applicationRole);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<ApplicationRoleEntity?> GetRoleByIdAsync(Guid roleId)
    {
        var result = await _context.ApplicationRoles.FindAsync(roleId);
        return result;
    }

    public  async Task<List<ApplicationRoleEntity>> GetApplicationRolesByApplicationIdAsync(Guid applicationId)
    {
        var result = await _context.ApplicationRoles.Where(r => r.ApplicationId == applicationId).ToListAsync();
        return result;
    }
}