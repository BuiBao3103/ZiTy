using zity.DTOs.Answers;
using zity.Models;
using zity.Utilities;

namespace zity.Mappers
{
    public class AnswerMapper
    {
        public static AnswerDTO ToDTO(Answer answer)
        {
            return new AnswerDTO
            {
                Id = answer.Id,
                CreatedAt = answer.CreatedAt,
                UpdatedAt = answer.UpdatedAt,
                Content = answer.Content,
                QuestionId = answer.QuestionId,
                // Question = answer.Question != null ? QuestionMapper.ToDTO(answer.Question) : null,
                // UserAnswers = answer.UserAnswers.Select(userAnswer => UserAnswerMapper.ToDTO(userAnswer)).ToList(),
            };
        }

        public static Answer ToModelFromCreate(AnswerCreateDTO answerCreateDTO)
        {
            return new Answer
            {
                Content = answerCreateDTO.Content,
                QuestionId = answerCreateDTO.QuestionId,
                CreatedAt = DateTime.Now,
            };
        }

        public static Answer UpdateModelFromUpdate(Answer answer, AnswerUpdateDTO updateDTO)
        {
            answer.Content = updateDTO.Content;
            answer.QuestionId = updateDTO.QuestionId;
            answer.UpdatedAt = DateTime.Now;
            return answer;
        }

        public static Answer PatchModelFromPatch(Answer answer, AnswerPatchDTO patchDTO)
        {
            if (patchDTO.Content != null)
                answer.Content = patchDTO.Content;
            if (patchDTO.QuestionId != null)
                answer.QuestionId = patchDTO.QuestionId.Value;
            answer.UpdatedAt = DateTime.Now;
            return answer;
        }
    }
}

