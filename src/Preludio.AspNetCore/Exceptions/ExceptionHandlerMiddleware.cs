using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Preludio.Core.Exceptions;
using Preludio.Core.Logging;

namespace Preludio.AspNetCore.Exceptions
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly int _bcCode;
        
        public ExceptionHandlerMiddleware(RequestDelegate next,int bcCode)
        {
            _next = next;
            _bcCode = bcCode;
        }
        public async Task Invoke(HttpContext httpContext,ILogger logger)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception exception)
            {
                await HandleException(httpContext, exception);
                logger.Error(exception);
            }
        }

        private async Task HandleException(HttpContext httpContext, Exception exception)
        {

            if (exception is UnauthorizedAccessException)
                await HandleUnAuthorizeException(httpContext, exception);
            else if (exception is BusinessException)
                await HandleBusinessException(httpContext, exception);
            else
                await HandleDefaultException(httpContext);
        }

        private async Task HandleDefaultException(HttpContext httpContext)
        {
            var error = new ErrorDetails("unhandled exception", _bcCode);

            await WriteToResponse(httpContext, error);

        }

        private async Task HandleBusinessException(HttpContext httpContext, Exception exception)
        {
            var businessException = exception as BusinessException;
            var exceptionCode = businessException.Code;

            var error = ErrorDetails.Build(exceptionCode.Message, exceptionCode.Code, _bcCode);

            await WriteToResponse(httpContext, error);
        }

        private async Task HandleUnAuthorizeException(HttpContext httpContext, Exception exception)
        {
            var unauthorizedException = exception as UnauthorizedAccessException;

            var error = ErrorDetails.Build(unauthorizedException.Message, 0, _bcCode);

            await WriteToResponse(httpContext, error,401);
        }

        private static async Task WriteToResponse(HttpContext httpContext, ErrorDetails error,int statusCode=500)
        {
            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }
    }
}