using System.ComponentModel.DataAnnotations;

namespace Calm_Healing.DAL.DTOs
{
    public class ProviderGroupRegisterDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public string IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public string Email { get; set; }
    }

}
