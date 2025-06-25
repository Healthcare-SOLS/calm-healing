using Calm_Healing.DAL.DTOs;
using Calm_Healing.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calm_Healing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderGroupProfileController : ControllerBase
    {
        private readonly IProviderGroupService _service; 

        public ProviderGroupProfileController(IProviderGroupService service) 
        {
            _service = service;
        }
        [HttpGet("GetProviderGroupDetails")]
        public async Task<IActionResult> GetPracticeDetails()
        {
            var result = await _service.GetPracticeDetailsAsync();
            if (result == null)
                return NotFound("Practice details not found.");

            return Ok(result);
        }
        [HttpPut("UpdateProviderGroupDetails")]
        public async Task<IActionResult> UpdatePractice([FromBody] PracticeUpdateDto dto)
        {
            var updated = await _service.UpdatePracticeAsync(dto);
            if (!updated)
                return NotFound("Practice not found or update failed.");

            return Ok("Practice updated successfully.");
        }

    }
}
