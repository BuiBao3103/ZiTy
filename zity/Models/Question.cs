using System;
using System.Collections.Generic;

namespace zity.Models;

public partial class Question
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? SurveyId { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual ICollection<OtherAnswer> OtherAnswers { get; set; } = new List<OtherAnswer>();

    public virtual Survey? Survey { get; set; }
}
