using System.ComponentModel.DataAnnotations;

namespace Calm_Healing.DAL.DTOs
{
    public class AdminRegisterDTO
    {
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        [Required]
        [StringLength(255)]
        public string Phone { get; set; }

        [Required]
        [StringLength(255)]
        public string UserTypeId { get; set; }

        [Required]
        [StringLength(255)]
        public string Active { get; set; }

        [Required]
        [StringLength(255)]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(255)]
        public string ModifiedBy { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }

}
