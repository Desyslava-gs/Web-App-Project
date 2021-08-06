using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp.Infrastructure
{
    public static class UserIdExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
            

        //public static bool IsAdmin(this ClaimsPrincipal user)
        //    => user.IsInRole(AdministratorRoleName);
    }
}
