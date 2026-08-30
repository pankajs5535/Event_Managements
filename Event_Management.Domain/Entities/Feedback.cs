using System;
using System.Collections.Generic;

namespace Event_Management.Domain.Entities;

public partial class Feedback
{
    public long FeedbackId { get; set; }

    public long EventId { get; set; }

    public long? SessionId { get; set; }

    public long UserId { get; set; }

    public byte Rating { get; set; }

    public string? Comments { get; set; }

    public DateTime SubmittedAt { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual EventSession? Session { get; set; }

    public virtual User User { get; set; } = null!;
}
