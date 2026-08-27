using System;
using System.Collections.Generic;

namespace Event_Management.Persistence;

public partial class Exhibitor
{
    public long ExhibitorId { get; set; }

    public long OrganizationId { get; set; }

    public long EventId { get; set; }

    public string BoothNumber { get; set; } = null!;

    public int? BoothSizeSqFt { get; set; }

    public long? ContactPersonId { get; set; }

    public decimal Amount { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User? ContactPerson { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;
}
