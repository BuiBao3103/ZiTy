

namespace Survey.Domain.Core.Models;

public class QuestionStatistics
{
    public int QuestionId { get; set; }
    public string QuestionContent { get; set; }
    public ICollection<AnswerStatistics> Answers { get; set; }
    public ICollection<OtherAnswerStatistics> OtherAnswers { get; set; }
}

public class AnswerStatistics
{
    public int AnswerId { get; set; }
    public string Content { get; set; }
    public int Count { get; set; }
}

public class OtherAnswerStatistics
{
    public int Id { get; set; }
    public string Content { get; set; }
}
