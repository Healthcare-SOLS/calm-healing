using Calm_Healing.DAL.DTOs;
using Calm_Healing.DAL.Models;
using Calm_Healing.Respository.IRepository;
using Calm_Healing.Service.IService;
using Calm_Healing.Utilities.IUtilities;
using Microsoft.EntityFrameworkCore;

namespace Calm_Healing.Service
{
    public class ProvideUserService : IProvideUserService
    {
        private readonly IProvideUserRepository _repository;
        private readonly TenantDbContext _context;
        private readonly IAESHelper _aesHelper;

        public ProvideUserService(IProvideUserRepository repository, TenantDbContext context, IAESHelper aesHelper)
        {
            _repository = repository;
            _context = context;
            _aesHelper = aesHelper;
        }

        public async Task<IEnumerable<ProvideUserDTO>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            var results = await _repository.GetAllProvideUsersAsync(pageNumber, pageSize);

            return results.Select(r => new ProvideUserDTO
            {
                Id = r.user.Id,
                FirstName = _aesHelper.Decrypt(r.user.FirstName),
                LastName = _aesHelper.Decrypt(r.user.LastName),
                Email = _aesHelper.Decrypt(r.user.Email),
                PhoneNumber = r.user.PhoneNumber,
                NpiNumber = r.clinician.NpiNumber,
                LanguagesSpoken = r.clinician.LanguagesSpoken,
                Status = r.clinician.Status,
                RoleName = r.role?.RoleName,
                LocationId = r.clinician.ClinicianLocationMappings.FirstOrDefault()?.LocationId,
                LocationName = r.clinician.ClinicianLocationMappings.FirstOrDefault()?.Location?.LocationName
            });
        }


        public async Task AddAsync(ProvideUserDTO dto, long roleId)
        {
            var user = new User
            {
                Id = dto.Id,
                FirstName = _aesHelper.Encrypt(dto.FirstName),
                LastName = _aesHelper.Encrypt(dto.LastName),
                Email = _aesHelper.Encrypt(dto.Email),
                PhoneNumber = _aesHelper.Encrypt(dto.PhoneNumber),
                IamId = Guid.NewGuid().ToString(),
                Uuid = Guid.NewGuid(),
                Active = true,
                Archive = false,
                Created = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                Modified = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                EmailVerified = false,
                PhoneVerified = false
            };

            var clinician = new Clinician
            {
                Uuid = Guid.NewGuid(),
                NpiNumber = _aesHelper.Encrypt(dto.NpiNumber),
                LanguagesSpoken = dto.LanguagesSpoken,
                Created = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                Modified = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                Status = true,
                Archive = false
            };

            // Save user + clinician first so we get clinician.Id
            await _repository.AddProvideUserAsync(user, clinician, roleId);

            // Save location mapping if LocationId is provided
            if (dto.LocationId.HasValue)
            {
                var mapping = new ClinicianLocationMapping
                {
                    ClinicianId = clinician.Id,
                    LocationId = dto.LocationId.Value
                };
                _context.ClinicianLocationMappings.Add(mapping);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> UpdateAsync(ProvideUserDTO dto)
        {
            return await _repository.UpdateProvideUserAsync(dto);
        }

    }
}
