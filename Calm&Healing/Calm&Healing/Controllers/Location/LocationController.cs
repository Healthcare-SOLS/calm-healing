using Calm_Healing.DAL.DTOs;
using Calm_Healing.Service.IService;
using Microsoft.AspNetCore.Mvc;
using static Calm_Healing.DAL.DTOs.LocationDto;

namespace Calm_Healing.Controllers.Location
{
    [ApiController]
    [Route("api/v1")]
    // [Authorize] // Assuming authentication is required
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly ILogger<LocationController> _logger;

        public LocationController(ILocationService locationService, ILogger<LocationController> logger)
        {
            _locationService = locationService;
            _logger = logger;
        }

        /// <summary>
        /// Create new location
        /// </summary>
        [HttpPost("Create-Location")]
        public async Task<ActionResult<ResponseDto<object>>> AddLocation([FromBody] LocationDto location)
        {
            _logger.LogInformation("AddLocation API called at {Time}", DateTime.UtcNow);
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state received at AddLocation API");
                    return BadRequest(new ResponseDto<object> { Code = "BAD_REQUEST", Message = "Invalid input data" });
                }

                await _locationService.AddLocationAsync(location);

                _logger.LogInformation("Location created successfully at {Time}", DateTime.UtcNow);
                return Ok(ResponseDto<object>.Success("LOCATION_CREATED_RESPONSE", "Location created successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in AddLocation API");
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
            _logger.LogInformation("UpdateLocation API called at {Time} for location ID: {LocationId}", DateTime.UtcNow, location.Uuid);
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state received at UpdateLocation API");
                    return BadRequest(new ResponseDto<object> { Code = "BAD_REQUEST", Message = "Invalid input data" });
                }

                await _locationService.UpdateLocationAsync(location);
                
