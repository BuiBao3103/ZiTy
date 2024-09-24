using System;
using System.Collections.Generic;

namespace ZiTy.Models;

public partial class UserAnswer
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? AnswerId { get; set; }

    public int? UserId { get; set; }

    public virtual Answer? Answer { get; set; }

    public virtual User? User { get; set; }
}
