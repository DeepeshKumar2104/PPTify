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

    public virtual Users User { get; set; } = null!;
}
