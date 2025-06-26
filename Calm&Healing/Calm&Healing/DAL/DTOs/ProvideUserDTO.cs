using Calm_Healing.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Calm_Healing.DAL.DTOs
{
    public class ProvideUserDTO
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Last name is required.")]
        public string? LastName { get; set; }
        public string? RoleName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Npi is required.")]
        public string? NpiNumber { get; set; }
        public long? LocationId { get; set; }
        public string? LocationName { get; set; }
        public bool? Status { get; set; }
        public string? LanguagesSpoken { get; set; }

    }
}
