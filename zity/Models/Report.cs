using System;
using System.Collections.Generic;

namespace zity.Models;

public partial class Report
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? RelationshipId { get; set; }

    public virtual RejectionReason? RejectionReason { get; set; }

    public virtual Relationship? Relationship { get; set; }
}
