//using Calm_Healing.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calm_Healing.DAL.DynamicModels
{
    [Table("patient")]
    public class GroupPatient
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public Guid Uuid { get; set; }

        public long UserId { get; set; }

        public DateTime? BirthDate { get; set; }
        [MaxLength(100)] public string Gender { get; set; }
        [MaxLength(100)] public string EmergencyContactFirstName { get; set; }
        [MaxLength(100)] public string EmergencyContactLastName { get; set; }
        [MaxLength(100)] public string EmergencyContactNumber { get; set; }
        [MaxLength(100)] public string EmergencyAddressId { get; set; }

        public string AlternativePhoneNumber { get; set; }
        public string Allergies { get; set; }
        public bool? ReferredByProvider { get; set; }
        public string ReferredProviderName { get; set; }
        public string ReferredProviderNpi { get; set; }

        public bool MessageConsent { get; set; } = true;
        public bool CallConsent { get; set; } = true;
        public bool EmailConsent { get; set; } = true;

        public long? AddressId { get; set; }
        public long? LastProvider { get; set; }

        [MaxLength(255)] public string PreferredPharmacy { get; set; }
        [MaxLength(255)] public string PreferredLabs { get; set; }
        [MaxLength(1000)] public string Note { get; set; }

        public string Source { get; set; }
        public DateTime? LastVisit { get; set; }

        public long? InsurancesId { get; set; }
        public string InsuranceCashPay { get; set; }

        public bool IntakeFormCompleted { get; set; } = false;
        public bool MentalScreeningCompleted { get; set; } = false;

        [Column(TypeName = "decimal(15,2)")]
        public decimal Balance { get; set; } = 0;

        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public long? AccessibleBy { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Navigation
        [ForeignKey(nameof(UserId))] public virtual GroupUser User { get; set; }
        [ForeignKey(nameof(AddressId))] public virtual GroupAddress Address { get; set; }
        [ForeignKey(nameof(CreatedBy))] public virtual GroupUser Creator { get; set; }
        [ForeignKey(nameof(ModifiedBy))] public virtual GroupUser Modifier { get; set; }
        [ForeignKey(nameof(LastProvider))] public virtual GroupProvider LastProviderReference { get; set; }
        [ForeignKey(nameof(AccessibleBy))] public virtual GroupUser AccessibleUser { get; set; }
    }


}
