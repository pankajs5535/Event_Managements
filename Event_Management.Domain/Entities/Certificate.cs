using System;
using System.Collections.Generic;

namespace Event_Management.Domain.Entities;

public partial class Certificate
{
    public long CertificateId { get; set; }

    public long RegistrationId { get; set; }

    public string CertificateType { get; set; } = null!;

    public DateTime IssuedAt { get; set; }

    public string? CertificateUrl { get; set; }

    public string VerificationCode { get; set; } = null!;

    public virtual Registration Registration { get; set; } = null!;
}
