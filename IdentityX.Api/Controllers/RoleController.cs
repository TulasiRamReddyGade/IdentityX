using IdentityX.Api.Dto;
using IdentityX.Api.Exceptions;
using IdentityX.BusinessLogic.Models;
using IdentityX.BusinessLogic.ServiceContracts;
using IdentityX.DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IdentityX.Api.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;
    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    
    [HttpGet("{roleId}")]
    public async Task<IActionResult> GetRole([FromRoute] Guid roleId)
    {
        var result = await _roleService.GetRoleByIdAsync(roleId);
        if (result.ResultVaule!=null && result.Success)
        {
            var resultRoleModel = result.ResultVaule;
            var response = new GetRoleResponseDto()
            {
                ApplicationId = resultRoleModel.ApplicationId,
                RoleId = resultRoleModel.RoleId,
                RoleName = resultRoleModel.RoleName,
                NormalizedRoleName = resultRoleModel.NormalizedRoleName,
                CreatedOn = resultRoleModel.CreateOn,
                UpdatedOn = resultRoleModel.UpdateOn,
                Active = resultRoleModel.Active,
                
            };
            return Ok(response);
        }
        else
        {
            string error = result?.ErrorMessage ?? "Unable to find role";
            throw new OperationFailedException(error);
        }
    }
    
   
}