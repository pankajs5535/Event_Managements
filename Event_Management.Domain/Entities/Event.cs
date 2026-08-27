using System;
using System.Collections.Generic;

namespace Event_Management.Persistence;

public partial class Event
{
    public long EventId { get; set; }

    public string EventCode { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int CategoryId { get; set; }

    public long OrganizationId { get; set; }

    public long? VenueId { get; set; }

    public bool IsVirtual { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string TimeZone { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int? MaxAttendees { get; set; }

    public bool IsPublic { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual EventCategory Category { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<EventSession> EventSessions { get; set; } = new List<EventSession>();

    public virtual ICollection<Exhibitor> Exhibitors { get; set; } = new List<Exhibitor>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    public virtual ICollection<Sponsor> Sponsors { get; set; } = new List<Sponsor>();

    public virtual ICollection<TicketType> TicketTypes { get; set; } = new List<TicketType>();

    public virtual Venue? Venue { get; set; }
}
