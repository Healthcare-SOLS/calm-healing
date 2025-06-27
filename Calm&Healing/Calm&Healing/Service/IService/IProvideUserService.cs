using Calm_Healing.DAL.DTOs;

namespace Calm_Healing.Service.IService
{
    public interface IProvideUserService
    {
        Task<IEnumerable<ProvideUserDTO>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<ProvideUserDTO?> GetByIdAsync(long id);

        Task AddAsync(ProvideUserCreateUpdateDTO dto);
        Task<bool> UpdateAsync(ProvideUserCreateUpdateDTO dto);

    }
}
