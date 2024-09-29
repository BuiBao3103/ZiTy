namespace zity.DTOs.Answers
{
    public class AnswerDTO
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public int QuestionId { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
