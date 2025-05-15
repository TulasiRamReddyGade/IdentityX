using IdentityX.DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IdentityX.DataAccess.Repository;

public class RoleRepository
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    public RoleRepository(RoleManager<ApplicationRole> roleManager,ILogger<RoleRepository> logger)
    {
        _roleManager = roleManager;
    }
}