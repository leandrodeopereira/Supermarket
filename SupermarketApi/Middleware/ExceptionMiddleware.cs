namespace SupermarketApi.Middleware
{
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using SupermarketApi.Errors;
    using static System.Net.HttpStatusCode;

    public sealed class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment environment;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IHostEnvironment environment)
        {
            this.next = next;
            this.logger = logger;
            this.environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // If there is no exception, then the request moves on to its next stage.
                await this.next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)InternalServerError;

                var response = this.environment.IsDevelopment() ?
                    new ApiExceptionResponse(InternalServerError, ex.Message, ex.StackTrace?.ToString()) :
                    new ApiExceptionResponse(InternalServerError, ex.Message);

                var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, option);
                await context.Response.WriteAsync(json).ConfigureAwait(false);
            }
        }
    }
}
