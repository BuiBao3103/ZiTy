using Application.DTOs;
using Application.DTOs.Surveys;
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
        var spec = new BaseSpecification<Survey>(a => a.DeletedAt == null);
        var totalCount = await _unitOfWork.Repository<Survey>().CountAsync(spec);
        query.Includes?.Split(',').ToList().ForEach(spec.AddInclude);
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
        includes?.Split(',').ToList().ForEach(spec.AddInclude);
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
        User user = await _unitOfWork.Repository<User>().GetByIdAsync(surveySubmitDTO.UserId)
            ?? throw new EntityNotFoundException(nameof(User), surveySubmitDTO.UserId);

        foreach (var answer in surveySubmitDTO.AnswerIds)
        {
            UserAnswer userAnswer = new UserAnswer
            {
                UserId = user.Id,
                AnswerId = answer,
                CreatedAt = DateTime.Now
            };
            await _unitOfWork.Repository<UserAnswer>().AddAsync(userAnswer);
        }
        foreach (var question in surveySubmitDTO.OtherAnswers)
        {
            OtherAnswer otherAnswer = new OtherAnswer
            {
                UserId = user.Id,
                QuestionId = question.QuestionId,
                Content = question.Content,
                CreatedAt = DateTime.Now
            };
            await _unitOfWork.Repository<OtherAnswer>().AddAsync(otherAnswer);
        }
        await _unitOfWork.SaveChangesAsync();
    }
}