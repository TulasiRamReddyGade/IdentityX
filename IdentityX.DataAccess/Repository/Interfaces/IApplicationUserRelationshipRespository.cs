using IdentityX.DataAccess.Entities;

namespace IdentityX.DataAccess.Repository.Interfaces;

public interface IApplicationUserRelationshipRespository
{
    public Task<ApplicationUserRelationsEntity> CrateRelationAsync(ApplicationUserRelationsEntity relation);
    public Task<ApplicationUserRelationsEntity?> GetRelationAsync(Guid applicationUserId, Guid applicationRoleId,Guid applicationId);
}