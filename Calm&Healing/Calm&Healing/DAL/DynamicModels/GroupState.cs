using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calm_Healing.DAL.DynamicModels
{
    [Table("state")]
    public class GroupState
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [ForeignKey("Country")]
        public long CountryId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        // Navigation to Country
        public virtual GroupCountry Country { get; set; }

        // Reverse navigation to Addresses
        public virtual ICollection<GroupAddress> Addresses { get; set; } = new List<GroupAddress>();
    }
}
