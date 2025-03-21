﻿using Report.Domain.Core.Models;

namespace Report.Domain.Entities;
public partial class Report : BaseEntity
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
}
