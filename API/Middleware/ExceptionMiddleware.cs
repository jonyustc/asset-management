using System.Text.Json;
using API.Errors;

namespace API.Middleware
{
   public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                
                var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(_env.IsDevelopment() 
                    ? 
                    new ApiException((int)StatusCodes.Status500InternalServerError,ex.Message,ex.StackTrace.ToString())
                    :
                    new ApiException((int)StatusCodes.Status500InternalServerError,ex.Message,ex.StackTrace.ToString())
                    ,jsonOptions);

                await context.Response.WriteAsync(json);


                
            }
        } 
    }
}