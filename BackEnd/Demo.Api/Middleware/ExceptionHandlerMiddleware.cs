using Demo.Application.CustomException;
using System.Net;
using System.Text.Json;

namespace Demo.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var result = string.Empty;
            List<string> errors = new List<string>();

            switch (exception)
            {
                case ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.ValdationErrors);
                    errors.AddRange(validationException.ValdationErrors);
                    break;
                case IdentityResultException identityException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(identityException.IdentityErrors);
                    errors.AddRange(identityException.IdentityErrors);
                    break;
                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = badRequestException.Message;
                    break;
                case UnauthorizedException unauthorizedException:
                    httpStatusCode = HttpStatusCode.Unauthorized;
                    result = unauthorizedException.Message;
                    break;
                case NotFoundException notFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    result = notFoundException.Message;
                    break;
                default:
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    result = "Internal Server Error";
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;

            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }
            if (errors.Count == 0)
                errors.Add(result);

            return context.Response.WriteAsync(JsonSerializer.Serialize(errors));
        }
    }
}
