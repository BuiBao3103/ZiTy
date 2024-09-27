using System;
using System.Collections.Generic;

namespace ZiTy.Models;

public partial class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Avatar { get; set; }
    public bool? IsFirstLogin { get; set; }
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string NationId { get; set; } = null!;
    public string UserType { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public bool? IsStaying { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
    public virtual ICollection<OtherAnswer> OtherAnswers { get; set; } = new List<OtherAnswer>();
    public virtual ICollection<Relationship> Relationships { get; set; } = new List<Relationship>();
    public virtual ICollection<Survey> Surveys { get; set; } = new List<Survey>();
    public virtual ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
}
