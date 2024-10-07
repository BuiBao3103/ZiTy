using zity.DTOs.UserAnswers;
using zity.Models;
using zity.Utilities;

namespace zity.Mappers
{
    public class UserAnswerMapper
    {
        public static UserAnswerDTO ToDTO(UserAnswer userAnswer)
        {
            return new UserAnswerDTO
            {
                Id = userAnswer.Id,
                CreatedAt = userAnswer.CreatedAt,
                UpdatedAt = userAnswer.UpdatedAt,
                AnswerId = userAnswer.AnswerId,
                UserId = userAnswer.UserId,
                // Answer = userAnswer.Answer != null ? AnswerMapper.ToDTO(userAnswer.Answer) : null,
                // User = userAnswer.User != null ? UserMapper.ToDTO(userAnswer.User) : null,
            };
        }

        public static UserAnswer ToModelFromCreate(UserAnswerCreateDTO userAnswerCreateDTO)
        {
            return new UserAnswer
            {
                AnswerId = userAnswerCreateDTO.AnswerId,
                UserId = userAnswerCreateDTO.UserId,
                CreatedAt = DateTime.Now,
            };
        }

        public static UserAnswer UpdateModelFromUpdate(UserAnswer userAnswer, UserAnswerUpdateDTO updateDTO)
        {
            userAnswer.AnswerId = updateDTO.AnswerId;
            userAnswer.UserId = updateDTO.UserId;
            userAnswer.UpdatedAt = DateTime.Now;
            return userAnswer;
        }

        public static UserAnswer PatchModelFromPatch(UserAnswer userAnswer, UserAnswerPatchDTO patchDTO)
        {
            if (patchDTO.AnswerId != null)
                userAnswer.AnswerId = patchDTO.AnswerId;
            if (patchDTO.UserId != null)
                userAnswer.UserId = patchDTO.UserId.Value;
            userAnswer.UpdatedAt = DateTime.Now;
            return userAnswer;
        }
    }
}

