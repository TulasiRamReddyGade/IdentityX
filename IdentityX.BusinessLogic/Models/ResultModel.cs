namespace IdentityX.BusinessLogic.Models;

public class ResultModel<T>
{
    public bool Success { get; set; }

    public string? SuccessMessage { get; set; }
    public T? ResultVaule { get; set; }

    public string? ErrorMessage { get; set; }
}