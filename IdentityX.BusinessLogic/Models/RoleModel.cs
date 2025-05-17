namespace IdentityX.BusinessLogic.Models;

public class RoleModel
{
    public Guid ApplicationId { get; set; }
    public string RoleName { get; set; }
    public Guid RoleId { get; set; }
    public string NormalizedRoleName { get; set; }
    public DateTime? CreateOn { get; set; }
    public DateTime? UpdateOn { get; set; }
    public bool? Active { get; set; }
}