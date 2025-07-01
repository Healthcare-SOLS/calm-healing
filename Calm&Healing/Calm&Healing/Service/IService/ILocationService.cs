using Calm_Healing.DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Calm_Healing.DAL.DTOs.LocationDto;

namespace Calm_Healing.Service.IService
{
    public interface ILocationService
    {
        Task AddLocationAsync(LocationDto location);
        Task<PagedResult<LocationResDto>> GetAllLocationsAsync(int page, int pageSize, string searchString, bool? active, bool? archive);

        Task<PagedResult<Dictionary<string, string>>> GetAllClinicianLocationsAsync(Guid clinicianId, int page, int size, string search);
        Task<LocationResDto> GetLocationByIdAsync(Guid locationId);
        Task UpdateLocationAsync(LocationResDto location);
        Task ChangeLocationStatusAsync(Guid locationId, bool status);
        Task ArchiveRestoreLocationAsync(Guid locationId, bool flag);
    }

    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    }
}
