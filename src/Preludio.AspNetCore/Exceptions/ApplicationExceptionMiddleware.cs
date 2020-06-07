using Microsoft.AspNetCore.Builder;

namespace Preludio.AspNetCore.Exceptions
{
    public static class ApplicationExceptionMiddleware
    {
        public static void UseApplicationExceptionMiddleware(this IApplicationBuilder applicationBuilder, int bcCode)
        {
            applicationBuilder.UseMiddleware(typeof(ExceptionHandlerMiddleware), bcCode);
        }
    }
}