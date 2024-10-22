using System.ComponentModel.DataAnnotations;

namespace zity.DTOs.UserAnswers
{
    public class UserAnswerCreateDTO
    {
        [Required]
        public int AnswerId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
