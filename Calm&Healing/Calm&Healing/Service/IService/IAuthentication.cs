using Calm_Healing.DAL.DTOs;

namespace Calm_Healing.Service.IService
{
    public interface IAuthentication
    {
        Task<APIResponse<TokenResponse>> RefreshToken(TokenResponse tokenResponse);
        Task<APIResponse<bool>> RegisteredUser(AdminRegisterDTO registerDTO);
        Task<APIResponse<bool>> RegisteredUser(ProviderGroupRegisterDTO registerDTO);
        Task<APIResponse<bool>> LoginUser(AdminLoginDTO loginDTO);
        Task<APIResponse<bool>> LoginUser(ProviderGroupLoginDTO loginDTO);
        Task<APIResponse<bool>> GroupRegistration();
    }
}
