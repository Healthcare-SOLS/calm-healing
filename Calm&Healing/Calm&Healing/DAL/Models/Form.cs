using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class Form
{
    public long Id { get; set; }

    public Guid Uuid { get; set; }

    public string FormTitle { get; set; } = null!;

    public string FormType { get; set; } = null!;

    public string FormKey { get; set; } = null!;

    public bool Status { get; set; }

    public bool Archive { get; set; }

    public bool AutoAssign { get; set; }

    public string FormStatus { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
}
