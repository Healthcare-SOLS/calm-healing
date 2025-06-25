using Calm_Healing.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calm_Healing.Respository.IRepository
{
    public interface ILocationRepository
    {
        Task<Location> AddLocationAsync(Location location);
        Task<Location> FindByUuidAsync(Guid uuid);
        Task<(List<Location> Items, int TotalCount)> GetAllLocationsAsync(string searchString, bool? active, bool? archive, int page, int pageSize);
        Task<(List<object[]> Items, int TotalCount)> GetAllClinicianLocationsAsync(Guid clinicianId, string search, int page, int size);

        Task UpdateLocationAsync(Location location);

    }
}
