using System;
using System.Collections.Generic;

namespace Event_Management.Domain.Entities;

public partial class Sponsor
{
    public long SponsorId { get; set; }

    public long OrganizationId { get; set; }

    public long EventId { get; set; }

    public string SponsorshipTier { get; set; } = null!;

    public decimal Amount { get; set; }

    public string Currency { get; set; } = null!;

    public string? LogoUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;
}
