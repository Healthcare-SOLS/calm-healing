using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class Contact
{
    public long Id { get; set; }

    public Guid Uuid { get; set; }

    public string? Name { get; set; }

    public string? ContactNumber { get; set; }

    public string? EmailId { get; set; }

    public string? FaxNumber { get; set; }

    public bool? Status { get; set; }

    public string? ContactType { get; set; }

    public bool? Archive { get; set; }

    public long? AddressId { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Modified { get; set; }

    public virtual Address? Address { get; set; }
}
