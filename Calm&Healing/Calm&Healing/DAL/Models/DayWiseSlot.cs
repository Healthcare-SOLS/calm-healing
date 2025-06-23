using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class DayWiseSlot
{
    public long Id { get; set; }

    public Guid Uuid { get; set; }

    public string DayOfWeek { get; set; } = null!;

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public long? AvailabilityId { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }

    public virtual Availability? Availability { get; set; }
}
