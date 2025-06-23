using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class Location
{
    public long Id { get; set; }

    public Guid Uuid { get; set; }

    public string? LocationName { get; set; }

    public string? ContactNumber { get; set; }

    public string? EmailId { get; set; }

    public string? GroupNpiNumber { get; set; }

    public bool? Status { get; set; }

    public string? Fax { get; set; }

    public long? AddressId { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Modified { get; set; }

    public bool? Archive { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<ClinicianLocationMapping> ClinicianLocationMappings { get; set; } = new List<ClinicianLocationMapping>();
}
