namespace IdentityX.Api.Dto;

public class GetApplicationResponseDto
{
    public string ApplicationName { get; set; }
    public Guid ApplicationId { get; set; }
    public string ApplicationNormalizedName { get; set; }
    public DateTime ApplicationCreatedOn { get; set; }
    public DateTime ApplicationUpdatedOn { get; set; }
    public bool ApplicationActive { get; set; }
}