using System.ComponentModel.DataAnnotations;

namespace zity.DTOs.OtherAnswers
{
    public class OtherAnswerQueryDTO : BaseQueryDTO
    {
        [RegularExpression(@"^(eq|neq|gt|gte|lt|lte|like|in):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
        public string? Id { get; set; }
        [RegularExpression(@"^(eq|like|in):[^\:]*$", ErrorMessage = "Invalid filter format")]
        public string? Content { get; set; }
        [RegularExpression(@"^(eq):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
        public int? QuestionId { get; set; }
        [RegularExpression(@"^(eq):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
        public int? UserId { get; set; }
    }
}
