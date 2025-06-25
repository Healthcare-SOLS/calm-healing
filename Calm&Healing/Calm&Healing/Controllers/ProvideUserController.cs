using Calm_Healing.DAL.DTOs;
using Calm_Healing.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calm_Healing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvideUserController : ControllerBase
    {
        private readonly IProvideUserService _service;

        public ProvideUserController(IProvideUserService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpPost("{roleId:long}")]
        public async Task<IActionResult> Post([FromBody] ProvideUserDTO dto, long roleId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddAsync(dto, roleId);
            return Ok(new { message = "User created successfully." });
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Put(long id, [FromBody] ProvideUserDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Mismatched user ID");

            var updated = await _service.UpdateAsync(dto);
            if (!updated)
                return NotFound(new { message = "User not found." });

            return Ok(new { message = "User updated successfully." });
        }

    }
}
