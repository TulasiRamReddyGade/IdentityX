using IdentityX.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityX.DataAccess.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string PersonName { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    public string? FirebaseUid { get; set; }
    
    public ICollection<ApplicationUserRelationsEntity> ApplicationUserRelations { get; set; }
   
}