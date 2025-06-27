using Calm_Healing.DAL.DTOs;
using Calm_Healing.DAL.Models;

namespace Calm_Healing.Respository.IRepository
{
    public interface IProvideUserRepository
    {
        Task<IEnumerable<(User user, Clinician clinician, Role? role)>> GetAllProvideUsersAsync(int pageNumber, int pageSize);
        Task<(User user, Clinician clinician, Role? role)> GetProvideUserByIdAsync(long id);
        Task AddProvideUserAsync(User user, Clinician clinician, long roleId);
        public Task<bool> UpdateProvideUserAsync(ProvideUserCreateUpdateDTO dto);
    }
}
