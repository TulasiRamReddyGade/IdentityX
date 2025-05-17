namespace IdentityX.BusinessLogic.Models;

public class UserModel
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string PersonName { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public Guid RoleId { get; set; }
    public Guid ApplicationId { get; set; }
    
}