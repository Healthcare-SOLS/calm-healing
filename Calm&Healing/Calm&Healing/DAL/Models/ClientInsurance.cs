using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class ClientInsurance
{
    public long Id { get; set; }

    public Guid? Uuid { get; set; }

    public string? InsuranceName { get; set; }

    public string? MemberId { get; set; }

    public string? GroupId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Relationship { get; set; }

    public string? SubscriberFirstName { get; set; }

    public string? SubscriberLastName { get; set; }

    public DateOnly? SubscriberBirthDate { get; set; }

    public string? InsuranceCardFront { get; set; }

    public string? InsuranceCardBack { get; set; }

    public long? ClientId { get; set; }

    public string? InsuranceType { get; set; }

    public virtual Client? Client { get; set; }
}
