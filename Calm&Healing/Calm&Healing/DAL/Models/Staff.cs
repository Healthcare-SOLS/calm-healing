using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class Staff
{
    public long Id { get; set; }

    public Guid Uuid { get; set; }

    public string? ContactNumber { get; set; }

    public bool? Status { get; set; }

    public bool? Archive { get; set; }

    public long? UserId { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Modified { get; set; }

    public virtual User? User { get; set; }
}
