using Application.DTOs;
using Application.DTOs.Questions;
using Application.Interfaces;
using AutoMapper;
using Domain.Core.Repositories;
using Domain.Core.Specifications;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services;

public class QuestionService(IUnitOfWork unitOfWork, IMapper mapper) : IQuestionService
{
    readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<QuestionDTO>> GetAllAsync(QuestionQueryDTO query)
    {
        var spec = new BaseSpecification<Question>(a => a.DeletedAt == null);
        var totalCount = await _unitOfWork.Repository<Question>().CountAsync(spec);
        query.Includes?.Split(',').ToList().ForEach(spec.AddInclude);
        if (!string.IsNullOrEmpty(query.Sort))
            if (query.Sort.StartsWith("-"))
                spec.ApplyOrderByDescending(query.Sort[1..]);
            else
                spec.ApplyOrderBy(query.Sort);
        spec.ApplyPaging(query.PageSize * (query.Page - 1), query.PageSize);
        var data = await _unitOfWork.Repository<Question>().ListAsync(spec);
        return new PaginatedResult<QuestionDTO>(
            data.Select(_mapper.Map<QuestionDTO>).ToList(),
            totalCount,
            query.Page,
            query.PageSize);
    }


    public async Task<QuestionDTO> GetByIdAsync(int id, string? includes = null)
    {
        var question = await _unitOfWork.Repository<Question>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(Question), id);
        return _mapper.Map<QuestionDTO>(question);
    }
    public async Task<QuestionDTO> CreateAsync(QuestionCreateDTO createDTO)
    {
        var question = await _unitOfWork.Repository<Question>().AddAsync(_mapper.Map<Question>(createDTO));
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<QuestionDTO>(question);
    }
    public async Task<QuestionDTO> UpdateAsync(int id, QuestionUpdateDTO updateDTO)
    {
        var existingQuestion = await _unitOfWork.Repository<Question>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(Question), id);
        _mapper.Map(updateDTO, existingQuestion);
        _unitOfWork.Repository<Question>().Update(existingQuestion);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<QuestionDTO>(existingQuestion);
    }

    public async Task<QuestionDTO> PatchAsync(int id, QuestionPatchDTO patchDTO)
    {
        var existingQuestion = await _unitOfWork.Repository<Question>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(Question), id);
        _mapper.Map(patchDTO, existingQuestion);
        _unitOfWork.Repository<Question>().Update(existingQuestion);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<QuestionDTO>(existingQuestion);
    }

    public async Task DeleteAsync(int id)
    {
        var existingQuestion = await _unitOfWork.Repository<Question>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(Question), id);
        _unitOfWork.Repository<Question>().Delete(existingQuestion);
        await _unitOfWork.SaveChangesAsync();
    }
}