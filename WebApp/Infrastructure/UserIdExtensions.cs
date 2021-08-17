using System.Security.Claims;
using static WebApp.WebConstants;

namespace WebApp.Infrastructure
{
    public static class UserIdExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AdminRoleName);
        }

    }
}
