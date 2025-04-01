using System;
using System.Collections.Generic;

namespace PPTify.Infrastructure;

public partial class UserCredentials
{
    public Guid UserId { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string? TwoFactorSecret { get; set; }

    public string? PasswordResetToken { get; set; }

    public DateTime? ResetTokenExpiry { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<AuditLogs> AuditLogs { get; set; } = new List<AuditLogs>();

    public virtual ICollection<AuthTokens> AuthTokens { get; set; } = new List<AuthTokens>();

    public virtual ICollection<PresentationHistory> PresentationHistory { get; set; } = new List<PresentationHistory>();

    public virtual ICollection<Presentations> Presentations { get; set; } = new List<Presentations>();

    public virtual ICollection<UserPreferences> UserPreferences { get; set; } = new List<UserPreferences>();

    public virtual Users? Users { get; set; }
}
