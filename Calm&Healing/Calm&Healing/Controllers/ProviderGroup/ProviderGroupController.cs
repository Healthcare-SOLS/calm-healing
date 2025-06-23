using Calm_Healing.DAL.DTOs;
using Calm_Healing.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Calm_Healing.Controllers.Authentication
{
    [Route("api/v1")]
    [ApiController]
    public class ProviderGroupController : ControllerBase
    {
        private readonly IAuthentication _auht;

        public ProviderGroupController(IAuthentication auht)
        {
            _auht = auht;
        }

        [HttpPost("provider-login")]
        public async Task<IActionResult> AdminLogin([FromBody] ProviderGroupLoginDTO loginDTO)
        {
            var result = await _auht.LoginUser(loginDTO);
            return Ok(result);
        }

        [HttpPost("provider-registration")]
        public async Task<IActionResult> AdminRegistration([FromBody] ProviderGroupRegisterDTO registerDTO)
        {
            var result = await _auht.RegisteredUser(registerDTO);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenResponse tokenResponse)
        {
            var result = await _auht.RefreshToken(tokenResponse);
            return Ok(result);
        }
    }
}
