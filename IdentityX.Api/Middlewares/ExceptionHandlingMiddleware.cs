using System.Net;
using System.Text.Json;
using IdentityX.Api.Exceptions;
using IdentityX.BusinessLogic.Models;

namespace IdentityX.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _env;
    private readonly bool _isProductionFlag;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
        _isProductionFlag = _env.IsProduction();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // Proceed to controller
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            ErrorDetailsModel response = ExceptionClassifier(ex);
            context.Response.StatusCode = response.StatusCode;
            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }

    private ErrorDetailsModel ExceptionClassifier(Exception ex)
    {
        ErrorDetailsModel response;
        if (ex is OperationFailedException)
        {
            response = new ErrorDetailsModel()
            {
                StatusCode = 400,
                Message = ex.Message,
                StackTrace = _isProductionFlag ? null : ex.StackTrace
            };
        }
        else
        {
            response = new ErrorDetailsModel()
            {
                StatusCode = 500,
                Message = _isProductionFlag ?  "OOPS!! Something went wrong! Try again later." : ex.Message,
                StackTrace = _isProductionFlag ?  null : ex.StackTrace
            };
        }

        return response;
    }
}