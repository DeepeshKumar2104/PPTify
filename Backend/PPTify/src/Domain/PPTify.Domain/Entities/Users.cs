using System;
using System.Collections.Generic;

namespace PPTify.Infrastructure;

public partial class Users
{
    public Guid UserId { get; set; }

    public string Email { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? Role { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsEmailVerified { get; set; }

    public string UserUniqueId { get; set; } = null!;

    public virtual ICollection<AuditLogs> AuditLogs { get; set; } = new List<AuditLogs>();

    public virtual ICollection<AuthTokens> AuthTokens { get; set; } = new List<AuthTokens>();

    public virtual ICollection<PresentationHistory> PresentationHistory { get; set; } = new List<PresentationHistory>();

    public virtual ICollection<Presentations> Presentations { get; set; } = new List<Presentations>();

    public virtual UserCredentials? UserCredentials { get; set; }

    public virtual ICollection<UserPreferences> UserPreferences { get; set; } = new List<UserPreferences>();
}
