namespace Calm_Healing.DAL.DTOs
{
        public class PracticeUpdateDto
        {
            public long Id { get; set; }

            // Practice Info
            public string? ClinicName { get; set; }
            public string? NpiNumber { get; set; }
            public string? TaxType { get; set; }
            public string? TaxNumber { get; set; }
            public string? ContactNumber { get; set; }
            public string? EmailId { get; set; }
            public string? Taxonomy { get; set; }

            // Address Info
            public string? Line1 { get; set; }
            public string? Line2 { get; set; }
            public string? City { get; set; }
            public string? State { get; set; }
            public string? Zipcode { get; set; }
        }
}
