using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class AppointmentCptCode
{
    public long Id { get; set; }

    public Guid? Uuid { get; set; }

    public string? CptCode { get; set; }

    public int? Units { get; set; }

    public long? AppointmentId { get; set; }

    public virtual Appointment? Appointment { get; set; }
}
