using System.ComponentModel.DataAnnotations;

namespace zity.DTOs.Answers
{
    public class AnswerUpdateDTO
    {
        [Required]
        public string Content { get; set; } = null!;
        [Required]
        public int QuestionId { get; set; }
    }
}
