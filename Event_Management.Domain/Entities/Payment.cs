using System;
using System.Collections.Generic;

namespace Event_Management.Persistence;

public partial class Payment
{
    public long PaymentId { get; set; }

    public long RegistrationId { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; } = null!;

    public string PaymentMethod { get; set; } = null!;

    public string PaymentStatus { get; set; } = null!;

    public string? TransactionRef { get; set; }

    public DateTime? PaidAt { get; set; }

    public string? GatewayResponse { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Registration Registration { get; set; } = null!;
}
