//using Calm_Healing.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calm_Healing.DAL.DynamicModels
{
    [Table("provider")]
    public class GroupProvider
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Uuid { get; set; }

        public long? UserId { get; set; }

        [MaxLength(100)] public string Gender { get; set; }
        [MaxLength(100)] public string Fax { get; set; }
        [MaxLength(100)] public string Npi { get; set; }
        [MaxLength(100)] public string GroupNpiNumber { get; set; }

        public string ProviderType { get; set; }
        public long? LicensesId { get; set; }

        public string AcceptedInsurances { get; set; }

        [MaxLength(100)] public string ExperienceYear { get; set; }
        [MaxLength(1000)] public string Bio { get; set; }
        [MaxLength(1000)] public string Expertise { get; set; }
        [MaxLength(1000)] public string Experience { get; set; }

        public string InsuranceVerification { get; set; }
        public string PriorAuthorizations { get; set; }
        public string Language { get; set; }

        public long? AddressId { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [ForeignKey(nameof(UserId))] public virtual GroupUser User { get; set; }
        [ForeignKey(nameof(AddressId))] public virtual GroupAddress Address { get; set; }
        [ForeignKey(nameof(CreatedBy))] public virtual GroupUser Creator { get; set; }
        [ForeignKey(nameof(ModifiedBy))] public virtual GroupUser Modifier { get; set; }
    }

}
