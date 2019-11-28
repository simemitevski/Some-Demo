using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SecurityDemo.Domain.DataObjects;
using SecurityDemo.Domain.Logging;
using SecurityDemo.Services.Definition;

namespace SecurityDemo.Services.Services.Implementation.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IPasswordHasher<object> passwordHasher;
        private readonly ILoggerManager logger;
        public SecurityService(IHttpContextAccessor httpContextAccessor, 
            IPasswordHasher<object> passwordHasher,
            ILoggerManager logger)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.passwordHasher = passwordHasher;
            this.logger = logger;
        }

        public string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                //
            }

            return this.passwordHasher.HashPassword(new object(), password);
        }

        public async Task<ClaimsPrincipal> LoginAsync(IdentityData identityData)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, identityData.UserName),
                new Claim(nameof(IdentityData.Id), identityData.Id.ToString() ?? string.Empty),
                new Claim(nameof(IdentityData.Token), identityData.Token),
                new Claim(nameof(IdentityData.FirstName), identityData.FirstName ?? string.Empty),
                new Claim(nameof(IdentityData.FirstName), identityData.FirstName ?? string.Empty),
                new Claim(nameof(IdentityData.IpAddress), identityData.IpAddress ?? string.Empty),
                new Claim(nameof(IdentityData.IsSystemAdmin), identityData.IsSystemAdmin.ToString() ?? string.Empty),
                new Claim(nameof(IdentityData.LastLoginDateTime), identityData.LastLoginDateTime ?? string.Empty)
            };

            foreach (var roleName in identityData.RoleNames)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleName ?? string.Empty));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await this.httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
                //,
                //new AuthenticationProperties
                //{
                //    ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(options.IdleTimeout)),
                //    AllowRefresh = true
                //}
                );
            logger.LogInfo($"SystemInfoMessage: User {identityData.UserName} logged in!");
            return principal;
        }

        public async Task LogoutAsync()
        {
            await this.httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public bool VerifyHashedPassword(string hashedPassword, string password)
        {
            return this.passwordHasher.VerifyHashedPassword(new object(), hashedPassword, password) == PasswordVerificationResult.Success;
        }
    }
}
