using System;
using System.Collections.Generic;

namespace Event_Management.Persistence;

public partial class AuditLog
{
    public long AuditId { get; set; }

    public string TableName { get; set; } = null!;

    public string RecordId { get; set; } = null!;

    public string Action { get; set; } = null!;

    public long? ChangedBy { get; set; }

    public DateTime ChangedAt { get; set; }

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public string? ApplicationName { get; set; }

    public string? HostName { get; set; }

    public virtual User? ChangedByNavigation { get; set; }
}
