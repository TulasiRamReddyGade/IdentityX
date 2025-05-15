using IdentityX.BusinessLogic.Models;
using IdentityX.BusinessLogic.RESULT;
using IdentityX.BusinessLogic.ServiceContracts;
using IdentityX.DataAccess.Entities;
using IdentityX.DataAccess.Repository;
using IdentityX.DataAccess.Repository.Interfaces;

namespace IdentityX.BusinessLogic.Services;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;
    public ApplicationService(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }
    public async Task<ResultModel<ApplicationModel>> CreateApplicationAsync(ApplicationModel applicationModel)
    {
        ApplicationEntity applicationEntity = new ApplicationEntity()
        {
            Name = applicationModel.ApplicationName,
            NormalizedName = applicationModel.ApplicationName.ToUpper()
        };
        var result = await _applicationRepository.CreateApplicationAsync(applicationEntity);
        ApplicationModel resultApplicationModel = new ApplicationModel()
        {
            ApplicationName = result.Name,
            ApplicationId = result.Id,
            ApplicationNormalizedName = result.NormalizedName,
            ApplicationCreatedOn = result.CreatedOn,
            ApplicationUpdatedOn = result.UpdatedOn,
            ApplicationActive = result.Active ?? true
        };
        return Result<ApplicationModel>.Success(resultApplicationModel, "Application created successfully");
    }

    public async Task<ResultModel<ApplicationModel>> GetApplicationByIdAsync(Guid applicationId)
    {
        var result = await _applicationRepository.GetApplicationByIdAsync(applicationId);
        if (result == null)
        {
            return Result<ApplicationModel>.Failure(null, "Application not found");
        }
        else
        {
            ApplicationModel resultModel = new ApplicationModel()
            {
                ApplicationId = result.Id,
                ApplicationName = result.Name,
                ApplicationNormalizedName = result.NormalizedName,
                ApplicationCreatedOn = result.CreatedOn,
                ApplicationUpdatedOn = result.UpdatedOn,
                ApplicationActive = result.Active ?? true
            };
            return Result<ApplicationModel>.Success(resultModel, "Application retrieved successfully");
        }
    }

    public async Task<ResultModel<List<ApplicationModel>>> GetAllApplicationsAsync()
    {
        var result = await _applicationRepository.GetApplicationsAsync();
        var resultModelList = result.Select(x => new ApplicationModel()
        {
            ApplicationId = x.Id,
            ApplicationName = x.Name,
            ApplicationNormalizedName = x.NormalizedName,
            ApplicationCreatedOn = x.CreatedOn,
            ApplicationUpdatedOn = x.UpdatedOn,
            ApplicationActive = x.Active ?? true,
        }).ToList();
        return Result<List<ApplicationModel>>.Success(resultModelList, "Applications retrieved successfully");
    }

    public async Task<ResultModel<ApplicationModel>> DeactivateApplicationByIdAsync(Guid applicationId)
    {
        var result = await _applicationRepository.DeactivateApplicationByIdAsync(applicationId);
        
        if (result == null)
        {
            return Result<ApplicationModel>.Failure(null, "Application not found");
        }
        var applicationModel = new ApplicationModel()
        {
            ApplicationId = result.Id,
            ApplicationName = result.Name,
            ApplicationNormalizedName = result.NormalizedName,
            ApplicationCreatedOn = result.CreatedOn,
            ApplicationUpdatedOn = result.UpdatedOn,
            ApplicationActive = result.Active ?? true
        };
        return Result<ApplicationModel>.Success(applicationModel, "Application deleted successfully");
    }

    public async Task<ResultModel<ApplicationModel>> ActivateApplicationByIdAsync(Guid applicationId)
    {
        var result = await _applicationRepository.ActivateApplicationByIdAsync(applicationId);
        
        if (result == null)
        {
            return Result<ApplicationModel>.Failure(null, "Application not found");
        }
        var applicationModel = new ApplicationModel()
        {
            ApplicationId = result.Id,
            ApplicationName = result.Name,
            ApplicationNormalizedName = result.NormalizedName,
            ApplicationCreatedOn = result.CreatedOn,
            ApplicationUpdatedOn = result.UpdatedOn,
            ApplicationActive = result.Active ?? true
        };
        return Result<ApplicationModel>.Success(applicationModel, "Application activated successfully");
    }

    public async Task<ResultModel<ApplicationModel>> UpdateApplicationAsync(ApplicationModel applicationModel)
    {
        ApplicationEntity applicationEntity = new ApplicationEntity()
        {
            Id = applicationModel.ApplicationId,
            Name = applicationModel.ApplicationName,
        };
        var result = await _applicationRepository.UpdateApplicationAsync(applicationEntity);
        if (result != null)
        {
            ApplicationModel resultModel = new ApplicationModel()
            {
                ApplicationId = result.Id,
                ApplicationName = result.Name,
                ApplicationNormalizedName = result.NormalizedName,
                ApplicationCreatedOn = result.CreatedOn,
                ApplicationUpdatedOn = result.UpdatedOn,
                ApplicationActive = result.Active ?? true
            };
            return Result<ApplicationModel>.Success(resultModel, "Application updated successfully");
        }
        else
        {
            return Result<ApplicationModel>.Failure(null, "Application not found");
        }
    }
}