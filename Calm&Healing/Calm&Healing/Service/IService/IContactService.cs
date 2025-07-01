using Calm_Healing.DAL.DTOs;
using System;
using System.Threading.Tasks;

namespace Calm_Healing.Service.IService
{
    
        public interface IContactService
        {
            Task AddContactAsync(ContactDto contactDto);
            Task UpdateContactAsync(ContactResDto contactDto);
            Task<ContactResDto> GetContactByIdAsync(Guid contactId);
            Task<PagedResult<ContactResDto>> GetAllContactsAsync(int page, int pageSize, string? searchString, bool? active, bool? archive);
            Task ChangeContactStatusAsync(Guid contactId, bool status);

            Task ArchiveRestoreContactAsync(Guid contactId, bool flag);
    }
    
}
