using IdentityX.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace IdentityX.DataAccess.Repository.Interfaces;

public interface IApplicationRepository
{
    public Task<ApplicationEntity> CreateApplicationAsync(ApplicationEntity newApplication);
    public Task<ApplicationEntity?> GetApplicationByIdAsync(Guid applicationId);
    public Task<List<ApplicationEntity>> GetApplicationsAsync();
    
    public Task<ApplicationEntity?> DeactivateApplicationByIdAsync(Guid applicationId);
    public Task<ApplicationEntity?> ActivateApplicationByIdAsync(Guid applicationId);
    public Task<ApplicationEntity?> UpdateApplicationAsync(ApplicationEntity application);
}