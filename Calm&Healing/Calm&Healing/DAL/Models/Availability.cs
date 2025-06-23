using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class Availability
{
    public long Id { get; set; }

    public Guid Uuid { get; set; }

    public string TimeZone { get; set; } = null!;

    public long ClinicianId { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual ICollection<BlockDay> BlockDays { get; set; } = new List<BlockDay>();

    public virtual Clinician Clinician { get; set; } = null!;

    public virtual ICollection<DayWiseSlot> DayWiseSlots { get; set; } = new List<DayWiseSlot>();
}
