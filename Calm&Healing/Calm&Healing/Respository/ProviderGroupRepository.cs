using Calm_Healing.DAL.DTOs;
using Calm_Healing.DAL.Models;
using Calm_Healing.Respository.IRepository;
using Calm_Healing.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace Calm_Healing.Respository
{
    public class ProviderGroupRepository : IProviderGroupRepository
    {
        private readonly TenantDbContext _context;

        public ProviderGroupRepository(TenantDbContext context)
        {
            _context = context;
        }
        public async Task<PracticeDetailsDto?> GetPracticeDetailsAsync()
        {
            var practice = await _context.Practices
                .Include(p => p.Address)
                .FirstOrDefaultAsync(p => p.Archive != true);

            if (practice == null) return null;

            var address = practice.Address != null
                ? $"{practice.Address.Line1}, {practice.Address.Line2}, {practice.Address.City}, {practice.Address.State}, {practice.Address.Zipcode}"
                : null;

            return new PracticeDetailsDto
            {
                ClinicName = practice.ClinicName,
                EmailId = practice.EmailId,
                TaxNumber = practice.TaxNumber,
                Taxonomy = practice.Taxonomy,
                ContactNumber = practice.ContactNumber,
                Address = address
            };
        }
        public async Task<bool> UpdatePracticeAsync(PracticeUpdateDto dto)
        {
            var practice = await _context.Practices
                .Include(p => p.Address)
                .FirstOrDefaultAsync(p => p.Id == dto.Id && p.Archive != true);

            if (practice == null) return false;

            // Update practice fields
            practice.ClinicName = dto.ClinicName;
            practice.NpiNumber = dto.NpiNumber;
            practice.TaxType = dto.TaxType;
            practice.TaxNumber = dto.TaxNumber;
            practice.ContactNumber = dto.ContactNumber;
            practice.EmailId = dto.EmailId;
            practice.Taxonomy = dto.Taxonomy;
            practice.Modified = DateTime.UtcNow;

            // Update address if exists
            if (practice.Address != null)
            {
                practice.Address.Line1 = dto.Line1;
                practice.Address.Line2 = dto.Line2;
                practice.Address.City = dto.City;
                practice.Address.State = dto.State;
                practice.Address.Zipcode = dto.Zipcode;
                practice.Address.Modified = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
