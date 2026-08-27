using System;
using System.Collections.Generic;

namespace Event_Management.Persistence;

public partial class VwRegistrationDetail
{
    public long RegistrationId { get; set; }

    public long EventId { get; set; }

    public string EventTitle { get; set; } = null!;

    public long UserId { get; set; }

    public string AttendeeName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string RegistrationStatus { get; set; } = null!;

    public bool CheckInStatus { get; set; }

    public long? TicketId { get; set; }

    public string? TicketCode { get; set; }

    public string? TicketType { get; set; }

    public string? PaymentStatus { get; set; }

    public decimal? PaidAmount { get; set; }
}
