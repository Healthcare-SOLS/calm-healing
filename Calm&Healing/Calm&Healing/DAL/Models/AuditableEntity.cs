namespace Calm_Healing.DAL.Models
{
    public abstract class AuditableEntity
    {
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Modified { get; set; } = DateTime.UtcNow;
    }
}
