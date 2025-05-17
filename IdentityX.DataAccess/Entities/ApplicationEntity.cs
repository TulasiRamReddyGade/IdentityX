using IdentityX.DataAccess.Identity;

namespace IdentityX.DataAccess.Entities;

public class ApplicationEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string NormalizedName { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public bool? Active { get; set; }
    
    public ICollection<ApplicationRoleEntity> ApplicationRoles { get; set; }
    
    public ICollection<ApplicationUserRelationsEntity> ApplicationUserRelations { get; set; }
}