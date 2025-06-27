using Calm_Healing.DAL.Models;
using Calm_Healing.Respository.IRepository;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Calm_Healing.Respository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly TenantDbContext _context;
        private readonly ILogger<LocationRepository> _logger;
        public LocationRepository(TenantDbContext context, ILogger<LocationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Location> AddLocationAsync(Location location)
        {
            _logger.LogInformation("AddLocationAsync repository method called at {Time} for location: {LocationName}", 
                DateTime.UtcNow, location.LocationName);
            try
            {
                await _context.Locations.AddAsync(location);
                _logger.LogDebug("Location entity added to context, preparing to save changes");
                
                await _context.SaveChangesAsync();
                _logger.LogInformation("Location saved successfully to database with UUID: {Uuid}", location.Uuid);
                
                return location;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error occurred while saving location: {LocationName}", location.LocationName);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving location to the database: {LocationName}", location.LocationName);
                throw;
            }
        }
         
        public async Task<Location> FindByUuidAsync(Guid uuid)
        {
            _logger.LogInformation("FindByUuidAsync repository method called at {Time} for location ID: {LocationId}", 
                DateTime.UtcNow, uuid);
            try
            {
                var location = await _context.Locations
                    .Include(l => l.Address)
                    .FirstOrDefaultAsync(l => l.Uuid == uuid);
                
                if (location == null)
                {
                    _logger.LogWarning("No location found with UUID: {Uuid}", uuid);
                }
                else
                {
                    _logger.LogDebug("Location found: {LocationName} with UUID: {Uuid}", location.LocationName, uuid);
                }
                
                return location;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving location with UUID: {Uuid}", uuid);
                throw new ApplicationException($"An error occurred while retrieving location with id: {uuid}", ex);
            }
        }

        public async Task<(List<Location> Items, int TotalCount)> GetAllLocationsAsync(
            string searchString, bool? active, bool? archive, int page, int pageSize)
        {
            _logger.LogInformation("GetAllLocationsAsync repository method called at {Time} with parameters: " +
                "search={Search}, active={Active}, archive={Archive}, page={Page}, pageSize={PageSize}", 
                DateTime.UtcNow, searchString, active, archive, page, pageSize);
            try
            {
                var query = _context.Locations
                    .Include(l => l.Address)
                    .AsQueryable();

                _logger.LogDebug("Building query for locations");

                if (archive.HasValue)
                {
                    query = query.Where(l => l.Archive == archive.Value);
                    _logger.LogDebug("Applied archive filter: {Archive}", archive.Value);
                }

                if (active.HasValue)
                {
                    query = query.Where(l => l.Status == active.Value);
                    _logger.LogDebug("Applied active filter: {Active}", active.Value);
                }

                if (!string.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    query = query.Where(l =>
                        (l.LocationName != null && l.LocationName.ToLower().Contains(searchString)) ||
                        (l.EmailId != null && l.EmailId.ToLower().Contains(searchString)) ||
                        (l.Fax != null && l.Fax.ToLower().Contains(searchString))
                    );
                    _logger.LogDebug("Applied search filter: {SearchString}", searchString);
                }

                var totalCount = await query.CountAsync();
                _logger.LogDebug("Total count of locations matching criteria: {TotalCount}", totalCount);

                var items = await query
                    .OrderByDescending(l => l.Created)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                _logger.LogInformation("Retrieved {Count} locations out of {Total} total records", 
                    items.Count, totalCount);

                return (items, totalCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving locations with search: {Search}", searchString);
                throw new ApplicationException("An error occurred while retrieving the location list.", ex);
            }
        }

        public async Task<(List<object[]> Items, int TotalCount)> GetAllClinicianLocationsAsync(
            Guid clinicianId, string search, int page, int size)
        {
            _logger.LogInformation("GetAllClinicianLocationsAsync repository method called at {Time} for clinician ID: {ClinicianId}, " +
                "search={Search}, page={Page}, size={Size}", 
                DateTime.UtcNow, clinicianId, search, page, size);
            try
            {
                // Create a query using LINQ instead of native SQL
                var query = from c in _context.Clinicians
                            join u in _context.Users on c.UserId equals u.Id
                            join cl in _context.ClinicianLocationMappings on c.Id equals cl.ClinicianId
                            join l in _context.Locations on cl.LocationId equals l.Id
                            where c.Uuid == clinicianId
                            select new object[] { l.Uuid.ToString(), l.LocationName };

                _logger.LogDebug("Building query for clinician locations");

                // Apply search filter if provided
                if (!string.IsNullOrEmpty(search))
                {
                    search = search.ToLower();
                    query = query.Where(item =>
                        ((string)item[1]).ToLower().Contains(search)
                    );
                    _logger.LogDebug("Applied search filter: {Search}", search);
                }

                // Get total count for pagination
                var totalCount = await query.CountAsync();
                _logger.LogDebug("Total count of clinician locations matching criteria: {TotalCount}", totalCount);

                // Apply pagination
                var items = await query
                    .Skip(page * size)
                    .Take(size)
                    .ToListAsync();

                _logger.LogInformation("Retrieved {Count} clinician locations out of {Total} total records for clinician ID: {ClinicianId}", 
                    items.Count, totalCount, clinicianId);

                return (items, totalCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving clinician locations for clinician ID: {ClinicianId}", clinicianId);
                throw new ApplicationException("An error occurred while retrieving clinician locations.", ex);
            }
        }

        public async Task UpdateLocationAsync(Location location)
        {
            _logger.LogInformation("UpdateLocationAsync repository method called at {Time} for location ID: {LocationId}", 
                DateTime.UtcNow, location.Uuid);
            try
            {
                _context.Locations.Update(location);
                _logger.LogDebug("Location entity marked for update in context: {LocationName}", location.LocationName);
                
                await _context.SaveChangesAsync();
                _logger.LogInformation("Location updated successfully in database with UUID: {Uuid}", location.Uuid);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency error occurred while updating location with UUID: {Uuid}", location.Uuid);
                throw new ApplicationException("The location was modified by another user. Please refresh and try again.", ex);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error occurred while updating location with UUID: {Uuid}", location.Uuid);
                throw new ApplicationException("A database error occurred while updating the location.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating location with UUID: {Uuid}", location.Uuid);
                throw new ApplicationException("An error occurred while updating the location.", ex);
            }
        }
    }
}
