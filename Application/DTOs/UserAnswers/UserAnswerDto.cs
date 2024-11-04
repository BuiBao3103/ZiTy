
using Application.DTOs.Answers;
using Application.DTOs.Users;

namespace Application.DTOs.UserAnswers;

public class UserAnswerDTO
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? AnswerId { get; set; }

    public int? UserId { get; set; }

    public AnswerDTO? Answer { get; set; }

    public UserDTO? User { get; set; }
}
