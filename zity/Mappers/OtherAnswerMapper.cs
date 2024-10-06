using zity.DTOs.OtherAnswers;
using zity.Models;
using zity.Utilities;

namespace zity.Mappers
{
    public class OtherAnswerMapper
    {
        public static OtherAnswerDTO ToDTO(OtherAnswer otherAnswer)
        {
            return new OtherAnswerDTO
            {
                Id = otherAnswer.Id,
                CreatedAt = otherAnswer.CreatedAt,
                UpdatedAt = otherAnswer.UpdatedAt,
                Content = otherAnswer.Content,
                QuestionId = otherAnswer.QuestionId,
                UserId = otherAnswer.UserId,
                // Question = otherAnswer.Question != null ? QuestionMapper.ToDTO(otherAnswer.Question) : null,
                // User = otherAnswer.User != null ? UserMapper.ToDTO(otherAnswer.User) : null,
            };
        }

        public static OtherAnswer ToModelFromCreate(OtherAnswerCreateDTO otherAnswerCreateDTO)
        {
            return new OtherAnswer
            {
                Content = otherAnswerCreateDTO.Content,
                QuestionId = otherAnswerCreateDTO.QuestionId,
                CreatedAt = DateTime.Now,
                UserId = otherAnswerCreateDTO.UserId,
            };
        }

        public static OtherAnswer UpdateModelFromUpdate(OtherAnswer otherAnswer, OtherAnswerUpdateDTO updateDTO)
        {
            otherAnswer.Content = updateDTO.Content;
            otherAnswer.QuestionId = updateDTO.QuestionId;
            otherAnswer.UpdatedAt = DateTime.Now;
            otherAnswer.UserId = updateDTO.UserId;
            return otherAnswer;
        }

        public static OtherAnswer PatchModelFromPatch(OtherAnswer otherAnswer, OtherAnswerPatchDTO patchDTO)
        {
            if (patchDTO.Content != null)
                otherAnswer.Content = patchDTO.Content;
            if (patchDTO.QuestionId != null)
                otherAnswer.QuestionId = patchDTO.QuestionId.Value;
            if (patchDTO.UserId != null)
                otherAnswer.UserId = patchDTO.UserId.Value;
            otherAnswer.UpdatedAt = DateTime.Now;
            return otherAnswer;
        }
    }
}

