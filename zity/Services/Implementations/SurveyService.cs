using zity.DTOs.Surveys;
using zity.Mappers;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class SurveyService(ISurveyRepository surveyRepository) : ISurveyService
    {
        private readonly ISurveyRepository _surveyRepository = surveyRepository;

        public async Task<PaginatedResult<SurveyDTO>> GetAllAsync(SurveyQueryDTO queryParam)
        {
            var pageSurveys = await _surveyRepository.GetAllAsync(queryParam);
            return new PaginatedResult<SurveyDTO>(
                pageSurveys.Contents.Select(SurveyMapper.ToDTO).ToList(),
                pageSurveys.TotalItems,
                pageSurveys.Page,
                pageSurveys.PageSize);
        }


        public async Task<SurveyDTO?> GetByIdAsync(int id, string? includes)
        {
            var survey = await _surveyRepository.GetByIdAsync(id, includes);
            return survey != null ? SurveyMapper.ToDTO(survey) : null;
        }
        public async Task<SurveyDTO> CreateAsync(SurveyCreateDTO createDTO)
        {
            var survey = SurveyMapper.ToModelFromCreate(createDTO);
            return SurveyMapper.ToDTO(await _surveyRepository.CreateAsync(survey));
        }
        public async Task<SurveyDTO?> UpdateAsync(int id, SurveyUpdateDTO updateDTO)
        {
            var existingSurvey = await _surveyRepository.GetByIdAsync(id, null);
            if (existingSurvey == null)
            {
                return null;
            }

            SurveyMapper.UpdateModelFromUpdate(existingSurvey, updateDTO);
            var updatedSurvey = await _surveyRepository.UpdateAsync(existingSurvey);
            return SurveyMapper.ToDTO(updatedSurvey);
        }

        public async Task<SurveyDTO?> PatchAsync(int id, SurveyPatchDTO patchDTO)
        {
            var existingSurvey = await _surveyRepository.GetByIdAsync(id, null);
            if (existingSurvey == null)
            {
                return null;
            }

            SurveyMapper.PatchModelFromPatch(existingSurvey, patchDTO);
            var patchedSurvey = await _surveyRepository.UpdateAsync(existingSurvey);
            return SurveyMapper.ToDTO(patchedSurvey);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _surveyRepository.DeleteAsync(id);
        }

    }
}
