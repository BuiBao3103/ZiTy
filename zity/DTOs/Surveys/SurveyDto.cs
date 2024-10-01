using zity.DTOs.Questions;
using zity.DTOs.Users;

namespace zity.DTOs.Surveys
{
    public class SurveyDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int TotalQuestions { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UserCreateId { get; set; }

        public ICollection<QuestionDTO> Questions { get; set; } = [];

        public virtual UserDTO? UserCreate { get; set; }
    }
}
