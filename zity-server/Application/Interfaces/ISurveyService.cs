using Application.DTOs;
using Application.DTOs.Surveys;

namespace Application.Interfaces;

public interface ISurveyService
{
    Task<PaginatedResult<SurveyDTO>> GetAllAsync(SurveyQueryDTO query);
    Task<SurveyDTO> GetByIdAsync(int id, string? includes = null);
    Task<SurveyDTO> CreateAsync(SurveyCreateDTO surveyCreateDTO);
    Task<SurveyDTO> UpdateAsync(int id, SurveyUpdateDTO surveyUpdateDTO);
    Task<SurveyDTO> PatchAsync(int id, SurveyPatchDTO surveyPatchDTO);
    Task DeleteAsync(int id);
    Task SubmitAsync(int id, SurveySubmitDTO surveySubmitDTO);
    Task<SurveyDTO> CreateFullSurveyAsync(SurveyCreateFullDTO surveyCreateFullDTO);
    Task<StatisticSurveyDTO> StatisticSurveyAsync(int id);
}
