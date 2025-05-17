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

    public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<ApplicationUser?> FindByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<ApplicationUser?> FindByIdAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task SignInAsync(ApplicationUser user)
    {
        await _signInManager.SignInAsync(user, false);
    }

    public async Task<SignInResult> SignInAsync(string email, string password)
    {
        return await _signInManager.PasswordSignInAsync(email, password, false, false);
    }

    public async Task<IdentityResult> UpdateAsync(ApplicationUser userResult)
    {
        return await _userManager.UpdateAsync(userResult);
    }
}

