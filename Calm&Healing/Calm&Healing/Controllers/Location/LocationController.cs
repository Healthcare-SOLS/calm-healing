using Calm_Healing.DAL.DTOs;
using Calm_Healing.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using static Calm_Healing.DAL.DTOs.LocationDto;

namespace Calm_Healing.Controllers.Location
{
    [ApiController]
    [Route("api/v1")]
    // [Authorize] // Assuming authentication is required
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        /// <summary>
        /// Create new location
        /// </summary>
        [HttpPost("Create-Location")]
        public async Task<ActionResult<ResponseDto<object>>> AddLocation([FromBody] LocationDto location)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResponseDto<object> { Code = "BAD_REQUEST", Message = "Invalid input data" });
                }

                await _locationService.AddLocationAsync(location);

                return Ok(ResponseDto<object>.Success("LOCATION_CREATED_RESPONSE", "Location created successfully"));
            }
            catch (Exception ex)
            {
                // Handle any other unexpected errors
                return StatusCode(500, new ResponseDto<object> { 
                    Code = "INTERNAL_ERROR", 
                    Message = "An unexpected error occurred while creating location" 
                });
            }
        }

        /// <summary>
        /// Update existing location
        /// </summary>
        [HttpPut("Update-Location")]
        public async Task<ActionResult<ResponseDto<object>>> UpdateLocation([FromBody] LocationResDto location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDto<object> { Code = "BAD_REQUEST", Message = "Invalid input data" });
            }

            try
            {
                await _locationService.UpdateLocationAsync(location);
                return Ok(ResponseDto<object>.Success("LOCATION_UPDATED_RESPONSE", "Location updated successfully"));
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new ResponseDto<object> { Code = "BAD_REQUEST", Message = ex.Message });
            }
            catch (Exception ex)
            {
                // Optional: log the exception here
                return StatusCode(500, new ResponseDto<object>
                {
                    Code = "INTERNAL_SERVER_ERROR",
                    Message = "An unexpected error occurred. Please try again later."
                });
            }
        }


        /// <summary>
        /// Get location by ID
        /// </summary>
        [HttpGet("LocationById/{locationId}")]
        public async Task<ActionResult<ResponseDto<LocationResDto>>> GetLocationById([FromRoute] Guid locationId)
        {
            try
            {
                var location = await _locationService.GetLocationByIdAsync(locationId);
                return Ok(ResponseDto<LocationResDto>.WithData(location, "OK", "Success"));
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new ResponseDto<object> { Code = "BAD_REQUEST", Message = ex.Message });
            }
        }

        /// <summary>
        /// Get all locations with pagination
        /// </summary>
        [HttpGet("GetAllLocations")]
        public async Task<ActionResult<ResponseDto<PagedResult<LocationResDto>>>> GetAllLocations(
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10,
            [FromQuery] string searchString = null,
            [FromQuery] bool? active = null,
            [FromQuery] bool? archive = null)
        {
            try
            {
                var result = await _locationService.GetAllLocationsAsync(page, pageSize, searchString, active, archive);
                return Ok(ResponseDto<PagedResult<LocationResDto>>.WithData(result));

            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                // _logger.LogError(ex, "Error occurred while retrieving locations.");

                throw new ApplicationException("An error occurred while retrieving the location list.", ex);
            }
        }

        /// <summary>
        /// Get all locations associated with a specific clinician
        /// </summary>
        [HttpGet("Clinician-Location/{clinicianId}")]
        public async Task<ActionResult<PagedResult<Dictionary<string, string>>>> GetAllClinicianLocations(
            [FromRoute] Guid clinicianId, [FromQuery] int page = 0, [FromQuery] int size = 10, [FromQuery] string search = null)
        {
            try
            {
                var result = await _locationService.GetAllClinicianLocationsAsync(clinicianId, page, size, search);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                // _logger.LogError(ex, "Error occurred while retrieving locations.");

                throw new ApplicationException("An error occurred while retrieving the location list.", ex);
            }
        }

        /// <summary>
        /// Enable or disable location
        /// </summary>
        [HttpPut("location/Change-Status/{locationId}")]
        public async Task<ActionResult<ResponseDto<object>>> ChangeLocationStatus(
            [FromRoute] Guid locationId,
            [FromQuery] bool status)
        {
            try
            {
                await _locationService.ChangeLocationStatusAsync(locationId, status);
                string responseCode = status ? "LOCATION_ENABLED" : "LOCATION_DISABLED";
                string message = status ? "Location enabled successfully" : "Location disabled successfully";
                return Ok(ResponseDto<object>.Success(responseCode, message));
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new ResponseDto<object> { Code = "BAD_REQUEST", Message = ex.Message });
            }
        }

        /// <summary>
        /// Archive or restore location
        /// </summary>
        [HttpPut("location/archive-restore/{locationId}")]
        public async Task<ActionResult<ResponseDto<object>>> ArchiveRestoreLocation(
            [FromRoute] Guid locationId,
            [FromQuery] bool flag)
        {
            try
            {
                await _locationService.ArchiveRestoreLocationAsync(locationId, flag);
                string responseCode = flag ? "LOCATION_ARCHIVED" : "LOCATION_RESTORED";
                string message = flag ? "Location archived successfully" : "Location restored successfully";
                return Ok(ResponseDto<object>.Success(responseCode, message));
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new ResponseDto<object> { Code = "BAD_REQUEST", Message = ex.Message });
            }
        }
    }
}
