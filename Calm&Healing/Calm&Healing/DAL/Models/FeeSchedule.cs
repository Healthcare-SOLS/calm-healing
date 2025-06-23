using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class FeeSchedule
{
    public long Id { get; set; }

    public Guid? Uuid { get; set; }

    public string? ProcedureCode { get; set; }

    public string? CodeType { get; set; }

    public double? Rate { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Modified { get; set; }

    public bool? Archive { get; set; }

    public bool? Status { get; set; }
}
