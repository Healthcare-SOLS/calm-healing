using System.ComponentModel.DataAnnotations;

namespace Calm_Healing.DAL.DTOs
{
    public class AddressDto
    {
        //public Guid? Uuid { get; set; }
        [Required(ErrorMessage = "Address Line 1 is required.")]
        public string Line1 { get; set; }

        public string Line2 { get; set; }
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }
        [Required(ErrorMessage = "Zipcode is required.")]
        public string Zipcode { get; set; }

    }
    public class AddressResDto
    {
        public Guid? Uuid { get; set; }

        [Required(ErrorMessage = "Address Line 1 is required.")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }
        [Required(ErrorMessage = "Zipcode is required.")]
        public string Zipcode { get; set; }

    }
}
