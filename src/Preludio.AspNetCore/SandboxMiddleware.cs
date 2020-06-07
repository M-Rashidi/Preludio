using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Preludio.AspNetCore.Utility;
using Preludio.DataAccess;

namespace Preludio.AspNetCore
{
    public class SandboxMiddleware
    {
        private readonly RequestDelegate _next;

        public SandboxMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, IConnectionManager connectionManager)
        {
            if (context.Request.Headers.ContainsKey(HeaderNames.DatabaseConnectionString))
            {
                var connectionString = context.Request.Headers[HeaderNames.DatabaseConnectionString].First();
                connectionManager.Override(connectionString);
            }
            await _next.Invoke(context);
        }
    }
}