using Microsoft.AspNetCore.Identity;

namespace WebAPI_v3.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);

    }
}
    