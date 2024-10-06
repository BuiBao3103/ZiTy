using zity.DTOs.Surveys;
using zity.Models;

namespace zity.Mappers
{
    public class SurveyMapper
    {
        // ToDTO
        public static SurveyDTO ToDTO(Survey survey) =>
            new SurveyDTO
            {
                Id = survey.Id,
                Title = survey.Title,
                StartDate = survey.StartDate,
                EndDate = survey.EndDate,
                TotalQuestions = survey.TotalQuestions,
                UserCreateId = survey.UserCreateId,
                CreatedAt = survey.CreatedAt,
                UpdatedAt = survey.UpdatedAt
            };

        // To Model From Create
        public static Survey ToModelFromCreate(SurveyCreateDTO surveyCreateDTO) =>
            new Survey
            {
                Title = surveyCreateDTO.Title,
                StartDate = surveyCreateDTO.StartDate,
                EndDate = surveyCreateDTO.EndDate,
                TotalQuestions = surveyCreateDTO.TotalQuestions,
                UserCreateId = surveyCreateDTO.UserCreateId,
                CreatedAt = DateTime.Now,
            };

        // UpdateModelFromUpdate
        public static Survey UpdateModelFromUpdate(Survey survey, SurveyUpdateDTO surveyUpdateDTO)
        {
            survey.Title = surveyUpdateDTO.Title;
            survey.EndDate = surveyUpdateDTO.EndDate;
            survey.TotalQuestions = surveyUpdateDTO.TotalQuestions;
            survey.UserCreateId = surveyUpdateDTO.UserCreateId;
            survey.UpdatedAt = DateTime.Now;
            return survey;
        }

        // PatchModelFromPatch
        public static Survey PatchModelFromPatch(Survey survey, SurveyPatchDTO surveyPatchDTO)
        {
            if (surveyPatchDTO.Title != null)
            {
                survey.Title = surveyPatchDTO.Title;
            }

            if (surveyPatchDTO.EndDate != null)
            {
                survey.EndDate = surveyPatchDTO.EndDate.Value;
            }

            if (surveyPatchDTO.TotalQuestions != null)
            {
                survey.TotalQuestions = surveyPatchDTO.TotalQuestions.Value;
            }

            if (surveyPatchDTO.UserCreateId != null)
            {
                survey.UserCreateId = surveyPatchDTO.UserCreateId.Value;
            }

            survey.UpdatedAt = DateTime.Now;

            return survey;
        }
    }
}
