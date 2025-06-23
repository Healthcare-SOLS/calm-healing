using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class User
{
    public long Id { get; set; }

    public Guid Uuid { get; set; }

    public string IamId { get; set; } = null!;

    public string? Email { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public DateTime? LastLogin { get; set; }

    public bool Active { get; set; }

    public bool Archive { get; set; }

    public bool EmailVerified { get; set; }

    public bool PhoneVerified { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public string? MiddleName { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Clinician> Clinicians { get; set; } = new List<Clinician>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
