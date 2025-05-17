namespace IdentityX.DataAccess.Entities;

public class ApplicationRoleEntity
{
    public Guid Id { get; set; }
    public Guid ApplicationId { get; set; }
    public string RoleName { get; set; }
    public string NormalizedRoleName { get; set; }
    public bool? Active { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    
    public ApplicationEntity Application { get; set; }
}