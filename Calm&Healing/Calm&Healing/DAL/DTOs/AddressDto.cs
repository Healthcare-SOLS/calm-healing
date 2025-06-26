namespace Calm_Healing.DAL.DTOs
{
    public class AddressDto
    {
        //public Guid? Uuid { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zipcode { get; set; }

    }
    public class AddressResDto
    {
        public Guid? Uuid { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zipcode { get; set; }

    }
}
