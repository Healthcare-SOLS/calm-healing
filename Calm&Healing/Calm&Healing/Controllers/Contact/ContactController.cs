using Calm_Healing.DAL.DTOs;
using Calm_Healing.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Calm_Healing.Controllers.Contact
{
    [ApiController]
    [Route("api/v1")]
    // [Authorize] // Assuming authentication is required
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IContactService contactService, ILogger<ContactController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        /// <summary>
        /// Create new contact
        /// </summary>
        [HttpPost("Create-Contact")]
        public async Task<ActionResult<ResponseDto<object>>> AddContact([FromBody] ContactDto contact)
        {
            _logger.LogInformation("AddContact API called at {Time}", DateTime.UtcNow);
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state received at AddContact API");
                    return BadRequest(new ResponseDto<object> { Code = "BAD_REQUEST", Message = "Invalid input data" });
                }

                await _contactService.AddContactAsync(contact);

                _logger.LogInformation("Contact created successfully at {Time}", DateTime.UtcNow);
                return Ok(ResponseDto<object>.Success("CONTACT_CREATED_RESPONSE", "Contact created successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in AddContact API");
                // Handle any other unexpected errors
                return StatusCode(500, new ResponseDto<object>
                {
                    Code = "INTERNAL_ERROR",
                    Message = "An unexpected error occurred while creating contact"
                });
            }
        }

        /// <summary>
        /// Update existing contact
        /// </summary>
        [HttpPut("Update-Contact")]
        public async Task<ActionResult<ResponseDto<object>>> UpdateContact([FromBody] ContactResDto contact)
        {
            _logger.LogInformation("UpdateContact API called at {Time} for contact ID: {ContactId}", DateTime.UtcNow, contact.Uuid);
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state received at UpdateContact API");
                    return BadRequest(new ResponseDto<object> { Code = "BAD_REQUEST", Message = "Invalid input data" });
                }

                await _contactService.UpdateContactAsync(contact);

                _logger.LogInformation("Contact updated successfully at {Time} for contact ID: {ContactId}", DateTime.UtcNow, contact.Uuid);
                return Ok(ResponseDto<object>.Success("CONTACT_UPDATED_RESPONSE", "Contact updated successfully"));
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Contact not found in UpdateContact API for contact ID: {ContactId}", contact.Uuid);
                return BadRequest(new ResponseDto<object> { Code = "BAD_REQUEST", Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in UpdateContact API for contact ID: {ContactId}", contact.Uuid);
                return StatusCode(500, new ResponseDto<object>
                {
                    Code = "INTERNAL_SERVER_ERROR",
                    Message = "An unexpected error occurred. Please try again later."
                });
            }
        }

        /// <summary>
        /// Get contact by ID
        /// </summary>
        [HttpGet("ContactById/{contactId}")]
        public async Task<ActionResult<ResponseDto<ContactResDto>>> GetContactById([FromRoute] Guid contactId)
        {
            _logger.LogInformation("GetContactById API called at {Time} for contact ID: {ContactId}", DateTime.UtcNow, contactId);
            try
            {
                var contact = await _contactService.GetContactByIdAsync(contactId);
                _logger.LogInformation("Contact retrieved successfully at {Time} for contact ID: {ContactId}", DateTime.UtcNow, contactId);
                return Ok(ResponseDto<ContactResDto>.WithData(contact, "OK", "Success"));
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Contact not found in GetContactById API for contact ID: {ContactId}", contactId);
                return BadRequest(new ResponseDto<object> { Code = "BAD_REQUEST", Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in GetContactById API for contact ID: {ContactId}", contactId);
                return StatusCode(500, new ResponseDto<object>
                {
                    Code = "INTERNAL_SERVER_ERROR",
                    Message = "An unexpected error occurred while retrieving contact details."
                });
            }
        }

        /// <summary>
        /// Get all contacts with pagination
        /// </summary>
        [HttpGet("Get-Contacts")]
        public async Task<ActionResult<ResponseDto<PagedResult<ContactResDto>>>> GetAllContacts(
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchString = null,
            [FromQuery] bool? active = null,
            [FromQuery] bool? archive = null)
        {
            _logger.LogInformation("GetAllContacts API called at {Time} with parameters: page={Page}, pageSize={PageSize}, search={Search}, active={Active}, archive={Archive}",
                DateTime.UtcNow, page, pageSize, searchString, active, archive);
            try
            {
                var result = await _contactService.GetAllContactsAsync(page, pageSize, searchString, active, archive);
                _logger.LogInformation("Retrieved {Count} contacts out of {Total} total records at {Time}",
                    result.Items.Count, result.TotalCount, DateTime.UtcNow);
                return Ok(ResponseDto<PagedResult<ContactResDto>>.WithData(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in GetAllContacts API");
                return StatusCode(500, new ResponseDto<object>
                {
                    Code = "INTERNAL_SERVER_ERROR",
                    Message = "An unexpected error occurred while retrieving contacts."
                });
            }
        }

        /// <summary>
        /// Enable or disable contact
        /// </summary>
        [HttpPut("Change-Contact-Status/{contactId}")]
        public async Task<ActionResult<ResponseDto<object>>> ChangeContactStatus([FromRoute] Guid contactId, [FromQuery] bool status)
        {
            _logger.LogInformation("ChangeContactStatus API called at {Time} for contact ID: {ContactId}, new status: {Status}",
                DateTime.UtcNow, contactId, status);
            try
            {
                await _contactService.ChangeContactStatusAsync(contactId, status);
                string responseCode = status ? "CONTACT_ENABLED" : "CONTACT_DISABLED";
                string message = status ? "Contact enabled successfully" : "Contact disabled successfully";

                _logger.LogInformation("Contact status changed successfully to {Status} at {Time} for contact ID: {ContactId}",
                    status, DateTime.UtcNow, contactId);
                return Ok(ResponseDto<object>.Success(responseCode, message));
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Contact not found in ChangeContactStatus API for contact ID: {ContactId}", contactId);
                return BadRequest(new ResponseDto<object> { Code = "BAD_REQUEST", Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in ChangeContactStatus API for contact ID: {ContactId}", contactId);
                return StatusCode(500, new ResponseDto<object>
                {
                    Code = "INTERNAL_SERVER_ERROR",
                    Message = "An unexpected error occurred while changing contact status."
                });
            }
        }

        /// <summary>
        /// Archive or restore contact
        /// </summary>
        [HttpPut("Archive-Restore-Contact/{contactId}")]
        public async Task<ActionResult<ResponseDto<object>>> ArchiveRestoreContact([FromRoute] Guid contactId, [FromQuery] bool flag)
        {
            _logger.LogInformation("ArchiveRestoreContact API called at {Time} for contact ID: {ContactId}, archive flag: {Flag}",
                DateTime.UtcNow, contactId, flag);
            try
            {
                await _contactService.ArchiveRestoreContactAsync(contactId, flag);
                string responseCode = flag ? "CONTACT_ARCHIVED" : "CONTACT_RESTORED";
                string message = flag ? "Contact archived successfully" : "Contact restored successfully";

                _logger.LogInformation("Contact {Action} successfully at {Time} for contact ID: {ContactId}",
                    flag ? "archived" : "restored", DateTime.UtcNow, contactId);
                return Ok(ResponseDto<object>.Success(responseCode, message));
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Contact not found in ArchiveRestoreContact API for contact ID: {ContactId}", contactId);
                return BadRequest(new ResponseDto<object> { Code = "BAD_REQUEST", Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in ArchiveRestoreContact API for contact ID: {ContactId}", contactId);
                return StatusCode(500, new ResponseDto<object>
                {
                    Code = "INTERNAL_SERVER_ERROR",
                    Message = "An unexpected error occurred while archiving/restoring contact."
                });
            }
        }

    }
}
