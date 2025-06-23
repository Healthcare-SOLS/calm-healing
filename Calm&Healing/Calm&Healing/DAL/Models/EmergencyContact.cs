using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class EmergencyContact
{
    public long Id { get; set; }

    public Guid? Uuid { get; set; }

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Relationship { get; set; }

    public bool? ResponsibleParty { get; set; }

    public virtual ICollection<Client> ClientEmergencyContacts { get; set; } = new List<Client>();

    public virtual ICollection<Client> ClientGuardianContacts { get; set; } = new List<Client>();
}
