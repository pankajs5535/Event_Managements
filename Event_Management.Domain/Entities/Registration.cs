using System;
using System.Collections.Generic;

namespace Event_Management.Domain.Entities;

public partial class Registration
{
    public long RegistrationId { get; set; }

    public long EventId { get; set; }

    public long UserId { get; set; }

    public DateTime RegistrationDate { get; set; }

    public string Status { get; set; } = null!;

    public bool CheckInStatus { get; set; }

    public DateTime? CheckInTime { get; set; }

    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<SessionAttendance> SessionAttendances { get; set; } = new List<SessionAttendance>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual User User { get; set; } = null!;
}
