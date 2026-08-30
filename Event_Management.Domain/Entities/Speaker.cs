using System;
using System.Collections.Generic;

namespace Event_Management.Domain.Entities;

public partial class Speaker
{
    public long SpeakerId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Bio { get; set; }

    public string? OrganizationName { get; set; }

    public string? Designation { get; set; }

    public string? ProfileImageUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<SessionSpeaker> SessionSpeakers { get; set; } = new List<SessionSpeaker>();
}
