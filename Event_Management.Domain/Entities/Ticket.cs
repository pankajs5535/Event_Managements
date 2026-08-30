using System;
using System.Collections.Generic;

namespace Event_Management.Domain.Entities;

public partial class Ticket
{
    public long TicketId { get; set; }

    public long RegistrationId { get; set; }

    public long TicketTypeId { get; set; }

    public string TicketCode { get; set; } = null!;

    public DateTime IssuedAt { get; set; }

    public string? QrcodeUrl { get; set; }

    public string Status { get; set; } = null!;

    public virtual Registration Registration { get; set; } = null!;

    public virtual TicketType TicketType { get; set; } = null!;
}
