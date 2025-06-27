using Calm_Healing.DAL.DTOs;
using Calm_Healing.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calm_Healing.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProvideUserController : ControllerBase
    {
        private readonly IProvideUserService _service;

        public ProvideUserController(IProvideUserService service)
        {
            _service = service;
        }

        [HttpGet("GetAllProviders")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound(new { message = $"User with ID {id} not found." });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProvideUserCreateUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddAsync(dto);
            return Ok(new { message = "User created successfully." });
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Put(long id, [FromBody] ProvideUserCreateUpdateDTO dto)
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
