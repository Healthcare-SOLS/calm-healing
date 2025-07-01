using System.ComponentModel.DataAnnotations;

namespace Calm_Healing.DAL.DTOs
{
    public class ContactDto
    {
        //public Guid? Uuid { get; set; }

        [Required]
        public ContactType ContactType { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [RegularExpression(@"^$|^\+\d{1,3}\(\d{3}\)-\d{3}-\d{4}$|^\(?\d{3}\)?[- ]?\d{3}[- ]?\d{4}$",
            ErrorMessage = "Invalid phone number format. Valid formats: (XXX)-XXX-XXXX or +<XXX>(XXX)-XXX-XXXX")]
        public string? ContactNumber { get; set; }

        public string? FaxNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? EmailId { get; set; }
        public bool Status { get; set; } = true;
        public AddressDto? Address { get; set; }

        //public bool Archive { get; set; }

       
    }
    public class ContactResDto
    {
        public Guid? Uuid { get; set; }

        [Required]
        public ContactType ContactType { get; set; }

        public string? Name { get; set; }
        [RegularExpression(@"^$|^\+\d{1,3}\(\d{3}\)-\d{3}-\d{4}$|^\(?\d{3}\)?[- ]?\d{3}[- ]?\d{4}$",
            ErrorMessage = "Invalid phone number format. Valid formats: (XXX)-XXX-XXXX or +<XXX>(XXX)-XXX-XXXX")]
        public string? ContactNumber { get; set; }

        public string? FaxNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? EmailId { get; set; }
        public bool Status { get; set; } = true;

        public AddressResDto? Address { get; set; }

        public bool? Archive { get; set; } = false;
    }

    public enum ContactType
    {
        REFERRAL,
        LAB,
        PHARMACY
    }
}
