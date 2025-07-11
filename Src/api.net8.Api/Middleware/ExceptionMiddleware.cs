﻿using System.Diagnostics;

namespace api_net9.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                var response = _env.IsDevelopment() == true
                    ? new { message = ex.Message, stackTrace = ex.StackTrace }
                    : new { message = "خطای رخ داده است.", stackTrace = "" };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
