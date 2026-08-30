using System;
using System.Collections.Generic;

namespace Event_Management.Domain.Entities;

public partial class Organization
{
    public long OrganizationId { get; set; }

    public string OrgName { get; set; } = null!;

    public string OrgType { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Website { get; set; }

    public string? AddressLine1 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? PostalCode { get; set; }

    public bool IsActive { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Exhibitor> Exhibitors { get; set; } = new List<Exhibitor>();

    public virtual ICollection<OrganizationContact> OrganizationContacts { get; set; } = new List<OrganizationContact>();

    public virtual ICollection<Sponsor> Sponsors { get; set; } = new List<Sponsor>();
}
