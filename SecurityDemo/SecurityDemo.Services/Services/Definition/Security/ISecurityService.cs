using SecurityDemo.Domain.DataObjects;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SecurityDemo.Services.Definition
{
    public interface ISecurityService
    {
        string HashPassword(string password);

        bool VerifyHashedPassword(string hashedPassword, string providedPassword);

        Task<ClaimsPrincipal> LoginAsync(IdentityData identityData);

        Task LogoutAsync();
    }
}
