using Calm_Healing.DAL.DTOs;
using Calm_Healing.DAL.Models;

namespace Calm_Healing.Respository.IRepository
{
    public interface IProvideUserRepository
    {
        Task<IEnumerable<(User user, Clinician clinician, Role? role)>> GetAllProvideUsersAsync(int pageNumber, int pageSize);
        Task AddProvideUserAsync(User user, Clinician clinician, long roleId);
        public Task<bool> UpdateProvideUserAsync(ProvideUserDTO dto);

    }
}
