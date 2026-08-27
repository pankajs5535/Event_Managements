using System;
using System.Collections.Generic;

namespace Event_Management.Persistence;

public partial class VwEventSummary
{
    public long EventId { get; set; }

    public string EventCode { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string CategoryName { get; set; } = null!;

    public string OrganizerName { get; set; } = null!;

    public string? VenueName { get; set; }

    public string? VenueCity { get; set; }

    public int? ConfirmedRegistrations { get; set; }

    public int? SessionCount { get; set; }
}
