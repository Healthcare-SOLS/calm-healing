using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class ClinicianLocationMapping
{
    public long Id { get; set; }

    public long ClinicianId { get; set; }

    public long LocationId { get; set; }

    public virtual Clinician Clinician { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;
}
