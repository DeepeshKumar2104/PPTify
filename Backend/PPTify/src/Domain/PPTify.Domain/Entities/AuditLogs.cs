using System;
using System.Collections.Generic;

namespace PPTify.Infrastructure;

public partial class AuditLogs
{
    public Guid LogId { get; set; }

    public Guid UserId { get; set; }

    public string ActionType { get; set; } = null!;

    public string? ActionDetails { get; set; }

    public string? LogLevel { get; set; }

    public string? Source { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual UserCredentials User { get; set; } = null!;
}
