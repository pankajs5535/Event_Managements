using System;
using System.Collections.Generic;

namespace Event_Management.Domain.Entities;

public partial class SessionAttendance
{
    public long AttendanceId { get; set; }

    public long SessionId { get; set; }

    public long RegistrationId { get; set; }

    public DateTime CheckInTime { get; set; }

    public virtual Registration Registration { get; set; } = null!;

    public virtual EventSession Session { get; set; } = null!;
}
