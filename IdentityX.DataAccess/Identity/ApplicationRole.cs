using IdentityX.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityX.DataAccess.Identity;

public class ApplicationRole : IdentityRole<Guid>
{
    public ICollection<ApplicationUserRelationsEntity> ApplicationUserRelations { get; set; }
}