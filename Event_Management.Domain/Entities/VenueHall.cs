using System;
using System.Collections.Generic;

namespace Event_Management.Domain.Entities;

public partial class VenueHall
{
    public long HallId { get; set; }

    public long VenueId { get; set; }

    public string HallName { get; set; } = null!;

    public int Capacity { get; set; }

    public int? FloorNumber { get; set; }

    public bool HasAv { get; set; }

    public decimal? HourlyRate { get; set; }

    public virtual ICollection<EventSession> EventSessions { get; set; } = new List<EventSession>();

    public virtual Venue Venue { get; set; } = null!;
}
