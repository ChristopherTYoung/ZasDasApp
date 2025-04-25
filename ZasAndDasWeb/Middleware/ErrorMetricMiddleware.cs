using Microsoft.Extensions.Azure;
using ZasAndDasWeb.Services;

namespace ZasAndDasWeb.Middleware
{
    public class ErrorMetricMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly MetricService? _myService;

        public ErrorMetricMiddleware(RequestDelegate next, MetricService? myService)
        {
            _next = next;
            _myService = myService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            if (_myService != null)
                _myService.ResponseCodes.Record(context.Response.StatusCode);
        }

    }
}
