using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Preludio.Core;

namespace Preludio.AspNetCore
{
    public class HttpContextUserResolverAdapter : IUserResolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HttpContextUserResolverAdapter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal GetCurrentUser()
        {
            return _httpContextAccessor.HttpContext.User;
        }
    }
}
