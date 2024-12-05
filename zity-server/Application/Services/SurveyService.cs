using Application.Core.Utilities;
using Application.DTOs;
using Application.DTOs.Surveys;
using Application.Interfaces;
using AutoMapper;
using Domain.Core.Repositories;
using Domain.Core.Specifications;
using Domain.Entities;
using Domain.Exceptions;


namespace Application.Services;

public class SurveyService(IUnitOfWork unitOfWork, IMapper mapper) : ISurveyService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<SurveyDTO>> GetAllAsync(SurveyQueryDTO query)
    {
        var filterExpression = query.BuildFilterCriteria<Survey>(a => a.DeletedAt == null);
        var spec = new BaseSpecification<Survey>(filterExpression);
        var totalCount = await _unitOfWork.Repository<Survey>().CountAsync(spec);
        query.Includes?.Split(',').Select(i => char.ToUpper(i[0]) + i[1..]).ToList().ForEach(spec.AddInclude);
        if (!string.IsNullOrEmpty(query.Sort))
            if (query.Sort.StartsWith("-"))
                spec.ApplyOrderByDescending(query.Sort[1..]);
            else
                spec.ApplyOrderBy(query.Sort);
        spec.ApplyPaging(query.PageSize * (query.Page - 1), query.PageSize);
        var data = await _unitOfWork.Repository<Survey>().ListAsync(spec);
        return new PaginatedResult<SurveyDTO>(
            data.Select(_mapper.Map<SurveyDTO>).ToList(),
            totalCount,
            query.Page,
            query.PageSize);
    }

    public async Task<SurveyDTO> GetByIdAsync(int id, string? includes = null)
    {
        var spec = new BaseSpecification<Survey>(a => a.DeletedAt == null && a.Id == id);
        includes?.Split(',').Select(i => char.ToUpper(i[0]) + i[1..]).ToList().ForEach(spec.AddInclude);
        var survey = await _unitOfWork.Repository<Survey>().FirstOrDefaultAsync(spec)
            ?? throw new EntityNotFoundException(nameof(Survey), id);
        return _mapper.Map<SurveyDTO>(survey);
    }

    public async Task<SurveyDTO> CreateAsync(SurveyCreateDTO createDTO)
    {
        var survey = await _unitOfWork.Repository<Survey>().AddAsync(_mapper.Map<Survey>(createDTO));
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<SurveyDTO>(survey);
    }

    public async Task<SurveyDTO> UpdateAsync(int id, SurveyUpdateDTO updateDTO)
    {
        var existingSurvey = await _unitOfWork.Repository<Survey>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(Survey), id);
        _mapper.Map(updateDTO, existingSurvey);
        _unitOfWork.Repository<Survey>().Update(existingSurvey);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<SurveyDTO>(existingSurvey);
    }

    public async Task<SurveyDTO> PatchAsync(int id, SurveyPatchDTO patchDTO)
    {
        var existingSurvey = await _unitOfWork.Repository<Survey>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(Survey), id);
        _mapper.Map(patchDTO, existingSurvey);
        _unitOfWork.Repository<Survey>().Update(existingSurvey);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<SurveyDTO>(existingSurvey);
    }

    public async Task DeleteAsync(int id)
    {
        var existingSurvey = await _unitOfWork.Repository<Survey>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(Survey), id);
        _unitOfWork.Repository<Survey>().Delete(existingSurvey);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task SubmitAsync(int id, SurveySubmitDTO surveySubmitDTO)
    {

        foreach (var userAnswer in surveySubmitDTO.UserAnswers)
        {
            UserAnswer newUserAnswer = _mapper.Map<UserAnswer>(userAnswer);
            await _unitOfWork.Repository<UserAnswer>().AddAsync(newUserAnswer);
        }
        foreach (var otherAnswer in surveySubmitDTO.OtherAnswers)
        {
            OtherAnswer newOtherAnswer = _mapper.Map<OtherAnswer>(otherAnswer);
            await _unitOfWork.Repository<OtherAnswer>().AddAsync(newOtherAnswer);
        }
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<SurveyDTO> CreateFullSurveyAsync(SurveyCreateFullDTO surveyCreateFullDTO)
    {
        Survey newSurvey = _mapper.Map<Survey>(surveyCreateFullDTO);
        await _unitOfWork.Repository<Survey>().AddAsync(newSurvey);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<SurveyDTO>(newSurvey);
    }

    public async Task<StatisticSurveyDTO> StatisticSurveyAsync(int id)
    {
        var survey = await _unitOfWork.Repository<Survey>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(Survey), id);

        var QuestionStatistics = _mapper.Map<List<QuestionStatisticsDto>>(await _unitOfWork.StatisticRepository.GetAnswerStatisticsAsync(id));
        foreach (var questionStatistic in QuestionStatistics)
        {
            int maxCount = questionStatistic.Answers.Max(a => a.Count);
            if (maxCount == 0) continue;
            questionStatistic.Answers.ForEach(a => a.IsMostSelected = a.Count == maxCount);
        }
        var statisticSurvey = new StatisticSurveyDTO
        {
            Survey = _mapper.Map<SurveyDTO>(survey),
            TotalParticipants = await _unitOfWork.StatisticRepository.GetTotalParticipantsSurveyAsync(id),
            QuestionStatistics = QuestionStatistics

        };
        return statisticSurvey;
    }
}