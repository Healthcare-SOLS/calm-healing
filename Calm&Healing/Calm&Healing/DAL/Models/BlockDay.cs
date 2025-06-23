using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class BlockDay
{
    public long Id { get; set; }

    public Guid Uuid { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public long? AvailabilityId { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }

    public virtual Availability? Availability { get; set; }
}
