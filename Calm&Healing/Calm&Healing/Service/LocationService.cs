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
        private readonly ILogger<LocationService> _logger;

        public LocationService(ILocationRepository locationRepository, ILogger<LocationService> logger)
        {
            _logger = logger;
            _locationRepository = locationRepository;
        }
    

        public async Task AddLocationAsync(LocationDto locationDto)
        {
            try
            {
                _logger.LogInformation("AddLocationAsync called at {Time}", DateTime.UtcNow);
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
                //_logger.LogDebug("Mapping basic location info: {@Location}", location);
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
                    //_logger.LogDebug("Mapping address info: {@Address}", location.Address);
                }
                _logger.LogInformation("Location successfully saved with UUID: {Uuid}", location.Uuid);
                await _locationRepository.AddLocationAsync(location);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while creating a new location");
                throw; // Rethrow to be handled by the controller
            }
        }

        public async Task<PagedResult<LocationResDto>> GetAllLocationsAsync(int page, int pageSize, string searchString, bool? active, bool? archive)
        {
            try
            {
                _logger.LogInformation("GetAllLocationsAsync called at {Time} with parameters: page={Page}, pageSize={PageSize}, search={Search}, active={Active}, archive={Archive}", 
                    DateTime.UtcNow, page, pageSize, searchString, active, archive);

                var (items, totalCount) = await _locationRepository.GetAllLocationsAsync(
                    searchString, active, archive, page, pageSize);

                _logger.LogDebug("Retrieved {Count} locations from repository", items.Count);

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

                var result = new PagedResult<LocationResDto>
                {
                    Items = locationDtos,
                    TotalCount = totalCount,
                    PageSize = pageSize,
                    CurrentPage = page
                };

                _logger.LogInformation("Successfully mapped {Count} locations to DTOs out of {Total} total records", 
                    result.Items.Count, result.TotalCount);
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving locations with parameters: page={Page}, pageSize={PageSize}, search={Search}", 
                    page, pageSize, searchString);
                throw new ApplicationException("An error occurred while retrieving the location list.", ex);
            }
        }

        public async Task<PagedResult<Dictionary<string, string>>> GetAllClinicianLocationsAsync(Guid clinicianId, int page, int size, string search)
        {
            try
            {
                _logger.LogInformation("GetAllClinicianLocationsAsync called at {Time} for clinician ID: {ClinicianId}, page={Page}, size={Size}, search={Search}", 
                    DateTime.UtcNow, clinicianId, page, size, search);

                var (items, totalCount) = await _locationRepository.GetAllClinicianLocationsAsync(clinicianId, search, page, size);
                _logger.LogDebug("Retrieved {Count} clinician locations from repository", items.Count);

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

                var result = new PagedResult<Dictionary<string, string>>
                {
                    Items = new List<Dictionary<string, string>> { locationMap },
                    TotalCount = totalCount,
                    PageSize = size,
                    CurrentPage = page
                };

                _logger.LogInformation("Successfully mapped {Count} clinician locations to dictionary for clinician ID: {ClinicianId}", 
                    locationMap.Count, clinicianId);
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving clinician locations for clinician ID: {ClinicianId}", clinicianId);
                throw new ApplicationException("An error occurred while retrieving the All Clinician Locations list.", ex);
            }
        }

        public async Task<LocationResDto> GetLocationByIdAsync(Guid locationId)
        {
            try
            {
                _logger.LogInformation("GetLocationByIdAsync called at {Time} for location ID: {LocationId}", 
                    DateTime.UtcNow, locationId);

                var location = await _locationRepository.FindByUuidAsync(locationId);

                if (location == null)
                {
                    _logger.LogWarning("Location not found with ID: {LocationId}", locationId);
                    throw new KeyNotFoundException($"Cannot find location with id: {locationId}");
                }

                _logger.LogDebug("Location found with name: {LocationName}", location.LocationName);

                var locationDto = new LocationResDto
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

                _logger.LogInformation("Successfully retrieved and mapped location with ID: {LocationId}", locationId);
                return locationDto;
            }
            catch (KeyNotFoundException ex)
            {
                // Let this exception propagate as it's already logged
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving location by ID: {LocationId}", locationId);
                throw new ApplicationException("An error occurred while retrieving the location by id.", ex);
            }
        }

        public async Task UpdateLocationAsync(LocationResDto locationDto)
        {
            try
            {
                _logger.LogInformation("UpdateLocationAsync called at {Time} for location ID: {LocationId}", 
                    DateTime.UtcNow, locationDto.Uuid);

                var existingLocation = await _locationRepository.FindByUuidAsync(locationDto.Uuid.Value);

                if (existingLocation == null)
                {
                    _logger.LogWarning("Location not found with ID: {LocationId}", locationDto.Uuid);
                    throw new KeyNotFoundException($"Cannot find location with id: {locationDto.Uuid}");
                }

                _logger.LogDebug("Found existing location: {LocationName}", existingLocation.LocationName);

                // Update location properties
                existingLocation.LocationName = locationDto.LocationName;
                existingLocation.ContactNumber = locationDto.ContactNumber;
                existingLocation.EmailId = locationDto.EmailId;
                existingLocation.GroupNpiNumber = locationDto.GroupNpiNumber;
                existingLocation.Status = locationDto.Status;
                existingLocation.Fax = locationDto.Fax;

                _logger.LogDebug("Updated basic location properties for ID: {LocationId}", locationDto.Uuid);

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
                        };
                        _logger.LogDebug("Created new address for location ID: {LocationId}", locationDto.Uuid);
                    }
                    else
                    {
                        // Update existing address
                        existingLocation.Address.Line1 = locationDto.Address.Line1;
                        existingLocation.Address.Line2 = locationDto.Address.Line2;
                        existingLocation.Address.City = locationDto.Address.City;
                        existingLocation.Address.State = locationDto.Address.State;
                        existingLocation.Address.Zipcode = locationDto.Address.Zipcode;
                        _logger.LogDebug("Updated existing address for location ID: {LocationId}", locationDto.Uuid);
                    }
                }

                await _locationRepository.UpdateLocationAsync(existingLocation);
                _logger.LogInformation("Location successfully updated with ID: {LocationId}", locationDto.Uuid);
            }
            catch (KeyNotFoundException ex)
            {
                // Let this exception propagate as it's already logged
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating location with ID: {LocationId}", locationDto.Uuid);
                throw new ApplicationException("An error occurred while updating location.", ex);
            }
        }

        public async Task ChangeLocationStatusAsync(Guid locationId, bool status)
        {
            try
            {
                _logger.LogInformation("ChangeLocationStatusAsync called at {Time} for location ID: {LocationId}, new status: {Status}", 
                    DateTime.UtcNow, locationId, status);

                var location = await _locationRepository.FindByUuidAsync(locationId);

                if (location == null)
                {
                    _logger.LogWarning("Location not found with ID: {LocationId}", locationId);
                    throw new KeyNotFoundException($"Cannot find location with id: {locationId}");
                }

                _logger.LogDebug("Found location: {LocationName}, current status: {CurrentStatus}", 
                    location.LocationName, location.Status);

                location.Status = status;
                //location.Modified = DateTime.UtcNow;

                await _locationRepository.UpdateLocationAsync(location);
                _logger.LogInformation("Location status successfully changed to {Status} for location ID: {LocationId}", 
                    status, locationId);
            }
            catch (KeyNotFoundException ex)
            {
                // Let this exception propagate as it's already logged
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while changing status for location ID: {LocationId}", locationId);
                throw new ApplicationException("An error occurred while changing location status.", ex);
            }
        }

        public async Task ArchiveRestoreLocationAsync(Guid locationId, bool flag)
        {
            try
            {
                _logger.LogInformation("ArchiveRestoreLocationAsync called at {Time} for location ID: {LocationId}, archive flag: {Flag}", 
                    DateTime.UtcNow, locationId, flag);

                var location = await _locationRepository.FindByUuidAsync(locationId);

                if (location == null)
                {
                    _logger.LogWarning("Location not found with ID: {LocationId}", locationId);
                    throw new KeyNotFoundException($"Cannot find location with id: {locationId}");
                }

                _logger.LogDebug("Found location: {LocationName}, current archive status: {CurrentArchive}", 
                    location.LocationName, location.Archive);

                // Set archive status based on flag
                location.Archive = flag;

                // Important: When archiving, also disable the location
                // When restoring, also enable the location
                location.Status = !flag;

                location.Modified = DateTime.UtcNow;

                await _locationRepository.UpdateLocationAsync(location);
                _logger.LogInformation("Location {Action} successfully for location ID: {LocationId}", 
                    flag ? "archived" : "restored", locationId);
            }
            catch (KeyNotFoundException ex)
            {
                // Let this exception propagate as it's already logged
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while {Action} location with ID: {LocationId}", 
                    flag ? "archiving" : "restoring", locationId);
                throw new ApplicationException($"An error occurred while {(flag ? "archiving" : "restoring")} location.", ex);
            }
        }
    }
}
