using IdentityX.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityX.DataAccess.Identity;

public class ApplicationRole : IdentityRole<Guid>
{
    public Guid ApplicationId { get; set; }
    public ApplicationEntity Application { get; set; }
    public ICollection<ApplicationUserRelationsEntity> ApplicationUserRelations { get; set; }
    
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public bool? Active { get; set; }
}