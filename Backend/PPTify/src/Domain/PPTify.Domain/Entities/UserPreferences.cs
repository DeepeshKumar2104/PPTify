using System;
using System.Collections.Generic;

namespace PPTify.Infrastructure;

public partial class UserPreferences
{
    public Guid PreferenceId { get; set; }

    public Guid UserId { get; set; }

    public string? Theme { get; set; }

    public string? Language { get; set; }

    public virtual UserCredentials User { get; set; } = null!;
}
