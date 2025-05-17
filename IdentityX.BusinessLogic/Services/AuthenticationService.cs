using IdentityX.BusinessLogic.Models;
using IdentityX.BusinessLogic.RESULT;
using IdentityX.BusinessLogic.ServiceContracts;
using IdentityX.DataAccess.Entities;
using IdentityX.DataAccess.Identity;
using IdentityX.DataAccess.Repository.Interfaces;

namespace IdentityX.BusinessLogic.Services;

public class AuthenticationService : IAuthenicationService
{
    private readonly IUserRepositiry _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IApplicationUserRelationshipRespository _applicationUserRelationshipRespository;
    private readonly IJwtService _jwtService;
    
    public AuthenticationService(IUserRepositiry userRepository, IRoleRepository roleRepository,IApplicationRepository applicationRepository, IApplicationUserRelationshipRespository applicationUserRelationshipRespository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _applicationRepository = applicationRepository;
        _applicationUserRelationshipRespository = applicationUserRelationshipRespository;
        _jwtService = jwtService;
    }
    public async Task<ResultModel<AuthenticationModel>> RegisterAsync(UserModel userModel)
    {
        // Check if user exits ?
        var userResult = await _userRepository.FindByEmailAsync(userModel.Email);
        var roleResult = await _roleRepository.GetRoleByIdAsync(userModel.RoleId);
        var applicationResult = await _applicationRepository.GetApplicationByIdAsync(userModel.ApplicationId);
        if (roleResult == null)
        {
            return Result<AuthenticationModel>.Failure(null, "Role doesn't exist");
        }

        if (applicationResult == null)
        {
            return Result<AuthenticationModel>.Failure(null, "Application doesn't exist");
        }
        if (userResult == null)
        {
            ApplicationUser applicationUser = new()
            {
                Email = userModel.Email,
                PersonName = userModel.PersonName,
                UserName = userModel.Email
            };
            var identityResult = await _userRepository.CreateAsync(applicationUser, userModel.Password);
            userResult = applicationUser;
            if (identityResult.Succeeded)
            {
                await _userRepository.SignInAsync(applicationUser);
            }
            else
            {
                string errors = string.Join(" | ", identityResult.Errors.Select(e => e.Description));
                return Result<AuthenticationModel>.Failure(null, errors);
            }
        }
        var applicationUserRelationsResult = await _applicationUserRelationshipRespository.GetRelationAsync(userResult.Id,roleResult.Id,applicationResult.Id);
        if (applicationUserRelationsResult == null)// register is relation does not exist
        {
            // applicationuserrelationship populate
            ApplicationUserRelationsEntity applicationUserRelations = new()
            {
                ApplicationUserId = userResult.Id,
                ApplicationId = applicationResult.Id,
                ApplicationRoleId = roleResult.Id,
            };
            applicationUserRelationsResult = await _applicationUserRelationshipRespository.CrateRelationAsync(applicationUserRelations);
        }

        JwtModel jwtModel = new()
        {
            PersonName = userModel.PersonName,
            Email = userModel.Email,
            RelationshipId = applicationUserRelationsResult.Id,
        };
        
        var authenticationModel = _jwtService.CreateJwtToken(jwtModel);
        userResult.RefreshToken = authenticationModel.RefreshToken;
        userResult.RefreshTokenExpiry = authenticationModel.RefreshTokenExpirationDateTime;
        var userUpdateResult = await _userRepository.UpdateAsync(userResult);
        if (!userUpdateResult.Succeeded)
        {
            string errors = string.Join(" | ", userUpdateResult.Errors.Select(e => e.Description));
            return Result<AuthenticationModel>.Failure(null, errors);
        }

        return Result<AuthenticationModel>.Success(authenticationModel,"Created successfully");
    }

    public async Task<ResultModel<AuthenticationModel>> LoginAsync(UserModel userModel)
    {
        var signInResult = await _userRepository.SignInAsync(userModel.Email, userModel.Password);
        if (signInResult.Succeeded)
        {
            Guid applicationId = userModel.ApplicationId;
            Guid roleId = userModel.RoleId;
            Guid userId = userModel.UserId;
            var applicationUserRelations = await _applicationUserRelationshipRespository.GetRelationAsync(userId, roleId, applicationId);
            if (applicationUserRelations == null)
            {
                return Result<AuthenticationModel>.Failure(null, "Invalid login attempt");
            }

            JwtModel jwtModel = new JwtModel()
            {
                PersonName = userModel.PersonName,
                Email = userModel.Email,
                RelationshipId = applicationUserRelations.Id,
            };
            var authenticationModel = _jwtService.CreateJwtToken(jwtModel);
            return Result<AuthenticationModel>.Success(authenticationModel, "Login successfully");

        }
        else
        {
            return Result<AuthenticationModel>.Failure(null, "Invalid login attempt");
        }
    }
    
}