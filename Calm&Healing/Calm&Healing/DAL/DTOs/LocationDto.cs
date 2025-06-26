using System.ComponentModel.DataAnnotations;

namespace Calm_Healing.DAL.DTOs
{
    public class LocationDto
    {
        //public Guid? Uuid { get; set; }

        [Required(ErrorMessage = "Location name is required")]
        public string LocationName { get; set; }

        [RegularExpression(@"^$|^\+\d{1,3}\(\d{3}\)-\d{3}-\d{4}$|^\(?\d{3}\)?[- ]?\d{3}[- ]?\d{4}$",
            ErrorMessage = "Invalid phone number format. Valid formats: (XXX)-XXX-XXXX or +<XXX>(XXX)-XXX-XXXX")]
        public string ContactNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string EmailId { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "NPI number must be exactly 10 digits")]
        public string GroupNpiNumber { get; set; }

        public bool Status { get; set; } = true;

        public string Fax { get; set; }

        public AddressDto Address { get; set; }

        public bool? Archive { get; set; } = false;

        public class LocationResDto
        {
            public Guid? Uuid { get; set; }

            [Required(ErrorMessage = "Location name is required")]
            public string LocationName { get; set; }

            [RegularExpression(@"^$|^\+\d{1,3}\(\d{3}\)-\d{3}-\d{4}$|^\(?\d{3}\)?[- ]?\d{3}[- ]?\d{4}$",
                ErrorMessage = "Invalid phone number format. Valid formats: (XXX)-XXX-XXXX or +<XXX>(XXX)-XXX-XXXX")]
            public string ContactNumber { get; set; }

            [EmailAddress(ErrorMessage = "Invalid email format")]
            public string EmailId { get; set; }

            [RegularExpression(@"^\d{10}$", ErrorMessage = "NPI number must be exactly 10 digits")]
            public string GroupNpiNumber { get; set; }

            public bool Status { get; set; } = true;

            public string Fax { get; set; }

            public AddressResDto Address { get; set; }

            public bool? Archive { get; set; } = false;
        }
    }
}
