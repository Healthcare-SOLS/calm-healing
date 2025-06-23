using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class Appointment
{
    public long Id { get; set; }

    public Guid? Uuid { get; set; }

    public string? SessionType { get; set; }

    public string? AppointmentMode { get; set; }

    public string? AppointmentType { get; set; }

    public string? Status { get; set; }

    public string? Timezone { get; set; }

    public long? ClientId { get; set; }

    public long? ClinicianId { get; set; }

    public int? Duration { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public long? LocationId { get; set; }

    public string? PlaceOfService { get; set; }

    public double? EstimatedAmount { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Modified { get; set; }

    public long? GroupSettingsId { get; set; }

    public Guid? BaseAppointmentId { get; set; }

    public int? RepeatEvery { get; set; }

    public string? RecurrenceType { get; set; }

    public int? NumberOfAppointments { get; set; }

    public string? RepeatOnDays { get; set; }

    public int? MonthDay { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<AppointmentCptCode> AppointmentCptCodes { get; set; } = new List<AppointmentCptCode>();

    public virtual Client? Client { get; set; }

    public virtual Clinician? Clinician { get; set; }

    public virtual GroupSetting? GroupSettings { get; set; }

    public virtual Location? Location { get; set; }
}