                _logger.LogInformation("Location updated successfully at {Time} for location ID: {LocationId}", DateTime.UtcNow, location.Uuid);
                return Ok(ResponseDto<object>.Success("LOCATION_UPDATED_RESPONSE", "Location updated successfully"));
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Location not found in UpdateLocation API for location ID: {LocationId}", location.Uuid);
                return BadRequest(new ResponseDto<object> { Code = "BAD_REQUEST", Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in UpdateLocation API for location ID: {LocationId}", location.Uuid);
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
            _logger.LogInformation("GetLocationById API called at {Time} for location ID: {LocationId}", DateTime.UtcNow, locationId);
            try
            {
                var location = await _locationService.GetLocationByIdAsync(locationId);
                _logger.LogInformation("Location retrieved successfully at {Time} for location ID: {LocationId}", DateTime.UtcNow, locationId);
                return Ok(ResponseDto<LocationResDto>.WithData(location, "OK", "Success"));
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Location not found in GetLocationById API for location ID: {LocationId}", locationId);
                return BadRequest(new ResponseDto<object> { Code = "BAD_REQUEST", Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in GetLocationById API for location ID: {LocationId}", locationId);
                return StatusCode(500, new ResponseDto<object>
                {
                    Code = "INTERNAL_SERVER_ERROR",
                    Message = "An unexpected error occurred while retrieving location details."
                });
            }
        }

        /// <summary>
        /// Get all locations with pagination
        /// </summary>
        [HttpGet("Get-Locations")]
        public async Task<ActionResult<ResponseDto<PagedResult<LocationResDto>>>> GetAllLocations(
            [FromQuery] int page = 0, 
            [FromQuery] int pageSize = 10, 
            [FromQuery] string searchString = null,
            [FromQuery] bool? active = null,
            [FromQuery] bool? archive = null)
        {
            _logger.LogInformation("GetAllLocations API called at {Time} with parameters: page={Page}, pageSize={PageSize}, search={Search}, active={Active}, archive={Archive}", 
                DateTime.UtcNow, page, pageSize, searchString, active, archive);
            try
            {
                var result = await _locationService.GetAllLocationsAsync(page, pageSize, searchString, active, archive);
                _logger.LogInformation("Retrieved {Count} locations out of {Total} total records at {Time}", 
                    result.Items.Count, result.TotalCount, DateTime.UtcNow);
                return Ok(ResponseDto<PagedResult<LocationResDto>>.WithData(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in GetAllLocations API");
                return StatusCode(500, new ResponseDto<object>
                {
                    Code = "INTERNAL_SERVER_ERROR",
                    Message = "An unexpected error occurred while retrieving locations."
                });
            }
        }

        /// <summary>
        /// Get all locations associated with a specific clinician
        /// </summary>
        [HttpGet("Clinician-Location/{clinicianId}")]
        public async Task<ActionResult<PagedResult<Dictionary<string, string>>>> GetAllClinicianLocations(
            [FromRoute] Guid clinicianId, 
            [FromQuery] int page = 0, 
            [FromQuery] int size = 10, 
            [FromQuery] string search = null)
        {
            _logger.LogInformation("GetAllClinicianLocations API called at {Time} for clinician ID: {ClinicianId}, page={Page}, size={Size}, search={Search}", 
                DateTime.UtcNow, clinicianId, page, size, search);
            try
            {
                var result = await _locationService.GetAllClinicianLocationsAsync(clinicianId, page, size, search);
                _logger.LogInformation("Retrieved clinician locations successfully at {Time} for clinician ID: {ClinicianId}", 
                    DateTime.UtcNow, clinicianId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in GetAllClinicianLocations API for clinician ID: {ClinicianId}", clinicianId);
                return StatusCode(500, new ResponseDto<object>
                {
                    Code = "INTERNAL_SERVER_ERROR",
                    Message = "An unexpected error occurred while retrieving clinician locations."
                });
            }
        }

        /// <summary>
        /// Enable or disable location
        /// </summary>
        [HttpPut("Change-Location-Status/{locationId}")]
        public async Task<ActionResult<ResponseDto<object>>> ChangeLocationStatus([FromRoute] Guid locationId, [FromQuery] bool status)
        {
            _logger.LogInformation("ChangeLocationStatus API called at {Time} for location ID: {LocationId}, new status: {Status}", 
                DateTime.UtcNow, locationId, status);
            try
            {
                await _locationService.ChangeLocationStatusAsync(locationId, status);
                string responseCode = status ? "LOCATION_ENABLED" : "LOCATION_DISABLED";
                string message = status ? "Location enabled successfully" : "Location disabled successfully";
                
                _logger.LogInformation("Location status changed successfully to {Status} at {Time} for location ID: {LocationId}", 
                    status, DateTime.UtcNow, locationId);
                return Ok(ResponseDto<object>.Success(responseCode, message));
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Location not found in ChangeLocationStatus API for location ID: {LocationId}", locationId);
                return BadRequest(new ResponseDto<object> { Code = "BAD_REQUEST", Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in ChangeLocationStatus API for location ID: {LocationId}", locationId);
                return StatusCode(500, new ResponseDto<object>
                {
                    Code = "INTERNAL_SERVER_ERROR",
                    Message = "An unexpected error occurred while changing location status."
                });
            }
        }

        /// <summary>
        /// Archive or restore location
        /// </summary>
        [HttpPut("Archive-Restore/{locationId}")]
        public async Task<ActionResult<ResponseDto<object>>> ArchiveRestoreLocation([FromRoute] Guid locationId, [FromQuery] bool flag)
        {
            _logger.LogInformation("ArchiveRestoreLocation API called at {Time} for location ID: {LocationId}, archive flag: {Flag}", 
                DateTime.UtcNow, locationId, flag);
            try
            {
                await _locationService.ArchiveRestoreLocationAsync(locationId, flag);
                string responseCode = flag ? "LOCATION_ARCHIVED" : "LOCATION_RESTORED";
                string message = flag ? "Location archived successfully" : "Location restored successfully";
                
                _logger.LogInformation("Location {Action} successfully at {Time} for location ID: {LocationId}", 
                    flag ? "archived" : "restored", DateTime.UtcNow, locationId);
                return Ok(ResponseDto<object>.Success(responseCode, message));
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Location not found in ArchiveRestoreLocation API for location ID: {LocationId}", locationId);
                return BadRequest(new ResponseDto<object> { Code = "BAD_REQUEST", Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in ArchiveRestoreLocation API for location ID: {LocationId}", locationId);
                return StatusCode(500, new ResponseDto<object>
                {
                    Code = "INTERNAL_SERVER_ERROR",
                    Message = "An unexpected error occurred while archiving/restoring location."
                });
            }
        }
    }
}
