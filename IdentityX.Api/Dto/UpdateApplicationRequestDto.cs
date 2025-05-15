using System.ComponentModel.DataAnnotations;

namespace IdentityX.Api.Dto;

public class UpdateApplicationRequestDto
{
    [Required]
    public Guid ApplicationId { get; set; }
    [Required]
    public string ApplicationName { get; set; }
}