using System;
using System.Collections.Generic;

namespace PPTify.Infrastructure;

public partial class PresentationHistory
{
    public Guid HistoryId { get; set; }

    public Guid PresentationId { get; set; }

    public string ChangeType { get; set; } = null!;

    public Guid ChangedBy { get; set; }

    public string? ChangeDetails { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Users ChangedByNavigation { get; set; } = null!;

    public virtual Presentations Presentation { get; set; } = null!;
}
