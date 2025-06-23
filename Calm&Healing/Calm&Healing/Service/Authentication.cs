using Calm_Healing.DAL.DTOs;
//using Calm_Healing.DAL.Models;
using Calm_Healing.Respository.IRepository;
using Calm_Healing.Service.IService;
using Calm_Healing.Utilities;
using Calm_Healing.Utilities.IUtilities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Calm_Healing.Service
{
    public class Authentication : IAuthentication
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;
        private readonly IGenericRepositoryFactory _repositoryFactory;
        public Authentication(ITokenService tokenService, IConfiguration config, IGenericRepositoryFactory repositoryFactory) 
        {
            _tokenService = tokenService;
            _config = config;
            _repositoryFactory = repositoryFactory;
        }
        public async Task<APIResponse<TokenResponse>> RefreshToken(TokenResponse tokenResponse)
        {
            var response = new APIResponse<TokenResponse>
            {
                Date = DateTime.UtcNow
            };

            var principal = _tokenService.GetPrincipalFromExpiredToken(tokenResponse.AccessToken);
            var userId = principal?.Identity?.Name;

            if (userId == null || !_tokenService.ValidateRefreshToken(userId, tokenResponse.RefreshToken))
            {
                response.Code = 401;
                response.Message = "Invalid refresh token.";
                return response;
            }

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            _tokenService.ReplaceRefreshToken(userId, newRefreshToken);

            var tokenResult = new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["JwtSettings:AccessTokenExpirationMinutes"]))
            };

            response.Code = 200;
            response.Message = "Token refreshed successfully.";
            response.Data = tokenResult;
            response.Token = newAccessToken;
            response.RefreshToken = newRefreshToken;

            return response;
        }
        public async Task<APIResponse<bool>> RegisteredUser(AdminRegisterDTO registerDTO)
        {
            try
            {
                var userRepo = _repositoryFactory.GetRepository<object>();
                var hashPassord = Sha256Hasher.HashPassword(registerDTO.Email, registerDTO.Password);
                var user = new 
                {
                    Uuid = Guid.NewGuid().ToString(),
                    FirstName = registerDTO.FirstName,
                    LastName = registerDTO.LastName,
                    Phone = registerDTO.Phone,
                    Email = registerDTO.Email,
                    Password = hashPassord, // 🔐 Hash in real apps!
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    //EmailVerifiedAt = null

                };
                await userRepo.AddAsync(user);

                return new APIResponse<bool>
                {
                    Code = 201,
                    Message = "User registered successfully",
                    Data = true,
                    Date = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<bool>
                {
                    Code = 500,
                    Message = $"Registration failed: {ex.Message}",
                    Data = false,
                    Date = DateTime.UtcNow
                };
            }
        }
        public async Task<APIResponse<bool>> RegisteredUser(ProviderGroupRegisterDTO registerDTO)
        {
            // You can add actual registration logic here...

            return new APIResponse<bool>
            {
                Date = DateTime.UtcNow,
                Code = 201,
                Message = "Registration successful",
                Data = true
            };
        }
        public async Task<APIResponse<bool>> GroupRegistration()
        {
            // You can add actual registration logic here...

            return new APIResponse<bool>
            {
                Date = DateTime.UtcNow,
                Code = 201,
                Message = "Registration successful",
                Data = true
            };
        }
        public async Task<APIResponse<bool>> LoginUser(ProviderGroupLoginDTO loginDTO)
        {
            // You can add actual login logic here...

            return new APIResponse<bool>
            {
                Date = DateTime.UtcNow,
                Code = 200,
                Message = "Login successful",
                Data = true,
                Token = "your-jwt-token-here", // Optional: if needed
                RefreshToken = "your-refresh-token-here" // Optional: if needed
            };
        }
        public async Task<APIResponse<bool>> LoginUser(AdminLoginDTO loginDTO)
        {
            // You can add actual login logic here...

            return new APIResponse<bool>
            {
                Date = DateTime.UtcNow,
                Code = 200,
                Message = "Login successful",
                Data = true,
                Token = "your-jwt-token-here", // Optional: if needed
                RefreshToken = "your-refresh-token-here" // Optional: if needed
            };
        }
    }
}
