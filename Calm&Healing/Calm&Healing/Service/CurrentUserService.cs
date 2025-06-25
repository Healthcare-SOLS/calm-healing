using Calm_Healing.Service.IService;
using System.Security.Claims;

namespace Calm_Healing.Service
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUsername()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name) ?? "System";
        }
    }
}
