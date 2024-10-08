using zity.DTOs.Questions;
using zity.DTOs.Users;
using zity.Models;

namespace zity.DTOs.OtherAnswers
{
    public class OtherAnswerDTO
    {
        public int Id { get; set; }

        public string Content { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? QuestionId { get; set; }

        public int? UserId { get; set; }

        public QuestionDTO? Question { get; set; }

        public UserDTO? User { get; set; }
    }
}
