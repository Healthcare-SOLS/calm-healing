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

        public LocationRepository(TenantDbContext context)
        {
            _context = context;
        }

        public async Task<Location> AddLocationAsync(Location location)
        {
            try
            {
                await _context.Locations.AddAsync(location);
                await _context.SaveChangesAsync();
                return location;
            }
            catch (Exception ex)
            {
                // Log the general exception
                // _logger.LogError(ex, "Error occurred while saving location");
                throw; // Rethrow to be handled by the service
            }
        }

        public async Task<Location> FindByUuidAsync(Guid uuid)
        {
            try
            {
                return await _context.Locations
                    .Include(l => l.Address)
                    .FirstOrDefaultAsync(l => l.Uuid == uuid);
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                // _logger.LogError(ex, "Error occurred while retrieving locations.");

                throw new ApplicationException("An error occurred while retrieving the location list.", ex);
            }
        }

        public async Task<(List<Location> Items, int TotalCount)> GetAllLocationsAsync(string searchString, bool? active, bool? archive, int page, int pageSize)
        {
            try
            {
                var query = _context.Locations
                    .Include(l => l.Address)
                    .AsQueryable();

                if (archive.HasValue)
                {
                    query = query.Where(l => l.Archive == archive.Value);
                }

                if (active.HasValue)
                {
                    query = query.Where(l => l.Status == active.Value);
                }

                if (!string.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    query = query.Where(l =>
                        (l.LocationName != null && l.LocationName.ToLower().Contains(searchString)) ||
                        (l.EmailId != null && l.EmailId.ToLower().Contains(searchString)) ||
                        (l.Fax != null && l.Fax.ToLower().Contains(searchString))
                    );
                }

                var totalCount = await query.CountAsync();

                var items = await query
                    .OrderByDescending(l => l.Created)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return (items, totalCount);
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                // _logger.LogError(ex, "Error occurred while retrieving locations.");

                throw new ApplicationException("An error occurred while retrieving the location list.", ex);
            }
        }

        public async Task<(List<object[]> Items, int TotalCount)> GetAllClinicianLocationsAsync( Guid clinicianId, string search, int page, int size)
        {
            try
            {

                // Create a query using LINQ instead of native SQL
                var query = from c in _context.Clinicians
                            join u in _context.Users on c.UserId equals u.Id
                            join cl in _context.ClinicianLocationMappings on c.Id equals cl.ClinicianId
                            join l in _context.Locations on cl.LocationId equals l.Id
                            where c.Uuid == clinicianId
                            select new object[] { l.Uuid.ToString(), l.LocationName };

                // Apply search filter if provided
                if (!string.IsNullOrEmpty(search))
                {
                    search = search.ToLower();
                    query = query.Where(item =>
                        ((string)item[1]).ToLower().Contains(search)
                    );
                }

                // Get total count for pagination
                var totalCount = await query.CountAsync();

                // Apply pagination
                var items = await query
                    .Skip(page * size)
                    .Take(size)
                    .ToListAsync();

                return (items, totalCount);
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                // _logger.LogError(ex, "Error occurred while retrieving locations.");

                throw new ApplicationException("An error occurred while retrieving the location list.", ex);
            }
        }

        public async Task UpdateLocationAsync(Location location)
        {
            try
            {
                _context.Locations.Update(location);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                // _logger.LogError(ex, "Error occurred while retrieving locations.");

                throw new ApplicationException("An error occurred while retrieving the location list.", ex);
            }
        }

    }
}
