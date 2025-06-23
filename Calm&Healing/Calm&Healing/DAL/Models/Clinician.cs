using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class Clinician
{
    public long Id { get; set; }

    public Guid Uuid { get; set; }

    public string? ContactNumber { get; set; }

    public string? NpiNumber { get; set; }

    public bool? Status { get; set; }

    public bool? Archive { get; set; }

    public string? LanguagesSpoken { get; set; }

    public Guid? SupervisorClinicianUuid { get; set; }

    public long? UserId { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Modified { get; set; }

    public string? Signature { get; set; }

    public bool? TwoFactorAuthentication { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Availability> Availabilities { get; set; } = new List<Availability>();

    public virtual ICollection<Client> ClientPrimaryClinicians { get; set; } = new List<Client>();

    public virtual ICollection<Client> ClientReferringClinicians { get; set; } = new List<Client>();

    public virtual ICollection<ClinicianLocationMapping> ClinicianLocationMappings { get; set; } = new List<ClinicianLocationMapping>();

    public virtual ICollection<GroupSetting> GroupSettings { get; set; } = new List<GroupSetting>();

    public virtual User? User { get; set; }
}
