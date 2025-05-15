using IdentityX.BusinessLogic.Models;
using IdentityX.DataAccess.Entities;

namespace IdentityX.BusinessLogic.ServiceContracts;

public interface IApplicationService
{
    public Task<ResultModel<ApplicationModel>> CreateApplicationAsync(ApplicationModel applicationModel);
    public Task<ResultModel<ApplicationModel>> GetApplicationByIdAsync(Guid applicationId);
    public Task<ResultModel<List<ApplicationModel>>> GetAllApplicationsAsync();
    
    public Task<ResultModel<ApplicationModel>> DeactivateApplicationByIdAsync(Guid applicationId);
    public Task<ResultModel<ApplicationModel>> ActivateApplicationByIdAsync(Guid applicationId);
    public Task<ResultModel<ApplicationModel>> UpdateApplicationAsync(ApplicationModel applicationModel);
}