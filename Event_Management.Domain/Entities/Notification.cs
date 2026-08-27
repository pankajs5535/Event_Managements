using System;
using System.Collections.Generic;

namespace Event_Management.Persistence;

public partial class Notification
{
    public long NotificationId { get; set; }

    public long UserId { get; set; }

    public long? EventId { get; set; }

    public string Title { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string NotificationType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? SentAt { get; set; }

    public virtual Event? Event { get; set; }

    public virtual User User { get; set; } = null!;
}
