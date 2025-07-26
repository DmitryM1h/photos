using System;
using System.Text.Json;
using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.MiddleWare
{
    public class ExceptionHandlingMiddleware(
        RequestDelegate _next,
        ILogger<ExceptionHandlingMiddleware> _logger)
    {

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("Сработал ExceptionHandler");
            try
            {
                await _next(context);
            }
            catch (UserNotFoundException exception)
            {
                _logger.LogError(
                exception, "Exception occurred: {Message}", exception.Message);

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Server Error",
                    Detail = exception.Message
                };

                context.Response.StatusCode =
                    StatusCodes.Status404NotFound;

                var options = new JsonSerializerOptions
                {

                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                var json = JsonSerializer.Serialize(problemDetails, options);
                await context.Response.WriteAsync(json);
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    exception, "Exception occurred: {Message}", exception.Message);

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Server Error",
                    Detail = exception.Message
                };

                context.Response.StatusCode =
                    StatusCodes.Status500InternalServerError;

                var options = new JsonSerializerOptions
                {
                 
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                var json = JsonSerializer.Serialize(problemDetails,options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
