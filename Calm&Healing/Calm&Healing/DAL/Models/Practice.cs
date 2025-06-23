using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class Practice
{
    public long Id { get; set; }

    public Guid Uuid { get; set; }

    public string? ClinicName { get; set; }

    public string? NpiNumber { get; set; }

    public string? TaxType { get; set; }

    public string? TaxNumber { get; set; }

    public string? ContactNumber { get; set; }

    public string? EmailId { get; set; }

    public string? Taxonomy { get; set; }

    public bool? Archive { get; set; }

    public long? AddressId { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Modified { get; set; }

    public virtual Address? Address { get; set; }
}
