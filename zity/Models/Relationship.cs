using System;
using System.Collections.Generic;

namespace zity.Models;

public partial class Relationship
{
    public int Id { get; set; }

    public string Role { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int UserId { get; set; }

    public string ApartmentId { get; set; } = null!;

    public virtual Apartment Apartment { get; set; } = null!;

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual User User { get; set; } = null!;
}
