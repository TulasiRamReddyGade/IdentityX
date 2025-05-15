namespace IdentityX.BusinessLogic.Models;

public class ErrorDetailsModel
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string? StackTrace { get; set; }
}