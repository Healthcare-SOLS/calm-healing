using Calm_Healing.DAL.DTOs;

namespace Calm_Healing.Respository.IRepository
{
    public interface IProviderGroupRepository
    {
        Task<PracticeDetailsDto?> GetPracticeDetailsAsync();  // ✅ No method body here
        Task<bool> UpdatePracticeAsync(PracticeUpdateDto dto);
    }
}
