using System.ComponentModel.DataAnnotations.Schema;
using IdentityX.DataAccess.Identity;

namespace IdentityX.DataAccess.Entities;

public class ApplicationUserRelationsEntity 
{
    public Guid Id { get; set; }
    
    public Guid ApplicationId { get; set; }
    public ApplicationEntity Application { get; set; }
    
    public Guid ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    
    public Guid ApplicationRoleId { get; set; }
    public ApplicationRole ApplicationRole { get; set; }
    public bool? Active { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}