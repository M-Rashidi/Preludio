using System.Security.Claims;

namespace Preludio.Core
{
    public interface IUserResolver
    {
        ClaimsPrincipal GetCurrentUser();
    }
}
