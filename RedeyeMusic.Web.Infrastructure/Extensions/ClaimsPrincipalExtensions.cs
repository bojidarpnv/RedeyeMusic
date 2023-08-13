using System.Security.Claims;
using static RedeyeMusic.Common.GeneralApplicationConstants;
namespace RedeyeMusic.Web.Infrastrucutre.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AdminRoleName);
        }
    }
}
