using IdentityX.DataAccess.DBContext;
using IdentityX.DataAccess.Entities;
using IdentityX.DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdentityX.DataAccess.Repository;

public class ApplicationUserRelationshipRespository : IApplicationUserRelationshipRespository
{
    private readonly ApplicationDbContext _context;

    public ApplicationUserRelationshipRespository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApplicationUserRelationsEntity> CrateRelationAsync(ApplicationUserRelationsEntity relation)
    {
       var result = await  _context.ApplicationUserRelations.AddAsync(relation);
       await _context.SaveChangesAsync();
       return result.Entity;
    }

    public async Task<ApplicationUserRelationsEntity?> GetRelationAsync(Guid applicationUserId, Guid applicationRoleId, Guid applicationId)
    {
        var result = await _context.ApplicationUserRelations.Where(x =>
            x.ApplicationUserId == applicationUserId && x.ApplicationRoleId == applicationRoleId &&
            x.ApplicationId == applicationId).FirstOrDefaultAsync();
        return result;
    }
}