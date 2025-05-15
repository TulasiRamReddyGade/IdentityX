using IdentityX.DataAccess.Identity;
using IdentityX.DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;


namespace IdentityX.DataAccess.Repository;

public class UserRepository : IUserRepositiry
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ILogger<UserRepository> _logger;
    private readonly SignInManager<ApplicationUser> _signInManager;
    public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ILogger<UserRepository> logger,SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _logger = logger;
    }
}

