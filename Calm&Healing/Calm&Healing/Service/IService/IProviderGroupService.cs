using Calm_Healing.DAL.DTOs;

namespace Calm_Healing.Service.IService
{
    public interface IProviderGroupService
    {
        Task<PracticeDetailsDto?> GetPracticeDetailsAsync();
        Task<bool> UpdatePracticeAsync(PracticeUpdateDto dto);

    }
}
