using System;
using System.Collections.Generic;

namespace zity.Models;

public partial class BillDetail
{
    public int Id { get; set; }

    public float Price { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int BillId { get; set; }

    public int ServiceId { get; set; }

    public virtual Bill Bill { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
