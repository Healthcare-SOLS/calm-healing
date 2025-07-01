using Calm_Healing.DAL.Models;
using Calm_Healing.Respository.IRepository;
using System;
using System.Threading.Tasks;
namespace Calm_Healing.Respository.IRepository
{
        public interface IContactRepository
        {
            Task<Contact> AddContactAsync(Contact contact);
            Task<Contact> FindByUuidAsync(Guid uuid);
           Task UpdateContactAsync(Contact contact);
           Task<(List<Contact> Items, int TotalCount)> GetAllContactsAsync(string? searchString, bool? active, bool? archive, int page, int pageSize);
    }
    
}
