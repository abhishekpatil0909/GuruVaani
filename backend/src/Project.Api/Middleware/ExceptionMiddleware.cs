using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Project.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception processing request");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int status;
            string title;

            switch (ex)
            {
                case InvalidOperationException _:
                    status = (int)HttpStatusCode.BadRequest;
                    title = "Invalid operation";
                    break;
                case UnauthorizedAccessException _:
                    status = (int)HttpStatusCode.Unauthorized;
                    title = "Unauthorized";
                    break;
                case KeyNotFoundException _:
                    status = (int)HttpStatusCode.NotFound;
                    title = "Not found";
                    break;
                default:
                    status = (int)HttpStatusCode.InternalServerError;
                    title = "An unexpected error occurred";
                    break;
            }

            var problem = new ProblemDetails
            {
                Type = "about:blank",
                Title = title,
                Status = status,
                Detail = ex.Message,
                Instance = context.Request?.Path
            };

            var json = JsonSerializer.Serialize(problem, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = status;
            return context.Response.WriteAsync(json);
        }
    }
}
