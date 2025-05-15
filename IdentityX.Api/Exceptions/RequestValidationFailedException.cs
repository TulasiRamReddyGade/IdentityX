namespace IdentityX.Api.Exceptions;

public class RequestValidationFailedException(string message) : Exception(message);