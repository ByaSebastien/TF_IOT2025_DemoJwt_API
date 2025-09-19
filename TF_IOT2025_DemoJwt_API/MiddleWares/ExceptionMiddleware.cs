using System.Text.Json;

namespace TF_IOT2025_DemoJwt_API.MiddleWares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            int statusCode = 400;

            switch (ex)
            {
                case Exception:
                    statusCode = 418;
                    break;
                default:
                    throw new Exception("WTF");
            }

            context.Response.StatusCode = statusCode;

            await SendResponse(context, ex.Message);
        }

        private async Task SendResponse(HttpContext context, string message)
        {
            var response = new
            {
                message = message,
            };

            var responseText = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(responseText);
        }
    }
}
