using System.ComponentModel.DataAnnotations;
namespace zity.DTOs.OtherAnswers
{
    public class OtherAnswerCreateDTO
    {
        [Required]
        public string Content { get; set; } = null!;
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}

