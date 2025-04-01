using System;
using System.Collections.Generic;

namespace PPTify.Infrastructure;

public partial class Presentations
{
    public Guid PresentationId { get; set; }

    public Guid UserId { get; set; }

    public string FileUrl { get; set; } = null!;

    public string? Status { get; set; }

    public int? Version { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<PresentationHistory> PresentationHistory { get; set; } = new List<PresentationHistory>();

    public virtual ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();

    public virtual UserCredentials User { get; set; } = null!;
}
