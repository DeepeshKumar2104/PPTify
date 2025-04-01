using System;
using System.Collections.Generic;

namespace PPTify.Infrastructure;

public partial class Tasks
{
    public Guid TaskId { get; set; }

    public Guid PresentationId { get; set; }

    public string? TaskStatus { get; set; }

    public string? Result { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public int? Duration { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Presentations Presentation { get; set; } = null!;
}
