using System;
using System.Collections.Generic;

namespace PPTify.Infrastructure;

public partial class AuthTokens
{
    public Guid TokenId { get; set; }

    public Guid UserId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public bool? Revoked { get; set; }

    public virtual Users User { get; set; } = null!;
}
