using System;
using System.Collections.Generic;

namespace Event_Management.Persistence;

public partial class SessionSpeaker
{
    public long SessionId { get; set; }

    public long SpeakerId { get; set; }

    public string SpeakerRole { get; set; } = null!;

    public virtual EventSession Session { get; set; } = null!;

    public virtual Speaker Speaker { get; set; } = null!;
}
