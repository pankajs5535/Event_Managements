using System;
using System.Collections.Generic;
using Event_Management.Persistence;
using Microsoft.EntityFrameworkCore;
using Event_Management.Domain.Entities;


namespace Event_Management.Persistence.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventCategory> EventCategories { get; set; }

    public virtual DbSet<EventSession> EventSessions { get; set; }

    public virtual DbSet<Exhibitor> Exhibitors { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<OrganizationContact> OrganizationContacts { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SessionAttendance> SessionAttendances { get; set; }

    public virtual DbSet<SessionSpeaker> SessionSpeakers { get; set; }

    public virtual DbSet<Speaker> Speakers { get; set; }

    public virtual DbSet<Sponsor> Sponsors { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketType> TicketTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }

    public virtual DbSet<VenueHall> VenueHalls { get; set; }

    public virtual DbSet<VwEventSummary> VwEventSummaries { get; set; }

    public virtual DbSet<VwRegistrationDetail> VwRegistrationDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=Skylark;Database=Event_Managements;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditId).IsClustered(false);

            entity.ToTable("AuditLogs", "Audit");

            entity.HasIndex(e => e.ChangedAt, "IX_AuditLogs_ChangedAt");

            entity.HasIndex(e => new { e.TableName, e.RecordId }, "IX_AuditLogs_Table_Record");

            entity.Property(e => e.AuditId).HasColumnName("AuditID");
            entity.Property(e => e.Action).HasMaxLength(10);
            entity.Property(e => e.ApplicationName).HasMaxLength(128);
            entity.Property(e => e.ChangedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_AuditLogs_ChangedAt");
            entity.Property(e => e.HostName).HasMaxLength(128);
            entity.Property(e => e.RecordId)
                .HasMaxLength(50)
                .HasColumnName("RecordID");
            entity.Property(e => e.TableName).HasMaxLength(128);

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.ChangedBy)
                .HasConstraintName("FK_AuditLogs_Users");
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.ToTable("Certificates", "Comms");

            entity.HasIndex(e => e.RegistrationId, "IX_Certificates_RegistrationID");

            entity.HasIndex(e => e.VerificationCode, "UQ_Certificates_VerificationCode").IsUnique();

            entity.Property(e => e.CertificateId).HasColumnName("CertificateID");
            entity.Property(e => e.CertificateType).HasMaxLength(30);
            entity.Property(e => e.CertificateUrl)
                .HasMaxLength(400)
                .HasColumnName("CertificateURL");
            entity.Property(e => e.IssuedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Certificates_IssuedAt");
            entity.Property(e => e.RegistrationId).HasColumnName("RegistrationID");
            entity.Property(e => e.VerificationCode)
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.HasOne(d => d.Registration).WithMany(p => p.Certificates)
                .HasForeignKey(d => d.RegistrationId)
                .HasConstraintName("FK_Certificates_Registrations");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Events", "Event", tb => tb.HasTrigger("trg_Events_Audit"));

            entity.HasIndex(e => e.OrganizationId, "IX_Events_OrganizationID");

            entity.HasIndex(e => new { e.Status, e.StartDate }, "IX_Events_Status_StartDate");

            entity.HasIndex(e => e.VenueId, "IX_Events_VenueID").HasFilter("([VenueID] IS NOT NULL)");

            entity.HasIndex(e => e.EventCode, "UQ_Events_EventCode").IsUnique();

            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Events_CreatedAt");
            entity.Property(e => e.EndDate).HasPrecision(0);
            entity.Property(e => e.EventCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IsPublic).HasDefaultValue(true, "DF_Events_IsPublic");
            entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");
            entity.Property(e => e.StartDate).HasPrecision(0);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Draft", "DF_Events_Status");
            entity.Property(e => e.TimeZone)
                .HasMaxLength(50)
                .HasDefaultValue("UTC", "DF_Events_TimeZone");
            entity.Property(e => e.Title).HasMaxLength(250);
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
            entity.Property(e => e.VenueId).HasColumnName("VenueID");

            entity.HasOne(d => d.Category).WithMany(p => p.Events)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Events_Categories");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Events_CreatedBy");

            entity.HasOne(d => d.Organization).WithMany(p => p.Events)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Events_Organizations");

            entity.HasOne(d => d.Venue).WithMany(p => p.Events)
                .HasForeignKey(d => d.VenueId)
                .HasConstraintName("FK_Events_Venues");
        });

        modelBuilder.Entity<EventCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("EventCategories", "Event");

            entity.HasIndex(e => e.CategoryName, "UQ_EventCategories_Name").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(300);
        });

        modelBuilder.Entity<EventSession>(entity =>
        {
            entity.HasKey(e => e.SessionId);

            entity.ToTable("EventSessions", "Event");

            entity.HasIndex(e => new { e.EventId, e.StartTime }, "IX_Sessions_EventID_StartTime");

            entity.Property(e => e.SessionId).HasColumnName("SessionID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Sessions_CreatedAt");
            entity.Property(e => e.EndTime).HasPrecision(0);
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.HallId).HasColumnName("HallID");
            entity.Property(e => e.SessionType)
                .HasMaxLength(30)
                .HasDefaultValue("Session", "DF_Sessions_Type");
            entity.Property(e => e.StartTime).HasPrecision(0);
            entity.Property(e => e.Title).HasMaxLength(250);

            entity.HasOne(d => d.Event).WithMany(p => p.EventSessions)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK_Sessions_Events");

            entity.HasOne(d => d.Hall).WithMany(p => p.EventSessions)
                .HasForeignKey(d => d.HallId)
                .HasConstraintName("FK_Sessions_Halls");
        });

        modelBuilder.Entity<Exhibitor>(entity =>
        {
            entity.ToTable("Exhibitors", "Event");

            entity.HasIndex(e => new { e.EventId, e.BoothNumber }, "UQ_Exhibitors_Event_Booth").IsUnique();

            entity.Property(e => e.ExhibitorId).HasColumnName("ExhibitorID");
            entity.Property(e => e.Amount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.BoothNumber).HasMaxLength(20);
            entity.Property(e => e.ContactPersonId).HasColumnName("ContactPersonID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Exhibitors_CreatedAt");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

            entity.HasOne(d => d.ContactPerson).WithMany(p => p.Exhibitors)
                .HasForeignKey(d => d.ContactPersonId)
                .HasConstraintName("FK_Exhibitors_ContactPerson");

            entity.HasOne(d => d.Event).WithMany(p => p.Exhibitors)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK_Exhibitors_Events");

            entity.HasOne(d => d.Organization).WithMany(p => p.Exhibitors)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Exhibitors_Organizations");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.ToTable("Feedback", "Comms");

            entity.HasIndex(e => e.EventId, "IX_Feedback_EventID");

            entity.HasIndex(e => new { e.EventId, e.SessionId, e.UserId }, "UQ_Feedback_Event_Session_User").IsUnique();

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.SessionId).HasColumnName("SessionID");
            entity.Property(e => e.SubmittedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Feedback_SubmittedAt");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Event).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Feedback_Events");

            entity.HasOne(d => d.Session).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK_Feedback_Sessions");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Feedback_Users");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notifications", "Comms");

            entity.HasIndex(e => e.Status, "IX_Notifications_Status").HasFilter("([Status]='Queued')");

            entity.HasIndex(e => e.UserId, "IX_Notifications_UserID");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Notifications_CreatedAt");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.NotificationType).HasMaxLength(20);
            entity.Property(e => e.SentAt).HasPrecision(0);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Queued", "DF_Notifications_Status");
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Event).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK_Notifications_Events");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notifications_Users");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.ToTable("Organizations", "Org");

            entity.HasIndex(e => e.OrgName, "UQ_Organizations_Name").IsUnique();

            entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");
            entity.Property(e => e.AddressLine1).HasMaxLength(200);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Organizations_CreatedAt");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Organizations_IsActive");
            entity.Property(e => e.OrgName).HasMaxLength(200);
            entity.Property(e => e.OrgType).HasMaxLength(30);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.PostalCode).HasMaxLength(20);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.Website).HasMaxLength(200);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Organizations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Organizations_CreatedBy");
        });

        modelBuilder.Entity<OrganizationContact>(entity =>
        {
            entity.HasKey(e => e.ContactId);

            entity.ToTable("OrganizationContacts", "Org");

            entity.HasIndex(e => new { e.OrganizationId, e.UserId }, "UQ_OrgContacts_Org_User").IsUnique();

            entity.Property(e => e.ContactId).HasColumnName("ContactID");
            entity.Property(e => e.Designation).HasMaxLength(100);
            entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Organization).WithMany(p => p.OrganizationContacts)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("FK_OrgContacts_Organizations");

            entity.HasOne(d => d.User).WithMany(p => p.OrganizationContacts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrgContacts_Users");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payments", "Ticketing");

            entity.HasIndex(e => new { e.PaymentStatus, e.PaidAt }, "IX_Payments_Status_PaidAt");

            entity.HasIndex(e => e.TransactionRef, "UQ_Payments_TransactionRef").IsUnique();

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Payments_CreatedAt");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasDefaultValue("USD", "DF_Payments_Currency");
            entity.Property(e => e.PaidAt).HasPrecision(0);
            entity.Property(e => e.PaymentMethod).HasMaxLength(30);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .HasDefaultValue("Pending", "DF_Payments_Status");
            entity.Property(e => e.RegistrationId).HasColumnName("RegistrationID");
            entity.Property(e => e.TransactionRef)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Registration).WithMany(p => p.Payments)
                .HasForeignKey(d => d.RegistrationId)
                .HasConstraintName("FK_Payments_Registrations");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.ToTable("Registrations", "Ticketing");

            entity.HasIndex(e => new { e.EventId, e.Status }, "IX_Registrations_EventID_Status");

            entity.HasIndex(e => e.UserId, "IX_Registrations_UserID");

            entity.HasIndex(e => new { e.EventId, e.UserId }, "UQ_Registrations_Event_User").IsUnique();

            entity.Property(e => e.RegistrationId).HasColumnName("RegistrationID");
            entity.Property(e => e.CheckInTime).HasPrecision(0);
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.RegistrationDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Registrations_Date");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending", "DF_Registrations_Status");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Event).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Registrations_Events");

            entity.HasOne(d => d.User).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Registrations_Users");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Roles", "Security");

            entity.HasIndex(e => e.RoleName, "UQ_Roles_RoleName").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Roles_CreatedAt");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<SessionAttendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId);

            entity.ToTable("SessionAttendance", "Ticketing");

            entity.HasIndex(e => new { e.SessionId, e.RegistrationId }, "UQ_SessionAttendance_Session_Reg").IsUnique();

            entity.Property(e => e.AttendanceId).HasColumnName("AttendanceID");
            entity.Property(e => e.CheckInTime)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_SessionAttendance_CheckIn");
            entity.Property(e => e.RegistrationId).HasColumnName("RegistrationID");
            entity.Property(e => e.SessionId).HasColumnName("SessionID");

            entity.HasOne(d => d.Registration).WithMany(p => p.SessionAttendances)
                .HasForeignKey(d => d.RegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SessionAttendance_Registrations");

            entity.HasOne(d => d.Session).WithMany(p => p.SessionAttendances)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK_SessionAttendance_Sessions");
        });

        modelBuilder.Entity<SessionSpeaker>(entity =>
        {
            entity.HasKey(e => new { e.SessionId, e.SpeakerId });

            entity.ToTable("SessionSpeakers", "Event");

            entity.Property(e => e.SessionId).HasColumnName("SessionID");
            entity.Property(e => e.SpeakerId).HasColumnName("SpeakerID");
            entity.Property(e => e.SpeakerRole)
                .HasMaxLength(20)
                .HasDefaultValue("Speaker", "DF_SessionSpeakers_Role");

            entity.HasOne(d => d.Session).WithMany(p => p.SessionSpeakers)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK_SessionSpeakers_Sessions");

            entity.HasOne(d => d.Speaker).WithMany(p => p.SessionSpeakers)
                .HasForeignKey(d => d.SpeakerId)
                .HasConstraintName("FK_SessionSpeakers_Speakers");
        });

        modelBuilder.Entity<Speaker>(entity =>
        {
            entity.ToTable("Speakers", "Event");

            entity.HasIndex(e => e.Email, "UQ_Speakers_Email").IsUnique();

            entity.Property(e => e.SpeakerId).HasColumnName("SpeakerID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Speakers_CreatedAt");
            entity.Property(e => e.Designation).HasMaxLength(150);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.OrganizationName).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.ProfileImageUrl)
                .HasMaxLength(400)
                .HasColumnName("ProfileImageURL");
        });

        modelBuilder.Entity<Sponsor>(entity =>
        {
            entity.ToTable("Sponsors", "Event");

            entity.HasIndex(e => new { e.OrganizationId, e.EventId }, "UQ_Sponsors_Org_Event").IsUnique();

            entity.Property(e => e.SponsorId).HasColumnName("SponsorID");
            entity.Property(e => e.Amount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Sponsors_CreatedAt");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasDefaultValue("USD", "DF_Sponsors_Currency");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.LogoUrl)
                .HasMaxLength(400)
                .HasColumnName("LogoURL");
            entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");
            entity.Property(e => e.SponsorshipTier).HasMaxLength(20);

            entity.HasOne(d => d.Event).WithMany(p => p.Sponsors)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK_Sponsors_Events");

            entity.HasOne(d => d.Organization).WithMany(p => p.Sponsors)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sponsors_Organizations");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("Tickets", "Ticketing");

            entity.HasIndex(e => e.RegistrationId, "IX_Tickets_RegistrationID");

            entity.HasIndex(e => e.Status, "IX_Tickets_Status").HasFilter("([Status]='Active')");

            entity.HasIndex(e => e.TicketCode, "UQ_Tickets_TicketCode").IsUnique();

            entity.Property(e => e.TicketId).HasColumnName("TicketID");
            entity.Property(e => e.IssuedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Tickets_IssuedAt");
            entity.Property(e => e.QrcodeUrl)
                .HasMaxLength(400)
                .HasColumnName("QRCodeURL");
            entity.Property(e => e.RegistrationId).HasColumnName("RegistrationID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Active", "DF_Tickets_Status");
            entity.Property(e => e.TicketCode)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TicketTypeId).HasColumnName("TicketTypeID");

            entity.HasOne(d => d.Registration).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.RegistrationId)
                .HasConstraintName("FK_Tickets_Registrations");

            entity.HasOne(d => d.TicketType).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.TicketTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_TicketTypes");
        });

        modelBuilder.Entity<TicketType>(entity =>
        {
            entity.ToTable("TicketTypes", "Ticketing");

            entity.HasIndex(e => new { e.EventId, e.TypeName }, "UQ_TicketTypes_Event_Type").IsUnique();

            entity.Property(e => e.TicketTypeId).HasColumnName("TicketTypeID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_TicketTypes_CreatedAt");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasDefaultValue("USD", "DF_TicketTypes_Currency");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SaleEndDate).HasPrecision(0);
            entity.Property(e => e.SaleStartDate).HasPrecision(0);
            entity.Property(e => e.TypeName).HasMaxLength(50);

            entity.HasOne(d => d.Event).WithMany(p => p.TicketTypes)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK_TicketTypes_Events");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users", "Security");

            entity.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

            entity.HasIndex(e => e.Username, "UQ_Users_Username").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Users_CreatedAt");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Users_IsActive");
            entity.Property(e => e.LastLoginAt).HasPrecision(0);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(64);
            entity.Property(e => e.PasswordSalt).HasMaxLength(32);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId });

            entity.ToTable("UserRoles", "Security");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.AssignedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_UserRoles_AssignedAt");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_UserRoles_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserRoles_Users");
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.ToTable("Venues", "Venue");

            entity.Property(e => e.VenueId).HasColumnName("VenueID");
            entity.Property(e => e.AddressLine1).HasMaxLength(200);
            entity.Property(e => e.AddressLine2).HasMaxLength(200);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.ContactEmail).HasMaxLength(256);
            entity.Property(e => e.ContactPhone).HasMaxLength(20);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Venues_CreatedAt");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Venues_IsActive");
            entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.PostalCode).HasMaxLength(20);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.VenueName).HasMaxLength(200);
        });

        modelBuilder.Entity<VenueHall>(entity =>
        {
            entity.HasKey(e => e.HallId);

            entity.ToTable("VenueHalls", "Venue");

            entity.HasIndex(e => new { e.VenueId, e.HallName }, "UQ_VenueHalls_Venue_Name").IsUnique();

            entity.Property(e => e.HallId).HasColumnName("HallID");
            entity.Property(e => e.HallName).HasMaxLength(100);
            entity.Property(e => e.HasAv)
                .HasDefaultValue(true, "DF_VenueHalls_HasAV")
                .HasColumnName("HasAV");
            entity.Property(e => e.HourlyRate).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.VenueId).HasColumnName("VenueID");

            entity.HasOne(d => d.Venue).WithMany(p => p.VenueHalls)
                .HasForeignKey(d => d.VenueId)
                .HasConstraintName("FK_VenueHalls_Venues");
        });

        modelBuilder.Entity<VwEventSummary>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_EventSummary", "Event");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.EndDate).HasPrecision(0);
            entity.Property(e => e.EventCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.OrganizerName).HasMaxLength(200);
            entity.Property(e => e.StartDate).HasPrecision(0);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Title).HasMaxLength(250);
            entity.Property(e => e.VenueCity).HasMaxLength(100);
            entity.Property(e => e.VenueName).HasMaxLength(200);
        });

        modelBuilder.Entity<VwRegistrationDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_RegistrationDetails", "Ticketing");

            entity.Property(e => e.AttendeeName).HasMaxLength(201);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.EventTitle).HasMaxLength(250);
            entity.Property(e => e.PaidAmount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.PaymentStatus).HasMaxLength(20);
            entity.Property(e => e.RegistrationId).HasColumnName("RegistrationID");
            entity.Property(e => e.RegistrationStatus).HasMaxLength(20);
            entity.Property(e => e.TicketCode)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TicketId).HasColumnName("TicketID");
            entity.Property(e => e.TicketType).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
