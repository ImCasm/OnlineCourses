using Application.ExceptionHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPI.Middleware
{
    public class ExceptionHandlerMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ExceptionHandlerAsync(context, ex, _logger);
            }
        }

        private async Task ExceptionHandlerAsync(HttpContext context, Exception ex, ILogger<ExceptionHandlerMiddleware> logger)
        {
            object errors = null;

            switch (ex)
            {
                case ExceptionHandler me:
                    logger.LogError(ex, "Manejador Error");
                    errors = me.Errors;
                    context.Response.StatusCode = (int) me.Code;
                    break;
                case Exception e:
                    logger.LogError(ex, "Error de servidor");
                    errors = string.IsNullOrWhiteSpace(e.Message)? "Error" : e.Message;
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";

            if (errors != null)
            {
                var result = JsonSerializer.Serialize(new { errores = errors });
                await context.Response.WriteAsync(result);
            }
        }
    }
}
