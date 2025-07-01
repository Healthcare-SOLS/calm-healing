using Calm_Healing.DAL.DTOs;
using Calm_Healing.DAL.Models;
using Calm_Healing.Respository.IRepository;
using Calm_Healing.Service.IService;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Calm_Healing.Service
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly ILogger<ContactService> _logger;

        public ContactService(IContactRepository contactRepository, ILogger<ContactService> logger)
        {
            _contactRepository = contactRepository;
            _logger = logger;
        }

        public async Task AddContactAsync(ContactDto contactDto)
        {
            try
            {
                _logger.LogInformation("AddContactAsync called at {Time}", DateTime.UtcNow);

                // Convert DTO to Entity
                var contact = new Contact
                {
                    Uuid = Guid.NewGuid(),
                    ContactType = contactDto.ContactType.ToString(),
                    Name = contactDto.Name,
                    ContactNumber = contactDto.ContactNumber,
                    FaxNumber = contactDto.FaxNumber,
                    EmailId = contactDto.EmailId,
                    Status = contactDto.Status,
                    Archive = false // Always set to false for new contacts
                };

                // Handle Address if provided
                if (contactDto.Address != null)
                {
                    contact.Address = new Address
                    {
                        Uuid = Guid.NewGuid(),
                        Line1 = contactDto.Address.Line1,
                        Line2 = contactDto.Address.Line2,
                        City = contactDto.Address.City,
                        State = contactDto.Address.State,
                        Zipcode = contactDto.Address.Zipcode
                    };
                }

                _logger.LogInformation("Contact successfully saved with UUID: {Uuid}", contact.Uuid);
                await _contactRepository.AddContactAsync(contact);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while creating a new contact");
                throw; // Rethrow to be handled by the controller
            }
        }

        public async Task UpdateContactAsync(ContactResDto contactDto)
        {
            try
            {
                _logger.LogInformation("UpdateContactAsync called at {Time} for contact ID: {ContactId}",
                    DateTime.UtcNow, contactDto.Uuid);

                var existingContact = await _contactRepository.FindByUuidAsync(contactDto.Uuid.Value);

                if (existingContact == null)
                {
                    _logger.LogWarning("Contact not found with ID: {ContactId}", contactDto.Uuid);
                    throw new KeyNotFoundException($"Cannot find contact with id: {contactDto.Uuid}");
                }

                _logger.LogDebug("Found existing contact: {Name}", existingContact.Name);

                // Update contact properties
                existingContact.ContactType = contactDto.ContactType.ToString();
                existingContact.Name = contactDto.Name;
                existingContact.ContactNumber = contactDto.ContactNumber;
                existingContact.FaxNumber = contactDto.FaxNumber;
                existingContact.EmailId = contactDto.EmailId;
                existingContact.Status = contactDto.Status;

                _logger.LogDebug("Updated basic contact properties for ID: {ContactId}", contactDto.Uuid);

                // Update address if provided
                if (contactDto.Address != null)
                {
                    if (existingContact.Address == null)
                    {
                        // Create new address if it doesn't exist
                        existingContact.Address = new Address
                        {
                            Uuid = contactDto.Address.Uuid ?? Guid.NewGuid(),
                            Line1 = contactDto.Address.Line1,
                            Line2 = contactDto.Address.Line2,
                            City = contactDto.Address.City,
                            State = contactDto.Address.State,
                            Zipcode = contactDto.Address.Zipcode,
                        };
                        _logger.LogDebug("Created new address for contact ID: {ContactId}", contactDto.Uuid);
                    }
                    else
                    {
                        // Update existing address
                        existingContact.Address.Line1 = contactDto.Address.Line1;
                        existingContact.Address.Line2 = contactDto.Address.Line2;
                        existingContact.Address.City = contactDto.Address.City;
                        existingContact.Address.State = contactDto.Address.State;
                        existingContact.Address.Zipcode = contactDto.Address.Zipcode;
                        _logger.LogDebug("Updated existing address for contact ID: {ContactId}", contactDto.Uuid);
                    }
                }

                await _contactRepository.UpdateContactAsync(existingContact);
                _logger.LogInformation("Contact successfully updated with ID: {ContactId}", contactDto.Uuid);
            }
            catch (KeyNotFoundException ex)
            {
                // Let this exception propagate as it's already logged
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating contact with ID: {ContactId}", contactDto.Uuid);
                throw new ApplicationException("An error occurred while updating contact.", ex);
            }
        }

        public async Task<ContactResDto> GetContactByIdAsync(Guid contactId)
        {
            try
            {
                _logger.LogInformation("GetContactByIdAsync called at {Time} for contact ID: {ContactId}",
                    DateTime.UtcNow, contactId);

                var contact = await _contactRepository.FindByUuidAsync(contactId);

                if (contact == null)
                {
                    _logger.LogWarning("Contact not found with ID: {ContactId}", contactId);
                    throw new KeyNotFoundException($"Cannot find contact with id: {contactId}");
                }

                _logger.LogDebug("Contact found with name: {Name}", contact.Name);

                var contactDto = new ContactResDto
                {

                    Uuid = contact.Uuid,
                    ContactType = Enum.TryParse<ContactType>(contact.ContactType, true, out var parsed) ? parsed : throw new ArgumentException("Invalid ContactType from DB"),
                    Name = contact.Name,
                    ContactNumber = contact.ContactNumber,
                    FaxNumber = contact.FaxNumber,
                    EmailId = contact.EmailId,
                    Status = contact.Status ?? true,
                    Archive = contact.Archive ?? false,
                    Address = contact.Address != null ? new AddressResDto
                    {
                        Uuid = contact.Address.Uuid,
                        Line1 = contact.Address.Line1,
                        Line2 = contact.Address.Line2,
                        City = contact.Address.City,
                        State = contact.Address.State,
                        Zipcode = contact.Address.Zipcode
                    } : null
                };

                _logger.LogInformation("Successfully retrieved and mapped contact with ID: {ContactId}", contactId);
                return contactDto;
            }
            catch (KeyNotFoundException ex)
            {
                // Let this exception propagate as it's already logged
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving contact by ID: {ContactId}", contactId);
                throw new ApplicationException("An error occurred while retrieving the contact by id.", ex);
            }
        }

        public async Task<PagedResult<ContactResDto>> GetAllContactsAsync(int page, int pageSize, string? searchString, bool? active, bool? archive)
        {
            try
            {
                _logger.LogInformation("GetAllContactsAsync called at {Time} with parameters: page={Page}, pageSize={PageSize}, search={Search}, active={Active}, archive={Archive}",
                    DateTime.UtcNow, page, pageSize, searchString, active, archive);

                var (items, totalCount) = await _contactRepository.GetAllContactsAsync(
                    searchString, active, archive, page, pageSize);

                _logger.LogDebug("Retrieved {Count} contacts from repository", items.Count);

                var contactDtos = items.Select(contact => new ContactResDto
                {
                    Uuid = contact.Uuid,
                    ContactType = Enum.TryParse<ContactType>(contact.ContactType, true, out var parsed) ? parsed : throw new ArgumentException("Invalid ContactType from DB"),
                    Name = contact.Name,
                    ContactNumber = contact.ContactNumber,
                    FaxNumber = contact.FaxNumber,
                    EmailId = contact.EmailId,
                    Status = contact.Status ?? true,
                    Archive = contact.Archive ?? false,
                    Address = contact.Address != null ? new AddressResDto
                    {
                        Uuid = contact.Address.Uuid,
                        Line1 = contact.Address.Line1,
                        Line2 = contact.Address.Line2,
                        City = contact.Address.City,
                        State = contact.Address.State,
                        Zipcode = contact.Address.Zipcode
                    } : null
                }).ToList();

                var result = new PagedResult<ContactResDto>
                {
                    Items = contactDtos,
                    TotalCount = totalCount,
                    PageSize = pageSize,
                    CurrentPage = page
                };

                _logger.LogInformation("Successfully mapped {Count} contacts to DTOs out of {Total} total records",
                    result.Items.Count, result.TotalCount);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving contacts with parameters: page={Page}, pageSize={PageSize}, search={Search}",
                    page, pageSize, searchString);
                throw new ApplicationException("An error occurred while retrieving the contact list.", ex);
            }
        }

        public async Task ChangeContactStatusAsync(Guid contactId, bool status)
        {
            try
            {
                _logger.LogInformation("ChangeContactStatusAsync called at {Time} for contact ID: {ContactId}, new status: {Status}",
                    DateTime.UtcNow, contactId, status);

                var contact = await _contactRepository.FindByUuidAsync(contactId);

                if (contact == null)
                {
                    _logger.LogWarning("Contact not found with ID: {ContactId}", contactId);
                    throw new KeyNotFoundException($"Cannot find contact with id: {contactId}");
                }

                _logger.LogDebug("Found contact: {Name}, current status: {CurrentStatus}",
                    contact.Name, contact.Status);

                contact.Status = status;
                contact.Modified = DateTime.UtcNow;

                await _contactRepository.UpdateContactAsync(contact);
                _logger.LogInformation("Contact status successfully changed to {Status} for contact ID: {ContactId}",
                    status, contactId);
            }
            catch (KeyNotFoundException ex)
            {
                // Let this exception propagate as it's already logged
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while changing status for contact ID: {ContactId}", contactId);
                throw new ApplicationException("An error occurred while changing contact status.", ex);
            }
        }

        public async Task ArchiveRestoreContactAsync(Guid contactId, bool flag)
        {
            try
            {
                _logger.LogInformation("ArchiveRestoreContactAsync called at {Time} for contact ID: {ContactId}, archive flag: {Flag}",
                    DateTime.UtcNow, contactId, flag);

                var contact = await _contactRepository.FindByUuidAsync(contactId);

                if (contact == null)
                {
                    _logger.LogWarning("Contact not found with ID: {ContactId}", contactId);
                    throw new KeyNotFoundException($"Cannot find contact with id: {contactId}");
                }

                _logger.LogDebug("Found contact: {Name}, current archive status: {CurrentArchive}",
                    contact.Name, contact.Archive);

                // Set archive status based on flag
                contact.Archive = flag;

                // Important: When archiving, also disable the contact
                // When restoring, also enable the contact
                contact.Status = !flag;

                contact.Modified = DateTime.UtcNow;

                await _contactRepository.UpdateContactAsync(contact);
                _logger.LogInformation("Contact {Action} successfully for contact ID: {ContactId}",
                    flag ? "archived" : "restored", contactId);
            }
            catch (KeyNotFoundException ex)
            {
                // Let this exception propagate as it's already logged
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while {Action} contact with ID: {ContactId}",
                    flag ? "archiving" : "restoring", contactId);
                throw new ApplicationException($"An error occurred while {(flag ? "archiving" : "restoring")} contact.", ex);
            }
        }
    }
}
