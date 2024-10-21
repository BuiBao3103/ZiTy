using AutoMapper;
using zity.DTOs.Surveys;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class SurveyService(ISurveyRepository surveyRepository, IMapper mapper) : ISurveyService
    {
        private readonly ISurveyRepository _surveyRepository = surveyRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<PaginatedResult<SurveyDTO>> GetAllAsync(SurveyQueryDTO queryParam)
        {
            var pageSurveys = await _surveyRepository.GetAllAsync(queryParam);
            var surveys = pageSurveys.Contents.Select(_mapper.Map<SurveyDTO>).ToList();

            return new PaginatedResult<SurveyDTO>(
                surveys,
                pageSurveys.TotalItems,
                pageSurveys.Page,
                pageSurveys.PageSize);
        }

        public async Task<SurveyDTO?> GetByIdAsync(int id, string? includes)
        {
            var survey = await _surveyRepository.GetByIdAsync(id, includes);
            return survey != null ? _mapper.Map<SurveyDTO>(survey) : null;
        }

        public async Task<SurveyDTO> CreateAsync(SurveyCreateDTO createDTO)
        {
            var survey = _mapper.Map<Survey>(createDTO);
            var createdSurvey = await _surveyRepository.CreateAsync(survey);
            return _mapper.Map<SurveyDTO>(createdSurvey);
        }

        public async Task<SurveyDTO?> UpdateAsync(int id, SurveyUpdateDTO updateDTO)
        {
            var existingSurvey = await _surveyRepository.GetByIdAsync(id, null);
            if (existingSurvey == null)
            {
                return null;
            }

            _mapper.Map(updateDTO, existingSurvey);
            var updatedSurvey = await _surveyRepository.UpdateAsync(existingSurvey);
            return _mapper.Map<SurveyDTO>(updatedSurvey);
        }

        public async Task<SurveyDTO?> PatchAsync(int id, SurveyPatchDTO patchDTO)
        {
            var existingSurvey = await _surveyRepository.GetByIdAsync(id, null);
            if (existingSurvey == null)
            {
                return null;
            }

            _mapper.Map(patchDTO, existingSurvey);
            var patchedSurvey = await _surveyRepository.UpdateAsync(existingSurvey);
            return _mapper.Map<SurveyDTO>(patchedSurvey);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _surveyRepository.DeleteAsync(id);
        }
    }
}
