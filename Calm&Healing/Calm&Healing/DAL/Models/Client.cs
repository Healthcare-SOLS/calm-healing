using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class Client
{
    public long Id { get; set; }

    public Guid? Uuid { get; set; }

    public string? PreferredName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? LegalSex { get; set; }

    public string? GenderIdentity { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Ethnicity { get; set; }

    public string? Race { get; set; }

    public string? PreferredLanguage { get; set; }

    public long? UserId { get; set; }

    public long? AddressId { get; set; }

    public long? EmergencyContactId { get; set; }

    public long? GuardianContactId { get; set; }

    public long? PrimaryClinicianId { get; set; }

    public long? ReferringClinicianId { get; set; }

    public bool? PhoneAppointmentReminder { get; set; }

    public bool? EmailAppointmentRemainder { get; set; }

    public string? PaymentMethod { get; set; }

    public bool? Active { get; set; }

    public bool? Archive { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Modified { get; set; }

    public string? ClientStatus { get; set; }

    public string? ProfileImageUrl { get; set; }

    public bool? TwoFactorAuthentication { get; set; }

    public string? AlertNote { get; set; }

    public bool? PortalAccess { get; set; }

    public string? Mrn { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<ClientGroupSettingsMapping> ClientGroupSettingsMappings { get; set; } = new List<ClientGroupSettingsMapping>();

    public virtual ICollection<ClientInsurance> ClientInsurances { get; set; } = new List<ClientInsurance>();

    public virtual EmergencyContact? EmergencyContact { get; set; }

    public virtual EmergencyContact? GuardianContact { get; set; }

    public virtual Clinician? PrimaryClinician { get; set; }

    public virtual Clinician? ReferringClinician { get; set; }

    public virtual User? User { get; set; }
}
