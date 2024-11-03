

using Application.DTOs.Surveys;

namespace Application.DTOs;

public interface ISurveyService
{
    Task<PaginatedResult<SurveyDTO>> GetAllAsync(SurveyQueryDTO query);
    Task<SurveyDTO> GetByIdAsync(int id, string? includes = null);
    Task<SurveyDTO> CreateAsync(SurveyCreateDTO surveyCreateDTO);
    Task<SurveyDTO> UpdateAsync(int id, SurveyUpdateDTO surveyUpdateDTO);
    Task<SurveyDTO> PatchAsync(int id, SurveyPatchDTO surveyPatchDTO);
    Task DeleteAsync(int id);
}
