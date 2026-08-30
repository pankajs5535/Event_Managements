using System;
using System.Collections.Generic;

namespace Event_Management.Domain.Entities;

public partial class UserRole
{
    public long UserId { get; set; }

    public int RoleId { get; set; }

    public DateTime AssignedAt { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
