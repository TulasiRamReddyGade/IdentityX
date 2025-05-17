using IdentityX.Api.Dto;
using IdentityX.Api.Exceptions;
using IdentityX.BusinessLogic.Models;
using IdentityX.BusinessLogic.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace IdentityX.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ApplicationController : ControllerBase
{
    private readonly ILogger<ApplicationController> _logger;
    private readonly IApplicationService _applicationService;
    private readonly IRoleService _roleService;
    public ApplicationController(ILogger<ApplicationController> logger, IApplicationService applicationService,IRoleService roleService)
    {
        _logger = logger;
        _applicationService = applicationService;
        _roleService = roleService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateApplication(CreateApplicationRequestDto request)
    {
        
        ApplicationModel requestModel = new ApplicationModel()
        {
            ApplicationName = request.ApplicationName,
        };
        var result = await _applicationService.CreateApplicationAsync(requestModel);
        if (result.Success && result.ResultVaule!=null)
        {
            var value = result.ResultVaule;
            var response = new CreateApplicationResponseDto()
            {
                ApplicationId = value.ApplicationId,
                ApplicationName = value.ApplicationName,
                ApplicationActive = value.ApplicationActive,
                ApplicationCreatedOn = value.ApplicationCreatedOn,
                ApplicationUpdatedOn = value.ApplicationUpdatedOn,
                ApplicationNormalizedName = value.ApplicationNormalizedName,
            }; 
            return CreatedAtAction("CreateApplication",response);
        }
        else
        {
            throw new OperationFailedException(result.ErrorMessage ?? "Unable to register application");
        }
    }
    
    [HttpDelete("deactivate/{applicationId}")]
    public async Task<IActionResult> DeactivateApplication(Guid applicationId)
    {
        if (applicationId == Guid.Empty)
        {
            throw new RequestValidationFailedException("Application id is invalid");
        }
        var result = await _applicationService.DeactivateApplicationByIdAsync(applicationId);
        if (result.Success && result.ResultVaule != null)
        {
            return NoContent();
        }
        else
        {
            throw new OperationFailedException(result.ErrorMessage ?? "Unable to deactivate application");
        }
    }
    [HttpPut("activate/{applicationId}")]
    public async Task<IActionResult> ActivateApplication(Guid applicationId)
    {
        if (applicationId == Guid.Empty)
        {
            throw new RequestValidationFailedException("Application id is invalid");
        }
        var result = await _applicationService.ActivateApplicationByIdAsync(applicationId);
        if (result.Success && result.ResultVaule != null)
        {
            var response = new ActivateApplicationResponseDto()
            {
                ApplicationId = result.ResultVaule.ApplicationId,
                ApplicationName = result.ResultVaule.ApplicationName,
                ApplicationActive = result.ResultVaule.ApplicationActive,
                ApplicationCreatedOn = result.ResultVaule.ApplicationCreatedOn,
                ApplicationUpdatedOn = result.ResultVaule.ApplicationUpdatedOn,
                ApplicationNormalizedName = result.ResultVaule.ApplicationNormalizedName,
            };
            return CreatedAtAction("ActivateApplication",response);
        }
        else
        {
            throw new OperationFailedException(result.ErrorMessage ?? "Unable to activate application");
        }
    }
    
    [HttpGet("{applicationId}")]
    public async Task<IActionResult> GetApplication(Guid applicationId)
    {
        if (applicationId == Guid.Empty)
        {
            throw new RequestValidationFailedException("Application id is invalid");
        }
        var result = await _applicationService.GetApplicationByIdAsync(applicationId);
        if (result.Success && result.ResultVaule != null)
        {
            var value = result.ResultVaule;
            var response = new GetApplicationResponseDto()
            {
                ApplicationId = value.ApplicationId,
                ApplicationName = value.ApplicationName,
                ApplicationActive = value.ApplicationActive,
                ApplicationCreatedOn = value.ApplicationCreatedOn,
                ApplicationUpdatedOn = value.ApplicationUpdatedOn,
                ApplicationNormalizedName = value.ApplicationNormalizedName,
            };
            return Ok(response);
        }
        else
        {
            throw new OperationFailedException(result.ErrorMessage ?? "Application with given id does not exist");
        }
    }
    
    [HttpGet()]
    public async Task<IActionResult> GetApplications()
    {

        var result = await _applicationService.GetAllApplicationsAsync();
        
            var value = result.ResultVaule;
            var response = value?.Select(v => new GetApplicationResponseDto()
            {
                ApplicationId = v.ApplicationId,
                ApplicationName = v.ApplicationName,
                ApplicationActive = v.ApplicationActive,
                ApplicationCreatedOn = v.ApplicationCreatedOn,
                ApplicationUpdatedOn = v.ApplicationUpdatedOn,
                ApplicationNormalizedName = v.ApplicationNormalizedName,
            }).ToList();
            return Ok(response);
    }
    
    [HttpPut()]
    public async Task<IActionResult> UpdateApplication(UpdateApplicationRequestDto request)
    {
        if (request.ApplicationId == Guid.Empty)
        {
            throw new RequestValidationFailedException("Application id is invalid");
        }
        
        if (request.ApplicationName == String.Empty)
        {
            throw new RequestValidationFailedException("Application Name is invalid");
        }

        ApplicationModel applicationModel = new ApplicationModel()
        {
            ApplicationId = request.ApplicationId,
            ApplicationName = request.ApplicationName,
        };
        var result = await _applicationService.UpdateApplicationAsync(applicationModel);
        if (result.Success && result.ResultVaule != null)
        {
            var response = new UpdateApplicationResponseDto()
            {
                ApplicationId = result.ResultVaule.ApplicationId,
                ApplicationName = result.ResultVaule.ApplicationName,
                ApplicationActive = result.ResultVaule.ApplicationActive,
                ApplicationCreatedOn = result.ResultVaule.ApplicationCreatedOn,
                ApplicationUpdatedOn = result.ResultVaule.ApplicationUpdatedOn,
                ApplicationNormalizedName = result.ResultVaule.ApplicationNormalizedName,
            };
            return CreatedAtAction("UpdateApplication",response);
        }
        else
        {
            throw new OperationFailedException(result.ErrorMessage ?? "Unable to update application");
        }
    }

    [HttpGet("{applicationId}/role")]
    public async Task<IActionResult> GetApplicationRoles(Guid applicationId)
    {
        var result = await _roleService.GetRolesByApplicationId(applicationId);
        if (result.Success && result.ResultVaule != null)
        {
            var value = result.ResultVaule;
            var response = value.Select(x => new GetRolesByApplicationIdResponseDto()
            {
                ApplicationId = x.ApplicationId,
                RoleName = x.RoleName,
                Active = x.Active,
                UpdatedOn = x.UpdateOn,
                CreatedOn = x.CreateOn,
                RoleId = x.RoleId,
                NormalizedRoleName = x.NormalizedRoleName,
            }).ToList();
            return Ok(response);
        }
        throw new OperationFailedException(result.ErrorMessage ?? "Unable to retrieve roles");
    }
    [HttpPost("{applicationId}/role")]
    public async Task<IActionResult> CreateRole([FromRoute] Guid applicationId,
        [FromBody] CreateRoleRequestDto request)
    {
        RoleModel roleModel = new RoleModel()
        {
            ApplicationId = applicationId,
            RoleName = request.RoleName
        };
        var result = await _roleService.CreateRoleAsync(roleModel);
        if (result.ResultVaule!=null && result.Success)
        {
            var resultRoleModel = result.ResultVaule;
            var response = new CreateRoleResponseDto()
            {
                ApplicationId = resultRoleModel.ApplicationId,
                RoleId = resultRoleModel.RoleId,
                RoleName = resultRoleModel.RoleName,
                NormalizedRoleName = resultRoleModel.NormalizedRoleName,
                CreatedOn = resultRoleModel.CreateOn,
                UpdatedOn = resultRoleModel.UpdateOn,
                Active = resultRoleModel.Active,
            };
            return CreatedAtAction("CreateRole",response);
        }
        else
        {
            string error = result?.ErrorMessage ?? "Unable to create role";
            throw new OperationFailedException(error);
        }
    }

}