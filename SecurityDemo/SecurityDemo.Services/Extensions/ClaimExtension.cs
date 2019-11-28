using SecurityDemo.Domain.DataObjects;
using System.Security.Claims;

namespace SecurityDemo.Services.Extensions
{
    public static class ClaimExtension
    {
        public static string GetLoggedInUserId(this ClaimsPrincipal principal) =>
            principal.FindFirst(nameof(IdentityData.Id))?.Value ?? string.Empty;

        public static bool IsLoggedIn(this ClaimsPrincipal principal) =>
            principal.Identity.IsAuthenticated;

        public static bool IsLoggedIn(this ClaimsIdentity identity) =>
            identity.IsAuthenticated;
    }
}
