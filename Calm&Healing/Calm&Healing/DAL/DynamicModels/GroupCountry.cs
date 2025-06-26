//using Calm_Healing.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calm_Healing.DAL.DynamicModels
{
    [Table("country")]
    public class GroupCountry
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string ShortCode { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string CountryCode { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        // Navigation properties
        public virtual ICollection<GroupState> States { get; set; } = new List<GroupState>();
        public virtual ICollection<GroupAddress> Addresses { get; set; } = new List<GroupAddress>();
    }
}
