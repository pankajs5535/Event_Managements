using System;
using System.Collections.Generic;

namespace Event_Management.Domain.Entities;

public partial class EventSession
{
    public long SessionId { get; set; }

    public long EventId { get; set; }

    public long? HallId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string SessionType { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int? MaxSeats { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual VenueHall? Hall { get; set; }

    public virtual ICollection<SessionAttendance> SessionAttendances { get; set; } = new List<SessionAttendance>();

    public virtual ICollection<SessionSpeaker> SessionSpeakers { get; set; } = new List<SessionSpeaker>();
}
