using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class ClientClinician
{
    public long Id { get; set; }

    public Guid ClientId { get; set; }

    public Guid ClinicianId { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Modified { get; set; }
}
