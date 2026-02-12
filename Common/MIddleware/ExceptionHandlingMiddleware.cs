using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json;
using Microsoft.Extensions.Hosting;

namespace InnoClinic.Common.MIddleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, IWebHostEnvironment env)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var traceId = Guid.NewGuid().ToString();
            _logger.LogError(ex, $"Error ID: {traceId}. Messsage: {ex.Message}");

            var (statusCode, safeMessage) = ex switch
            {
                OperationCanceledException => (499, "Запрос был отменен пользователем"),
                UnauthorizedAccessException => (401, "Доступ запрещен"),
                _ => (500, "На сервере произошла непредвиденная ошибка. Обратить в поддержку.")
            };

            await HandleExceptionAsync(context, statusCode, safeMessage, traceId, env, ex);
        }
    }

    public static Task HandleExceptionAsync
        (HttpContext context,
        int statusCode,
        string message,
        string traceId,
        IWebHostEnvironment env,
        Exception ex)    
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new
        {
            Status = statusCode,
            Message = message,
            TraceId = traceId,
            Details = env.IsDevelopment() ? ex.ToString() : null
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
