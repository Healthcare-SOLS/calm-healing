using System.Security.Claims;

namespace Calm_Healing.Utilities.IUtilities
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        bool ValidateRefreshToken(string userId, string refreshToken);
        void ReplaceRefreshToken(string userId, string newRefreshToken);
    }
}
