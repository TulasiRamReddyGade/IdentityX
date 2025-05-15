using System.ComponentModel.DataAnnotations;

namespace IdentityX.Api.Dto;

public class CreateApplicationRequestDto
{
    [Required]
    public string ApplicationName { get; set; }
    
}