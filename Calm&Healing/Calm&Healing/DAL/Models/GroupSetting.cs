using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class GroupSetting
{
    public long Id { get; set; }

    public long ClinicianId { get; set; }

    public string? GroupName { get; set; }

    public string? GroupInitials { get; set; }

    public string? CptCode { get; set; }

    public bool? FamilyGroup { get; set; }

    public bool? Archive { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }

    public Guid? Uuid { get; set; }

    public Guid? BillTo { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<ClientGroupSettingsMapping> ClientGroupSettingsMappings { get; set; } = new List<ClientGroupSettingsMapping>();

    public virtual Clinician Clinician { get; set; } = null!;
}
