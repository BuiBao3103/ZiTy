using zity.DTOs.Surveys;
using zity.Utilities;

namespace zity.Services.Interfaces
{
    public interface ISurveyService
    {
        Task<PaginatedResult<SurveyDTO>> GetAllAsync(SurveyQueryDTO query);
        Task<SurveyDTO> GetByIdAsync(int id, string? includes);
        Task<SurveyDTO> CreateAsync(SurveyCreateDTO surveyCreateDTO);
        Task<SurveyDTO> UpdateAsync(int id, SurveyUpdateDTO surveyUpdateDTO);
        Task<SurveyDTO> PatchAsync(int id, SurveyPatchDTO surveyPatchDTO);
        Task DeleteAsync(int id);
    }
}
