using System.ComponentModel.DataAnnotations;

namespace Survey.Application.DTOs.Surveys
{
    public class SurveyCreateFullDTO
    {
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        [Required]
        public int? TotalQuestions { get; set; }
        [Required]
        public int? UserCreateId { get; set; }
        [Required]

        public List<Question> Questions { get; set; }
        public class Question
        {
            [Required]
            public string Content { get; set; } = null!;
            public List<Answer> Answers { get; set; }

        }
        public class Answer
        {
            [Required]
            public string Content { get; set; } = null!;
        }
    }
}

