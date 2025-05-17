using IdentityX.DataAccess.Identity;
using Microsoft.AspNetCore.Identity;

namespace IdentityX.DataAccess.Repository.Interfaces;

public interface IUserRepositiry
{
    public Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
    public Task<ApplicationUser?> FindByEmailAsync(string email);
    public Task<ApplicationUser?> FindByIdAsync(string userId);
    public Task SignInAsync(ApplicationUser user);
    public Task<SignInResult> SignInAsync(string email, string password);
    public Task<IdentityResult> UpdateAsync(ApplicationUser userResult);
    
    
}