using zity.DTOs.Questions;
using zity.DTOs.UserAnswers;

namespace zity.DTOs.Answers
{
    public class AnswerDTO
    {
        public int Id { get; set; }

        public string Content { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int QuestionId { get; set; }

        public QuestionDTO Question { get; set; } = null!;

        public virtual ICollection<UserAnswerDTO> UserAnswers { get; set; } = [];
    }
}
