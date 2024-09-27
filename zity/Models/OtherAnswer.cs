using System;
using System.Collections.Generic;

namespace zity.Models;

public partial class OtherAnswer
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? QuestionId { get; set; }

    public int? UserId { get; set; }

    public virtual Question? Question { get; set; }

    public virtual User? User { get; set; }
}
