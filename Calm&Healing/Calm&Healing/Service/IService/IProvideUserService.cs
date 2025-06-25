using Calm_Healing.DAL.DTOs;

namespace Calm_Healing.Service.IService
{
    public interface IProvideUserService
    {
        Task<IEnumerable<ProvideUserDTO>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task AddAsync(ProvideUserDTO dto, long roleId);
        Task<bool> UpdateAsync(ProvideUserDTO dto);

    }
}
