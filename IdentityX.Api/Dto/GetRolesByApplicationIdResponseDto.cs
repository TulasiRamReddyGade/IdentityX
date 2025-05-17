namespace IdentityX.Api.Dto;

public class GetRolesByApplicationIdResponseDto
{
    public Guid ApplicationId { get; set; }
    public string RoleName { get; set; }
    public Guid RoleId { get; set; }
    public string NormalizedRoleName { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public bool? Active { get; set; }
}