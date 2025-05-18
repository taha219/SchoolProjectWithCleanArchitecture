using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Bases;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var responseModel = new ApiResponse<object>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Internal Server Error"
            };

            switch (error)
            {
                case ValidationException validationEx:
                    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    responseModel.StatusCode = HttpStatusCode.UnprocessableEntity;
                    responseModel.Message = "Validation failed";

                    var validationErrors = validationEx.Errors?
                        .Select(e => $"{e.PropertyName}: {e.ErrorMessage}")
                        .ToList();

                    if (validationErrors != null && validationErrors.Any())
                        responseModel.Errors = validationErrors;
                    else
                        responseModel.Errors = new List<string> { validationEx.Message };

                    break;

                case UnauthorizedAccessException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    responseModel.StatusCode = HttpStatusCode.Unauthorized;
                    responseModel.Message = error.Message;
                    break;

                case KeyNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    responseModel.StatusCode = HttpStatusCode.NotFound;
                    responseModel.Message = error.Message;
                    break;

                case DbUpdateException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.StatusCode = HttpStatusCode.BadRequest;
                    responseModel.Message = "Database update error: " + error.Message;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    responseModel.Message = error.Message;
                    if (error.InnerException != null)
                        responseModel.Message += "\n" + error.InnerException.Message;
                    break;
            }

            var result = JsonSerializer.Serialize(responseModel);
            await response.WriteAsync(result);
        }
    }
}
