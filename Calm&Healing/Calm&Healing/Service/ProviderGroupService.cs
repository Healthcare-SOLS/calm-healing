using Calm_Healing.DAL.DTOs;
using Calm_Healing.Respository.IRepository;
using Calm_Healing.Service.IService;

namespace Calm_Healing.Service
{
    public class ProviderGroupService: IProviderGroupService
    {
        private readonly IProviderGroupRepository _repository;

        public ProviderGroupService(IProviderGroupRepository repository)
        {
            _repository = repository;
        }
        public async Task<PracticeDetailsDto?> GetPracticeDetailsAsync()
        {
            return await _repository.GetPracticeDetailsAsync();
        }
        public async Task<bool> UpdatePracticeAsync(PracticeUpdateDto dto)
        {
            return await _repository.UpdatePracticeAsync(dto);
        }

    }
}
