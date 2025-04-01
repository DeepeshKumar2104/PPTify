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

    public DateTime? LastLoginAt { get; set; }

    public bool? IsEmailVerified { get; set; }

    public virtual UserCredentials User { get; set; } = null!;
}
