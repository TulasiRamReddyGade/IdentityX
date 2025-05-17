using System.ComponentModel.DataAnnotations;

namespace IdentityX.Api.Dto;

public class CreateRoleRequestDto
{
    [Required]
    [MinLength(3)]
    public string RoleName { get; set; }
}