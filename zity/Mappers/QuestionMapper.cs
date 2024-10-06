using zity.DTOs.Questions;
using zity.Models;
using zity.Utilities;

namespace zity.Mappers
{
    public class QuestionMapper
    {
        public static QuestionDTO ToDTO(Question question)
        {
            return new QuestionDTO
            {
                Id = question.Id,
                CreatedAt = question.CreatedAt,
                UpdatedAt = question.UpdatedAt,
                Content = question.Content,
                SurveyId = question.SurveyId,
                // Answers = question.Answers.Select(answer => AnswerMapper.ToDTO(answer)).ToList(),
                // OtherAnswers = question.OtherAnswers.Select(otherAnswer => OtherAnswerMapper.ToDTO(otherAnswer)).ToList(),
                // Survey = question.Survey != null ? SurveyMapper.ToDTO(question.Survey) : null,
            };
        }

        public static Question ToModelFromCreate(QuestionCreateDTO questionCreateDTO)
        {
            return new Question
            {
                Content = questionCreateDTO.Content,
                SurveyId = questionCreateDTO.SurveyId,
                CreatedAt = DateTime.Now,
            };
        }

        public static Question UpdateModelFromUpdate(Question question, QuestionUpdateDTO updateDTO)
        {
            question.Content = updateDTO.Content;
            question.SurveyId = updateDTO.SurveyId;
            question.UpdatedAt = DateTime.Now;
            return question;
        }

        public static Question PatchModelFromPatch(Question question, QuestionPatchDTO patchDTO)
        {
            if (patchDTO.Content != null)
                question.Content = patchDTO.Content;
            if (patchDTO.SurveyId != null)
                question.SurveyId = patchDTO.SurveyId.Value;
            question.UpdatedAt = DateTime.Now;
            return question;
        }
    }
}
