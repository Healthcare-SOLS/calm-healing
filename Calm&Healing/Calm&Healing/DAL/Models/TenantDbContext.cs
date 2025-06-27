using System;
using System.Collections.Generic;
using Calm_Healing.Service.IService;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace Calm_Healing.DAL.Models;

public partial class TenantDbContext : DbContext
{
    public TenantDbContext()
    {
    }
    private readonly ICurrentUserService _currentUserService;
    // Add a constructor that accepts ICurrentUserService:
    public TenantDbContext(DbContextOptions<TenantDbContext> options, ICurrentUserService currentUserService)
        : base(options)
    {
        _currentUserService = currentUserService;
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<AppointmentCptCode> AppointmentCptCodes { get; set; }

    public virtual DbSet<Availability> Availabilities { get; set; }

    public virtual DbSet<BlockDay> BlockDays { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientClinician> ClientClinicians { get; set; }

    public virtual DbSet<ClientGroupSettingsMapping> ClientGroupSettingsMappings { get; set; }

    public virtual DbSet<ClientInsurance> ClientInsurances { get; set; }

    public virtual DbSet<Clinician> Clinicians { get; set; }

    public virtual DbSet<ClinicianLocationMapping> ClinicianLocationMappings { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Databasechangelog> Databasechangelogs { get; set; }

    public virtual DbSet<Databasechangeloglock> Databasechangeloglocks { get; set; }

    public virtual DbSet<DayWiseSlot> DayWiseSlots { get; set; }

    public virtual DbSet<EmergencyContact> EmergencyContacts { get; set; }

    public virtual DbSet<FeeSchedule> FeeSchedules { get; set; }

    public virtual DbSet<Form> Forms { get; set; }

    public virtual DbSet<GroupSetting> GroupSettings { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Practice> Practices { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermissionMapping> RolePermissionMappings { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<StickyNote> StickyNotes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=practiceeasily.cipuywessyqn.us-east-1.rds.amazonaws.com;Port=5432;Database=PracticeEasilyDotNet;Username=practiceeasily;Password=LLBgK8bG9KuojaVOV6Hu");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("address_pkey");

            entity.ToTable("address");

            entity.HasIndex(e => e.Uuid, "address_uuid_key").IsUnique();

            entity.HasIndex(e => e.Uuid, "uk_address_uuid").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .HasColumnName("city");
            entity.Property(e => e.Created)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.Line1)
                .HasMaxLength(255)
                .HasColumnName("line1");
            entity.Property(e => e.Line2)
                .HasMaxLength(255)
                .HasColumnName("line2");
            entity.Property(e => e.Modified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .HasColumnName("modified_by");
            entity.Property(e => e.State)
                .HasMaxLength(255)
                .HasColumnName("state");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
            entity.Property(e => e.Zipcode)
                .HasMaxLength(255)
                .HasColumnName("zipcode");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("appointment_pkey");

            entity.ToTable("appointment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppointmentMode)
                .HasMaxLength(50)
                .HasColumnName("appointment_mode");
            entity.Property(e => e.AppointmentType)
                .HasMaxLength(50)
                .HasColumnName("appointment_type");
            entity.Property(e => e.BaseAppointmentId).HasColumnName("base_appointment_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.ClinicianId).HasColumnName("clinician_id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.EndTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("end_time");
            entity.Property(e => e.EstimatedAmount).HasColumnName("estimated_amount");
            entity.Property(e => e.GroupSettingsId).HasColumnName("group_settings_id");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.Modified)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .HasColumnName("modified_by");
            entity.Property(e => e.MonthDay).HasColumnName("month_day");
            entity.Property(e => e.Note)
                .HasMaxLength(500)
                .HasColumnName("note");
            entity.Property(e => e.NumberOfAppointments).HasColumnName("number_of_appointments");
            entity.Property(e => e.PlaceOfService)
                .HasMaxLength(100)
                .HasColumnName("place_of_service");
            entity.Property(e => e.RecurrenceType)
                .HasMaxLength(50)
                .HasColumnName("recurrence_type");
            entity.Property(e => e.RepeatEvery).HasColumnName("repeat_every");
            entity.Property(e => e.RepeatOnDays)
                .HasMaxLength(250)
                .HasColumnName("repeat_on_days");
            entity.Property(e => e.SessionType)
                .HasMaxLength(50)
                .HasColumnName("session_type");
            entity.Property(e => e.StartTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_time");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Timezone)
                .HasMaxLength(50)
                .HasColumnName("timezone");
            entity.Property(e => e.Uuid).HasColumnName("uuid");

            entity.HasOne(d => d.Client).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("fk_appointment_client");

            entity.HasOne(d => d.Clinician).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ClinicianId)
                .HasConstraintName("fk_appointment_clinician");

            entity.HasOne(d => d.GroupSettings).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.GroupSettingsId)
                .HasConstraintName("fk_appointment_group_settings");

            entity.HasOne(d => d.Location).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("fk_appointment_location");
        });

        modelBuilder.Entity<AppointmentCptCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("appointment_cpt_codes_pkey");

            entity.ToTable("appointment_cpt_codes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.CptCode)
                .HasMaxLength(255)
                .HasColumnName("cpt_code");
            entity.Property(e => e.Units).HasColumnName("units");
            entity.Property(e => e.Uuid).HasColumnName("uuid");

            entity.HasOne(d => d.Appointment).WithMany(p => p.AppointmentCptCodes)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("fk_appointment_appointment_cpt_codes");
        });

        modelBuilder.Entity<Availability>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("availability_pkey");

            entity.ToTable("availability");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClinicianId).HasColumnName("clinician_id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.Modified)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .HasColumnName("modified_by");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.TimeZone)
                .HasMaxLength(100)
                .HasColumnName("time_zone");
            entity.Property(e => e.Uuid).HasColumnName("uuid");

            entity.HasOne(d => d.Clinician).WithMany(p => p.Availabilities)
                .HasForeignKey(d => d.ClinicianId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_availability_clinician");
        });

        modelBuilder.Entity<BlockDay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("block_days_pkey");

            entity.ToTable("block_days");

            entity.HasIndex(e => e.Uuid, "uk_block_days_uuid").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AvailabilityId).HasColumnName("availability_id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.EndTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("end_time");
            entity.Property(e => e.Modified)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .HasColumnName("modified_by");
            entity.Property(e => e.StartTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_time");
            entity.Property(e => e.Uuid).HasColumnName("uuid");

            entity.HasOne(d => d.Availability).WithMany(p => p.BlockDays)
                .HasForeignKey(d => d.AvailabilityId)
                .HasConstraintName("fk_block_days_availability");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_pkey");

            entity.ToTable("client");

            entity.HasIndex(e => e.Uuid, "uk_client_uuid").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.AlertNote).HasColumnName("alert_note");
            entity.Property(e => e.Archive).HasColumnName("archive");
            entity.Property(e => e.ClientStatus)
                .HasMaxLength(50)
                .HasColumnName("client_status");
            entity.Property(e => e.Created)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.EmailAppointmentRemainder).HasColumnName("email_appointment_remainder");
            entity.Property(e => e.EmergencyContactId).HasColumnName("emergency_contact_id");
            entity.Property(e => e.Ethnicity)
                .HasMaxLength(100)
                .HasColumnName("ethnicity");
            entity.Property(e => e.GenderIdentity)
                .HasMaxLength(50)
                .HasColumnName("gender_identity");
            entity.Property(e => e.GuardianContactId).HasColumnName("guardian_contact_id");
            entity.Property(e => e.LegalSex)
                .HasMaxLength(50)
                .HasColumnName("legal_sex");
            entity.Property(e => e.Modified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .HasColumnName("modified_by");
            entity.Property(e => e.Mrn)
                .HasMaxLength(20)
                .HasColumnName("mrn");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("payment_method");
            entity.Property(e => e.PhoneAppointmentReminder).HasColumnName("phone_appointment_reminder");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("phone_number");
            entity.Property(e => e.PortalAccess).HasColumnName("portal_access");
            entity.Property(e => e.PreferredLanguage)
                .HasMaxLength(100)
                .HasColumnName("preferred_language");
            entity.Property(e => e.PreferredName)
                .HasMaxLength(100)
                .HasColumnName("preferred_name");
            entity.Property(e => e.PrimaryClinicianId).HasColumnName("primary_clinician_id");
            entity.Property(e => e.ProfileImageUrl)
                .HasMaxLength(250)
                .HasColumnName("profile_image_url");
            entity.Property(e => e.Race)
                .HasMaxLength(100)
                .HasColumnName("race");
            entity.Property(e => e.ReferringClinicianId).HasColumnName("referring_clinician_id");
            entity.Property(e => e.TwoFactorAuthentication).HasColumnName("two_factor_authentication");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Uuid).HasColumnName("uuid");

            entity.HasOne(d => d.Address).WithMany(p => p.Clients)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("fk_client_address");

            entity.HasOne(d => d.EmergencyContact).WithMany(p => p.ClientEmergencyContacts)
                .HasForeignKey(d => d.EmergencyContactId)
                .HasConstraintName("fk_client_emergency_contact");

            entity.HasOne(d => d.GuardianContact).WithMany(p => p.ClientGuardianContacts)
                .HasForeignKey(d => d.GuardianContactId)
                .HasConstraintName("fk_client_guardian_contact");

            entity.HasOne(d => d.PrimaryClinician).WithMany(p => p.ClientPrimaryClinicians)
                .HasForeignKey(d => d.PrimaryClinicianId)
                .HasConstraintName("fk_client_primary_clinician");

            entity.HasOne(d => d.ReferringClinician).WithMany(p => p.ClientReferringClinicians)
                .HasForeignKey(d => d.ReferringClinicianId)
                .HasConstraintName("fk_client_referring_clinician");

            entity.HasOne(d => d.User).WithMany(p => p.Clients)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_client_user");
        });

        modelBuilder.Entity<ClientClinician>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_clinician_pkey");

            entity.ToTable("client_clinician");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.ClinicianId).HasColumnName("clinician_id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("now()")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.Modified)
                .HasDefaultValueSql("now()")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .HasColumnName("modified_by");
        });

        modelBuilder.Entity<ClientGroupSettingsMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_group_settings_mapping_pkey");

            entity.ToTable("client_group_settings_mapping");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.GroupId).HasColumnName("group_id");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientGroupSettingsMappings)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cgsm_client");

            entity.HasOne(d => d.Group).WithMany(p => p.ClientGroupSettingsMappings)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cgsm_group_settings");
        });

        modelBuilder.Entity<ClientInsurance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_insurance_pkey");

            entity.ToTable("client_insurance");

            entity.HasIndex(e => e.Uuid, "uk_client_insurance_uuid").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.GroupId)
                .HasMaxLength(100)
                .HasColumnName("group_id");
            entity.Property(e => e.InsuranceCardBack)
                .HasMaxLength(255)
                .HasColumnName("insurance_card_back");
            entity.Property(e => e.InsuranceCardFront)
                .HasMaxLength(255)
                .HasColumnName("insurance_card_front");
            entity.Property(e => e.InsuranceName)
                .HasMaxLength(255)
                .HasColumnName("insurance_name");
            entity.Property(e => e.InsuranceType)
                .HasMaxLength(50)
                .HasColumnName("insurance_type");
            entity.Property(e => e.MemberId)
                .HasMaxLength(100)
                .HasColumnName("member_id");
            entity.Property(e => e.Relationship)
                .HasMaxLength(50)
                .HasColumnName("relationship");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.SubscriberBirthDate).HasColumnName("subscriber_birth_date");
            entity.Property(e => e.SubscriberFirstName)
                .HasMaxLength(100)
                .HasColumnName("subscriber_first_name");
            entity.Property(e => e.SubscriberLastName)
                .HasMaxLength(100)
                .HasColumnName("subscriber_last_name");
            entity.Property(e => e.Uuid).HasColumnName("uuid");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientInsurances)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("fk_insurance_patient");
        });

        modelBuilder.Entity<Clinician>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clinician_pkey");

            entity.ToTable("clinician");

            entity.HasIndex(e => e.Uuid, "uk_clinician_uuid").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Archive)
                .HasDefaultValue(false)
                .HasColumnName("archive");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(255)
                .HasColumnName("contact_number");
            entity.Property(e => e.Created)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.LanguagesSpoken)
                .HasMaxLength(255)
                .HasColumnName("languages_spoken");
            entity.Property(e => e.Modified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .HasColumnName("modified_by");
            entity.Property(e => e.NpiNumber)
                .HasMaxLength(255)
                .HasColumnName("npi_number");
            entity.Property(e => e.Signature)
                .HasMaxLength(200)
                .HasColumnName("signature");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.SupervisorClinicianUuid).HasColumnName("supervisor_clinician_uuid");
            entity.Property(e => e.TwoFactorAuthentication).HasColumnName("two_factor_authentication");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Uuid).HasColumnName("uuid");

            entity.HasOne(d => d.User).WithMany(p => p.Clinicians)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_clinician_user");
        });

        modelBuilder.Entity<ClinicianLocationMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clinician_location_mapping_pkey");

            entity.ToTable("clinician_location_mapping");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClinicianId).HasColumnName("clinician_id");
            entity.Property(e => e.LocationId).HasColumnName("location_id");

            entity.HasOne(d => d.Clinician).WithMany(p => p.ClinicianLocationMappings)
                .HasForeignKey(d => d.ClinicianId)
                .HasConstraintName("fk_clinician_location_mapping_clinician");

            entity.HasOne(d => d.Location).WithMany(p => p.ClinicianLocationMappings)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("fk_clinician_location_mapping_location");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("contacts_pkey");

            entity.ToTable("contacts");

            entity.HasIndex(e => e.Uuid, "contacts_uuid_key").IsUnique();

            entity.HasIndex(e => e.Uuid, "uk_contacts_uuid").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.Archive)
                .HasDefaultValue(false)
                .HasColumnName("archive");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(255)
                .HasColumnName("contact_number");
            entity.Property(e => e.ContactType)
                .HasMaxLength(50)
                .HasColumnName("contact_type");
            entity.Property(e => e.Created)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.EmailId)
                .HasMaxLength(255)
                .HasColumnName("email_id");
            entity.Property(e => e.FaxNumber)
                .HasMaxLength(255)
                .HasColumnName("fax_number");
            entity.Property(e => e.Modified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .HasColumnName("modified_by");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.Uuid).HasColumnName("uuid");

            entity.HasOne(d => d.Address).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("fk_contacts_address");
        });

        modelBuilder.Entity<Databasechangelog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("databasechangelog");

            entity.Property(e => e.Author)
                .HasMaxLength(255)
                .HasColumnName("author");
            entity.Property(e => e.Comments)
                .HasMaxLength(255)
                .HasColumnName("comments");
            entity.Property(e => e.Contexts)
                .HasMaxLength(255)
                .HasColumnName("contexts");
            entity.Property(e => e.Dateexecuted)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateexecuted");
            entity.Property(e => e.DeploymentId)
                .HasMaxLength(10)
                .HasColumnName("deployment_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Exectype)
                .HasMaxLength(10)
                .HasColumnName("exectype");
            entity.Property(e => e.Filename)
                .HasMaxLength(255)
                .HasColumnName("filename");
            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .HasColumnName("id");
            entity.Property(e => e.Labels)
                .HasMaxLength(255)
                .HasColumnName("labels");
            entity.Property(e => e.Liquibase)
                .HasMaxLength(20)
                .HasColumnName("liquibase");
            entity.Property(e => e.Md5sum)
                .HasMaxLength(35)
                .HasColumnName("md5sum");
            entity.Property(e => e.Orderexecuted).HasColumnName("orderexecuted");
            entity.Property(e => e.Tag)
                .HasMaxLength(255)
                .HasColumnName("tag");
        });

        modelBuilder.Entity<Databasechangeloglock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("databasechangeloglock_pkey");

            entity.ToTable("databasechangeloglock");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Locked).HasColumnName("locked");
            entity.Property(e => e.Lockedby)
                .HasMaxLength(255)
                .HasColumnName("lockedby");
            entity.Property(e => e.Lockgranted)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lockgranted");
        });

        modelBuilder.Entity<DayWiseSlot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("day_wise_slots_pkey");

            entity.ToTable("day_wise_slots");

            entity.HasIndex(e => e.Uuid, "uk_day_wise_slots_uuid").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AvailabilityId).HasColumnName("availability_id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.DayOfWeek)
                .HasMaxLength(20)
                .HasColumnName("day_of_week");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.Modified)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .HasColumnName("modified_by");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.Uuid).HasColumnName("uuid");

            entity.HasOne(d => d.Availability).WithMany(p => p.DayWiseSlots)
                .HasForeignKey(d => d.AvailabilityId)
                .HasConstraintName("fk_day_wise_slots_availability");
        });

        modelBuilder.Entity<EmergencyContact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("emergency_contact_pkey");

            entity.ToTable("emergency_contact");

            entity.HasIndex(e => e.Uuid, "uk_emergency_contact_uuid").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("phone_number");
            entity.Property(e => e.Relationship)
                .HasMaxLength(50)
                .HasColumnName("relationship");
            entity.Property(e => e.ResponsibleParty).HasColumnName("responsible_party");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
        });

        modelBuilder.Entity<FeeSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("fee_schedule_pkey");

            entity.ToTable("fee_schedule");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Archive)
                .HasDefaultValue(false)
                .HasColumnName("archive");
            entity.Property(e => e.CodeType)
                .HasMaxLength(50)
                .HasColumnName("code_type");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.Modified)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .HasColumnName("modified_by");
            entity.Property(e => e.ProcedureCode)
                .HasMaxLength(255)
                .HasColumnName("procedure_code");
            entity.Property(e => e.Rate).HasColumnName("rate");
            entity.Property(e => e.Status)
                .HasDefaultValue(false)
                .HasColumnName("status");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
        });

        modelBuilder.Entity<Form>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("forms_pkey");

            entity.ToTable("forms");

            entity.HasIndex(e => e.Uuid, "forms_uuid_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Archive)
                .HasDefaultValue(false)
                .HasColumnName("archive");
            entity.Property(e => e.AutoAssign).HasColumnName("auto_assign");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.FormKey)
                .HasMaxLength(255)
                .HasColumnName("form_key");
            entity.Property(e => e.FormStatus)
                .HasMaxLength(50)
                .HasColumnName("form_status");
            entity.Property(e => e.FormTitle)
                .HasMaxLength(255)
                .HasColumnName("form_title");
            entity.Property(e => e.FormType)
                .HasMaxLength(50)
                .HasColumnName("form_type");
            entity.Property(e => e.Modified)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .HasColumnName("modified_by");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
        });

        modelBuilder.Entity<GroupSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("group_settings_pkey");

            entity.ToTable("group_settings");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Archive)
                .HasDefaultValue(false)
                .HasColumnName("archive");
            entity.Property(e => e.BillTo).HasColumnName("bill_to");
            entity.Property(e => e.ClinicianId).HasColumnName("clinician_id");
            entity.Property(e => e.CptCode)
                .HasMaxLength(50)
                .HasColumnName("cpt_code");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.FamilyGroup)
                .HasDefaultValue(false)
                .HasColumnName("family_group");
            entity.Property(e => e.GroupInitials)
                .HasMaxLength(50)
                .HasColumnName("group_initials");
            entity.Property(e => e.GroupName)
                .HasMaxLength(255)
                .HasColumnName("group_name");
            entity.Property(e => e.Modified)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .HasColumnName("modified_by");
            entity.Property(e => e.Uuid).HasColumnName("uuid");

            entity.HasOne(d => d.Clinician).WithMany(p => p.GroupSettings)
                .HasForeignKey(d => d.ClinicianId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_group_settings_clinician");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("location_pkey");

            entity.ToTable("location");

            entity.HasIndex(e => e.Uuid, "location_uuid_key").IsUnique();

            entity.HasIndex(e => e.Uuid, "uk_location_uuid").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.Archive)
                .HasDefaultValue(false)
                .HasColumnName("archive");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(255)
                .HasColumnName("contact_number");
            entity.Property(e => e.Created)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.EmailId)
                .HasMaxLength(255)
                .HasColumnName("email_id");
            entity.Property(e => e.Fax)
                .HasMaxLength(255)
                .HasColumnName("fax");
            entity.Property(e => e.GroupNpiNumber)
                .HasMaxLength(255)
                .HasColumnName("group_npi_number");
            entity.Property(e => e.LocationName)
                .HasMaxLength(255)
                .HasColumnName("location_name");
            entity.Property(e => e.Modified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .HasColumnName("modified_by");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.Uuid).HasColumnName("uuid");

            entity.HasOne(d => d.Address).WithMany(p => p.Locations)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("fk_location_address");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("notifications_pkey");

            entity.ToTable("notifications");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.MarkedAsRead).HasColumnName("marked_as_read");
            entity.Property(e => e.Modified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .HasColumnName("modified_by");
            entity.Property(e => e.NotificationType)
                .HasMaxLength(50)
                .HasColumnName("notification_type");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("permissions_pkey");

            entity.ToTable("permissions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.Group)
                .HasMaxLength(100)
                .HasColumnName("group");
            entity.Property(e => e.Modified)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .HasColumnName("modified_by");
            entity.Property(e => e.Permission1)
                .HasMaxLength(255)
                .HasColumnName("permission");
            entity.Property(e => e.PermissionKey)
                .HasMaxLength(255)
                .HasColumnName("permission_key");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
        });

        modelBuilder.Entity<Practice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("practice_pkey");

            entity.ToTable("practice");

            entity.HasIndex(e => e.Uuid, "practice_uuid_key").IsUnique();

            entity.HasIndex(e => e.Uuid, "uk_practice_uuid").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.Archive)
                .HasDefaultValue(false)
                .HasColumnName("archive");
            entity.Property(e => e.ClinicName)
                .HasMaxLength(255)
                .HasColumnName("clinic_name");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(255)
                .HasColumnName("contact_number");
            entity.Property(e => e.Created)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.EmailId)
                .HasMaxLength(255)
                .HasColumnName("email_id");
            entity.Property(e => e.Modified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .HasColumnName("modified_by");
            entity.Property(e => e.NpiNumber)
                .HasMaxLength(255)
                .HasColumnName("npi_number");
            entity.Property(e => e.TaxNumber)
                .HasMaxLength(255)
                .HasColumnName("tax_number");
            entity.Property(e => e.TaxType)
                .HasMaxLength(255)
                .HasColumnName("tax_type");
            entity.Property(e => e.Taxonomy)
                .HasMaxLength(255)
                .HasColumnName("taxonomy");
            entity.Property(e => e.Uuid).HasColumnName("uuid");

            entity.HasOne(d => d.Address).WithMany(p => p.Practices)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("fk_practice_address");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.Modified)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .HasColumnName("modified_by");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .HasColumnName("role_name");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
        });

        modelBuilder.Entity<RolePermissionMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_permission_mapping_pkey");

            entity.ToTable("role_permission_mapping");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissionMappings)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_role_permission_permission");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissionMappings)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_role_permission_role");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("staff_pkey");

            entity.ToTable("staff");

            entity.HasIndex(e => e.Uuid, "staff_uuid_key").IsUnique();

            entity.HasIndex(e => e.Uuid, "uk_staff_uuid").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Archive)
                .HasDefaultValue(false)
                .HasColumnName("archive");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(255)
                .HasColumnName("contact_number");
            entity.Property(e => e.Created)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.Modified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .HasColumnName("modified_by");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Uuid).HasColumnName("uuid");

            entity.HasOne(d => d.User).WithMany(p => p.Staff)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_staff_user");
        });

        modelBuilder.Entity<StickyNote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sticky_note_pkey");

            entity.ToTable("sticky_note");

            entity.HasIndex(e => e.Uuid, "sticky_note_uuid_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlertNote).HasColumnName("alert_note");
            entity.Property(e => e.AlertNoteCreatedBy)
                .HasMaxLength(100)
                .HasColumnName("alert_note_created_by");
            entity.Property(e => e.AlertNoteModified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("alert_note_modified");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.Created)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.Modified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .HasColumnName("modified_by");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.StickyNoteCreatedBy)
                .HasMaxLength(100)
                .HasColumnName("sticky_note_created_by");
            entity.Property(e => e.StickyNoteModified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("sticky_note_modified");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Uuid, "uk_user_uuid").IsUnique();

            entity.HasIndex(e => e.Email, "uk_users_email").IsUnique();

            entity.HasIndex(e => e.IamId, "uk_users_iam_id").IsUnique();

            entity.HasIndex(e => e.Uuid, "users_uuid_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Archive).HasColumnName("archive");
            entity.Property(e => e.Created)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.EmailVerified).HasColumnName("email_verified");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.IamId)
                .HasMaxLength(255)
                .HasColumnName("iam_id");
            entity.Property(e => e.LastLogin)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_login");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(255)
                .HasColumnName("middle_name");
            entity.Property(e => e.Modified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .HasColumnName("modified_by");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .HasColumnName("phone_number");
            entity.Property(e => e.PhoneVerified).HasColumnName("phone_verified");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_role_pkey");

            entity.ToTable("user_role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("fk_user-role_role");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_user-role_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }
    // Add these methods before the OnConfiguring method:
    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    /*private void UpdateAuditFields()
    {
        var userName = _currentUserService?.GetCurrentUsername() ?? "System";
        var now = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);

        foreach (var entry in ChangeTracker.Entries<Location>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = userName;
                entry.Entity.Created = now;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.ModifiedBy = userName;
                entry.Entity.Modified = now;
            }
        }
    }*/

    private void UpdateAuditFields()
    {
        var userName = _currentUserService?.GetCurrentUserId() ?? "System";
        var now = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);

        foreach (var entry in ChangeTracker.Entries())
        {
            switch (entry.Entity)
            {
                case Location location:
                    if (entry.State == EntityState.Added)
                    {
                        location.CreatedBy = userName;
                        location.Created = now;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        location.ModifiedBy = userName;
                        location.Modified = now;
                    }
                    break;

                case Address address:
                    if (entry.State == EntityState.Added)
                    {
                        address.CreatedBy = userName;
                        address.Created = now;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        address.ModifiedBy = userName;
                        address.Modified = now;
                    }
                    break;
            }
        }
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
