using zity.DTOs.Answers;
using zity.Models;
using zity.Utilities;

namespace zity.Mappers
{
    public class AnswerMapper
    {
        public static AnswerDTO ToDTO(Answer question)
        {
            return new AnswerDTO
            {
                Id = question.Id,
                CreatedAt = question.CreatedAt,
                UpdatedAt = question.UpdatedAt,
                Content = question.Content,
                QuestionId = question.QuestionId,
                // Question = question.Question != null ? QuestionMapper.ToDTO(question.Question) : null,
                // UserAnswers = question.UserAnswers.Select(userAnswer => UserAnswerMapper.ToDTO(userAnswer)).ToList(),
            };
        }

        public static Answer ToModelFromCreate(AnswerCreateDTO questionCreateDTO)
        {
            return new Answer
            {
                Content = questionCreateDTO.Content,
                QuestionId = questionCreateDTO.QuestionId,
                CreatedAt = DateTime.Now,
            };
        }

        public static Answer UpdateModelFromUpdate(Answer question, AnswerUpdateDTO updateDTO)
        {
            question.Content = updateDTO.Content;
            question.QuestionId = updateDTO.QuestionId;
            question.UpdatedAt = DateTime.Now;
            return question;
        }

        public static Answer PatchModelFromPatch(Answer question, AnswerPatchDTO patchDTO)
        {
            if (patchDTO.Content != null)
                question.Content = patchDTO.Content;
            if (patchDTO.QuestionId != null)
                question.QuestionId = patchDTO.QuestionId.Value;
            question.UpdatedAt = DateTime.Now;
            return question;
        }
    }
}

