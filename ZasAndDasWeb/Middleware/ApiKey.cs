using ZasAndDasWeb.Services;

namespace ZasAndDasWeb.Middleware
{
    public class APIKeyValidationMiddleware(RequestDelegate next, ILogger<APIKeyValidationMiddleware> logger,
                                                IServiceProvider serviceProvider)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.ToString();
            if (path.Contains("Player/") || path.Contains("GetLevel")) await next(context);
            else
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var validate = scope.ServiceProvider.GetRequiredService<IAPIKeyValidationService>();
                    var apiKey = context.Request.Headers["APIKEY"];
                    logger.LogInformation($"User with api key {apiKey} made a request to {context.Request.Path}");
                    if (validate.IsValidAPIKey(apiKey))
                    {
                        logger.LogInformation("Key valid. Awaiting response");
                        await next(context);
                    }
                    else throw new UnauthorizedAccessException();
                }
            }
        }
    }
}
