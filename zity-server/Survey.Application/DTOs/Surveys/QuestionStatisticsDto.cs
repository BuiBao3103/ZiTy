

namespace Survey.Application.DTOs.Surveys;

public class QuestionStatisticsDto
{
    public int QuestionId { get; set; }
    public string QuestionContent { get; set; }
    public List<AnswerDetailDto> Answers { get; set; }
    public List<OtherAnswerDetailDto> OtherAnswers { get; set; }
}

public class AnswerDetailDto
{
    public int AnswerId { get; set; }
    public string Content { get; set; }
    public int Count { get; set; }
    public bool IsMostSelected { get; set; }
}

public class OtherAnswerDetailDto
{
    public int Id { get; set; }
    public string Content { get; set; }
}