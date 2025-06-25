using Calm_Healing.DAL.DTOs;
using Calm_Healing.DAL.Models;
using Calm_Healing.Respository.IRepository;
using Calm_Healing.Service.IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Calm_Healing.DAL.DTOs.LocationDto;

namespace Calm_Healing.Service
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }
    

        public async Task AddLocationAsync(LocationDto locationDto)
        {
            try
            {
                // Convert DTO to Entity
                var location = new Location
                {
                    Uuid = Guid.NewGuid(),
                    LocationName = locationDto.LocationName,
                    ContactNumber = locationDto.ContactNumber,
                    EmailId = locationDto.EmailId,
                    GroupNpiNumber = locationDto.GroupNpiNumber,
                    Status = locationDto.Status,
                    Fax = locationDto.Fax,
                    Archive = false // Always set to false for new locations
                };

                // Handle Address if provided
                if (locationDto.Address != null)
                {
                    location.Address = new Address
                    {
                        Uuid = Guid.NewGuid(),
                        Line1 = locationDto.Address.Line1,
                        Line2 = locationDto.Address.Line2,
                        City = locationDto.Address.City,
                        State = locationDto.Address.State,
                        Zipcode = locationDto.Address.Zipcode
                    };
                }

                await _locationRepository.AddLocationAsync(location);
            }
            catch (Exception ex)
            {
                // Log the general exception
                // _logger.LogError(ex, "Error occurred while creating location");
                throw; // Rethrow to be handled by the controller
            }
        }

        public async Task<PagedResult<LocationResDto>> GetAllLocationsAsync(int page, int pageSize, string searchString, bool? active, bool? archive)
        {
            try
            {


                var (items, totalCount) = await _locationRepository.GetAllLocationsAsync(
                    searchString, active, archive, page, pageSize);

                var locationDtos = items.Cast<Location>().Select(location => new LocationResDto
                {
                    Uuid = location.Uuid,
                    LocationName = location.LocationName,
                    ContactNumber = location.ContactNumber,
                    EmailId = location.EmailId,
                    GroupNpiNumber = location.GroupNpiNumber,
                    Status = location.Status ?? true,
                    Fax = location.Fax,
                    Archive = location.Archive ?? false,
                    Address = location.Address != null ? new AddressResDto
                    {
                        Uuid = location.Address.Uuid,
                        Line1 = location.Address.Line1,
                        Line2 = location.Address.Line2,
                        City = location.Address.City,
                        State = location.Address.State,
                        Zipcode = location.Address.Zipcode
                    } : null
                }).ToList();

                return new PagedResult<LocationResDto>
                {
                    Items = locationDtos,
                    TotalCount = totalCount,
                    PageSize = pageSize,
                    CurrentPage = page
                };
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                // _logger.LogError(ex, "Error occurred while retrieving locations.");

                throw new ApplicationException("An error occurred while retrieving the location list.", ex);
            }
        }

        public async Task<PagedResult<Dictionary<string, string>>> GetAllClinicianLocationsAsync(Guid clinicianId, int page, int size, string search)
        {
            try
            {


                var (items, totalCount) = await _locationRepository.GetAllClinicianLocationsAsync(clinicianId, search, page, size);

                // Convert the list of object arrays to a dictionary
                var locationMap = new Dictionary<string, string>();

                foreach (var item in items)
                {
                    string uuid = item[0].ToString();
                    string name = item[1].ToString();

                    if (!locationMap.ContainsKey(uuid))
                    {
                        locationMap.Add(uuid, name);
                    }
                }

                return new PagedResult<Dictionary<string, string>>
                {
                    Items = new List<Dictionary<string, string>> { locationMap },
                    TotalCount = totalCount,
                    PageSize = size,
                    CurrentPage = page
                };
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                // _logger.LogError(ex, "Error occurred while retrieving locations.");

                throw new ApplicationException("An error occurred while retrieving the All Clinician Locations list.", ex);
            }
        }

        public async Task<LocationResDto> GetLocationByIdAsync(Guid locationId)
        {
            try
            {


                var location = await _locationRepository.FindByUuidAsync(locationId);

                if (location == null)
                {
                    throw new KeyNotFoundException($"Cannot find location with id: {locationId}");
                }

                return new LocationResDto
                {
                    Uuid = location.Uuid,
                    LocationName = location.LocationName,
                    ContactNumber = location.ContactNumber,
                    EmailId = location.EmailId,
                    GroupNpiNumber = location.GroupNpiNumber,
                    Status = location.Status ?? true,
                    Fax = location.Fax,
                    Archive = location.Archive ?? false,
                    Address = location.Address != null ? new AddressResDto
                    {
                        Uuid = location.Address.Uuid,
                        Line1 = location.Address.Line1,
                        Line2 = location.Address.Line2,
                        City = location.Address.City,
                        State = location.Address.State,
                        Zipcode = location.Address.Zipcode
                    } : null
                };
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                // _logger.LogError(ex, "Error occurred while retrieving locations.");

                throw new ApplicationException("An error occurred while retrieving the location by id.", ex);
            }
        }

        public async Task UpdateLocationAsync(LocationResDto locationDto)
        {
            /* if (locationDto.Uuid == null)
             {
                 throw new ArgumentException("Location UUID cannot be null");
             }*/
            try
            {
                var existingLocation = await _locationRepository.FindByUuidAsync(locationDto.Uuid.Value);

                if (existingLocation == null)
                {
                    throw new KeyNotFoundException($"Cannot find location with id: {locationDto.Uuid}");
                }
                // Set current time as Unspecified kind to avoid PostgreSQL 'timestamp without time zone' issue
                //var now = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);

                // Update location properties
                existingLocation.LocationName = locationDto.LocationName;
                existingLocation.ContactNumber = locationDto.ContactNumber;
                existingLocation.EmailId = locationDto.EmailId;
                existingLocation.GroupNpiNumber = locationDto.GroupNpiNumber;
                existingLocation.Status = locationDto.Status;
                existingLocation.Fax = locationDto.Fax;
                //existingLocation.Modified = now;

                // Update address if provided
                if (locationDto.Address != null)
                {
                    if (existingLocation.Address == null)
                    {
                        // Create new address if it doesn't exist
                        existingLocation.Address = new Address
                        {
                            Uuid = locationDto.Address.Uuid ?? Guid.NewGuid(),
                            Line1 = locationDto.Address.Line1,
                            Line2 = locationDto.Address.Line2,
                            City = locationDto.Address.City,
                            State = locationDto.Address.State,
                            Zipcode = locationDto.Address.Zipcode,
                            //  Created = now,
                            // Modified = now
                        };
                    }
                    else
                    {
                        // Update existing address
                        existingLocation.Address.Line1 = locationDto.Address.Line1;
                        existingLocation.Address.Line2 = locationDto.Address.Line2;
                        existingLocation.Address.City = locationDto.Address.City;
                        existingLocation.Address.State = locationDto.Address.State;
                        existingLocation.Address.Zipcode = locationDto.Address.Zipcode;
                        //existingLocation.Address.Modified = now;
                    }
                }

                await _locationRepository.UpdateLocationAsync(existingLocation);
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                // _logger.LogError(ex, "Error occurred while retrieving locations.");

                throw new ApplicationException("An error occurred while retrieving the updating location.", ex);
            }
        }

        public async Task ChangeLocationStatusAsync(Guid locationId, bool status)
        {
            try
            {

                var location = await _locationRepository.FindByUuidAsync(locationId);

                if (location == null)
                {
                    throw new KeyNotFoundException($"Cannot find location with id: {locationId}");
                }

                location.Status = status;
                //location.Modified = DateTime.UtcNow;

                await _locationRepository.UpdateLocationAsync(location);
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                // _logger.LogError(ex, "Error occurred while retrieving locations.");

                throw new ApplicationException("An error occurred while retrieving the location list.", ex);
            }
        }

        public async Task ArchiveRestoreLocationAsync(Guid locationId, bool flag)
        {
            try
            {
                var location = await _locationRepository.FindByUuidAsync(locationId);

                if (location == null)
                {
                    throw new KeyNotFoundException($"Cannot find location with id: {locationId}");
                }

                // Set archive status based on flag
                location.Archive = flag;

                // Important: When archiving, also disable the location
                // When restoring, also enable the location
                location.Status = !flag;

                location.Modified = DateTime.UtcNow;

                await _locationRepository.UpdateLocationAsync(location);
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
