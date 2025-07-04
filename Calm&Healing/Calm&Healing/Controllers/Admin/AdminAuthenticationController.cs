﻿using Calm_Healing.DAL.DTOs;
using Calm_Healing.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Calm_Healing.Controllers.Admin
{
    [Route("api/v1")]
    [ApiController]
    public class AdminAuthenticationController : ControllerBase
    {
        private readonly IAuthentication _auht;
       

        public AdminAuthenticationController(IAuthentication auht)
        {
            _auht = auht;
            
        }

        [HttpPost("Admin-Login")]
        public async Task<IActionResult> AdminLogin([FromBody] AdminLoginDTO loginDTO)
        {
            var result = await _auht.LoginUser(loginDTO);
            return Ok(result);
        }

        [HttpPost("Admin-Registration")]
        public async Task<IActionResult> AdminRegistration([FromBody] AdminRegisterDTO registerDTO)
        {
            var result = await _auht.RegisteredUser(registerDTO);
            return Ok(result);
        }

        [HttpPost("Group-Registration")]
        public async Task<IActionResult> GroupRegistration([FromBody] AdminRegisterDTO registerDTO)
        {
            var result = await _auht.GroupRegistration(); // You can pass DTO if needed
            return Ok(result);
        }

        [HttpPost("Refresh-Token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenResponse tokenResponse)
        {
            var result = await _auht.RefreshToken(tokenResponse);
            return Ok(result);
        }

        /*[HttpPost("create-schema")]
        public async Task<IActionResult> NewSchema()
        {
            await _dynamicSchemaService.CreateSchemaAndTablesAsync("Kamlesh80913");
            return Ok("Schema created successfully.");
        }*/
    }
}
