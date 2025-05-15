using IdentityX.DataAccess.DBContext;
using IdentityX.DataAccess.Entities;
using IdentityX.DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;

namespace IdentityX.DataAccess.Repository;

public class ApplicationRepository : IApplicationRepository
{
    private readonly ApplicationDbContext _context;

    public ApplicationRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ApplicationEntity> CreateApplicationAsync(ApplicationEntity newApplication)
    {
        
        var result = await _context.Applications.AddAsync(newApplication);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<ApplicationEntity?> GetApplicationByIdAsync(Guid applicationId)
    {
        var result = await _context.Applications.FirstOrDefaultAsync(x =>x.Id == applicationId && x.Active == true);
        return result;
    }

    public async Task<List<ApplicationEntity>> GetApplicationsAsync()
    {
        var result = await _context.Applications.ToListAsync();
        result = result.Where(x => x.Active ==true ).ToList();
        return result;
    }

    public async Task<ApplicationEntity?> DeactivateApplicationByIdAsync(Guid applicationId)
    {
        var result = await _context.Applications.FirstOrDefaultAsync(x =>x.Id == applicationId);
        if (result == null)
            return null;
        result.Active = false;
        await _context.SaveChangesAsync();
        return result;
    }

    public async Task<ApplicationEntity?> ActivateApplicationByIdAsync(Guid applicationId)
    {
        var result = await _context.Applications.FirstOrDefaultAsync(x =>x.Id == applicationId);
        if (result == null)
            return null;
        result.Active = true;
        await _context.SaveChangesAsync();
        return result;
    }

    public async Task<ApplicationEntity?> UpdateApplicationAsync(ApplicationEntity application)
    {
        var result = await _context.Applications.FirstOrDefaultAsync(x =>x.Id == application.Id);
        if (result == null)
            return null;
        result.Name = application.Name;
        result.NormalizedName = application.Name.ToUpper();
        await _context.SaveChangesAsync();
        return result;
    }
}