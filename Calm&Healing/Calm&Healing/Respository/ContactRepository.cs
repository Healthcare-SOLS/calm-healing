using Calm_Healing.DAL.Models;
using Calm_Healing.Respository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Calm_Healing.Respository
{
    public class ContactRepository : IContactRepository
    {
        private readonly TenantDbContext _context;
        private readonly ILogger<ContactRepository> _logger;

        public ContactRepository(TenantDbContext context, ILogger<ContactRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Contact> AddContactAsync(Contact contact)
        {
            _logger.LogInformation("AddContactAsync repository method called at {Time} for contact: {Name}", DateTime.UtcNow, contact.Name);
            try
            {
                await _context.Contacts.AddAsync(contact);
                _logger.LogDebug("Contact entity added to context, preparing to save changes");

                await _context.SaveChangesAsync();
                _logger.LogInformation("Contact saved successfully to database with UUID: {Uuid}", contact.Uuid);

                return contact;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error occurred while saving contact: {Name}", contact.Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving contact to the database: {Name}", contact.Name);
                throw;
            }
        }

        public async Task<Contact> FindByUuidAsync(Guid uuid)
        {
            _logger.LogInformation("FindByUuidAsync repository method called at {Time} for contact ID: {ContactId}",
                DateTime.UtcNow, uuid);
            try
            {
                var contact = await _context.Contacts
                    .Include(c => c.Address)
                    .FirstOrDefaultAsync(c => c.Uuid == uuid);

                if (contact == null)
                {
                    _logger.LogWarning("No contact found with UUID: {Uuid}", uuid);
                }
                else
                {
                    _logger.LogDebug("Contact found: {Name} with UUID: {Uuid}", contact.Name, uuid);
                }

                return contact;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving contact with UUID: {Uuid}", uuid);
                throw new ApplicationException($"An error occurred while retrieving contact with id: {uuid}", ex);
            }
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            _logger.LogInformation("UpdateContactAsync repository method called at {Time} for contact ID: {ContactId}",
                DateTime.UtcNow, contact.Uuid);
            try
            {
                _context.Contacts.Update(contact);
                _logger.LogDebug("Contact entity marked for update in context: {Name}", contact.Name);

                await _context.SaveChangesAsync();
                _logger.LogInformation("Contact updated successfully in database with UUID: {Uuid}", contact.Uuid);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency error occurred while updating contact with UUID: {Uuid}", contact.Uuid);
                throw new ApplicationException("The contact was modified by another user. Please refresh and try again.", ex);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error occurred while updating contact with UUID: {Uuid}", contact.Uuid);
                throw new ApplicationException("A database error occurred while updating the contact.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating contact with UUID: {Uuid}", contact.Uuid);
                throw new ApplicationException("An error occurred while updating the contact.", ex);
            }
        }

         public async Task<(List<Contact> Items, int TotalCount)> GetAllContactsAsync(
            string? searchString, bool? active, bool? archive, int page, int pageSize)
        {
            _logger.LogInformation("GetAllContactsAsync repository method called at {Time} with parameters: " +
                "search={Search}, active={Active}, archive={Archive}, page={Page}, pageSize={PageSize}",
                DateTime.UtcNow, searchString, active, archive, page, pageSize);
            try
            {
                var query = _context.Contacts
                    .Include(c => c.Address)
                    .AsQueryable();

                _logger.LogDebug("Building query for contacts");

                // Apply archive filter
                if (active.HasValue)
                {
                    query = query.Where(c => c.Archive == archive);
                    _logger.LogDebug("Applied archive filter: {Archive}", archive);
                }
                // Apply active filter if provided
                if (active.HasValue)
                {
                    query = query.Where(c => c.Status == active.Value);
                    _logger.LogDebug("Applied active filter: {Active}", active.Value);
                }

                // Apply search filter if provided
                if (!string.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    query = query.Where(c =>
                        (c.Name != null && c.Name.ToLower().Contains(searchString)) ||
                        (c.EmailId != null && c.EmailId.ToLower().Contains(searchString)) ||
                        (c.ContactType.ToString().ToLower().Contains(searchString))
                    );
                    _logger.LogDebug("Applied search filter: {SearchString}", searchString);
                }

                // Get total count for pagination
                var totalCount = await query.CountAsync();
                _logger.LogDebug("Total count of contacts matching criteria: {TotalCount}", totalCount);

                // Apply pagination and ordering
                var items = await query
                    .OrderByDescending(c => c.Created)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                _logger.LogInformation("Retrieved {Count} contacts out of {Total} total records",
                    items.Count, totalCount);

                return (items, totalCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving contacts with search: {Search}", searchString);
                throw new ApplicationException("An error occurred while retrieving the contact list.", ex);
            }
        }
    }
}
