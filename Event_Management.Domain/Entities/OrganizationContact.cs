using System;
using System.Collections.Generic;

namespace Event_Management.Persistence;

public partial class OrganizationContact
{
    public long ContactId { get; set; }

    public long OrganizationId { get; set; }

    public long UserId { get; set; }

    public string? Designation { get; set; }

    public bool IsPrimary { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
