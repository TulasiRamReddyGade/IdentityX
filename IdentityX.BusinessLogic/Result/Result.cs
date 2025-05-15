using IdentityX.BusinessLogic.Models;

namespace IdentityX.BusinessLogic.RESULT;

public static class Result<T>
{
    public static ResultModel<T> Success(T? result, string successMessage)
    {
        return new ResultModel<T> { Success = true, ResultVaule = result,SuccessMessage=successMessage };
    }


    public static ResultModel<T> Failure(T? result, string exceptionMessage)
    {
        return new ResultModel<T> { Success = false, ResultVaule = result, ErrorMessage = exceptionMessage };
    }
}