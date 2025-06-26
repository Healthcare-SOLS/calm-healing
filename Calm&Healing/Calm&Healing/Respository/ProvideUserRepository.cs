using Calm_Healing.DAL.DTOs;
using Calm_Healing.DAL.Models;
using Calm_Healing.Respository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Calm_Healing.Respository
{
    public class ProvideUserRepository : IProvideUserRepository
    {
        private readonly TenantDbContext _context;

        public ProvideUserRepository(TenantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<(User user, Clinician clinician, Role? role)>> GetAllProvideUsersAsync(int pageNumber, int pageSize)
        {
            var result = await _context.Clinicians
                .Where(c => (!c.Archive.HasValue || c.Archive == false) && c.User != null)
                .Include(c => c.User)
                    .ThenInclude(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                .Include(c => c.ClinicianLocationMappings)
                    .ThenInclude(clm => clm.Location) // ✅ Include Location data
                .OrderByDescending(c => c.Created)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return result.Select(c => (
                user: c.User!,
                clinician: c,
                role: c.User!.UserRoles.FirstOrDefault()?.Role
            ));
        }


        public async Task AddProvideUserAsync(User user, Clinician clinician, long roleId)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Attach role to user
            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = roleId
            };
            _context.UserRoles.Add(userRole);

            // Attach clinician to user
            clinician.UserId = user.Id;
            _context.Clinicians.Add(clinician);

            await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateProvideUserAsync(ProvideUserDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == dto.Id);
            var clinician = await _context.Clinicians.FirstOrDefaultAsync(c => c.UserId == dto.Id);

            if (user == null || clinician == null)
                return false;

            // Update User fields
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.Modified = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);

            // Update Clinician fields
            clinician.NpiNumber = dto.NpiNumber;
            clinician.LanguagesSpoken = dto.LanguagesSpoken;
            clinician.Modified = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);

            _context.Users.Update(user);
            _context.Clinicians.Update(clinician);

            await _context.SaveChangesAsync();
            return true;
        }

    }

}
