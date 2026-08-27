using System;
using System.Collections.Generic;

namespace Event_Management.Persistence;

public partial class TicketType
{
    public long TicketTypeId { get; set; }

    public long EventId { get; set; }

    public string TypeName { get; set; } = null!;

    public decimal Price { get; set; }

    public string Currency { get; set; } = null!;

    public int QuantityAvailable { get; set; }

    public int QuantitySold { get; set; }

    public DateTime? SaleStartDate { get; set; }

    public DateTime? SaleEndDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
