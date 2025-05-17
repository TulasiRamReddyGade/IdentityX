namespace IdentityX.BusinessLogic.Models;

public class AuthenticationModel
{
    public string Token { get; set; }
    public string Email { get; set; }
    public string PersonName { get; set; }
    public DateTime Expiration { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpirationDateTime { get; set; }
}