using AutoMapper;
using Survey.Domain.Entities;
using Survey.Domain.Core.Repositories;
using Survey.Domain.Core.Specifications;
using Survey.Domain.Exceptions;
using System.Linq.Expressions;
using Survey.Application.DTOs.Answers;
using Survey.Application.DTOs;
using Survey.Application.Core.Utilities;
using Survey.Application.Interfaces;


namespace Survey.Application.Services;

public class AnswerService(IUnitOfWork unitOfWork, IMapper mapper) : IAnswerService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<AnswerDTO>> GetAllAsync(AnswerQueryDTO query)
    {
        var filterExpression = query.BuildFilterCriteria<Answer>(a => a.DeletedAt == null);
        var spec = new BaseSpecification<Answer>(filterExpression);
        var totalCount = await _unitOfWork.Repository<Answer>().CountAsync(spec);
        query.Includes?.Split(',').Select(i => char.ToUpper(i[0]) + i[1..]).ToList().ForEach(spec.AddInclude);
        if (!string.IsNullOrEmpty(query.Sort))
            if (query.Sort.StartsWith("-"))
                spec.ApplyOrderByDescending(query.Sort[1..]);
            else
                spec.ApplyOrderBy(query.Sort);
        spec.ApplyPaging(query.PageSize * (query.Page - 1), query.PageSize);
        var data = await _unitOfWork.Repository<Answer>().ListAsync(spec);
        return new PaginatedResult<AnswerDTO>(
            data.Select(_mapper.Map<AnswerDTO>).ToList(),
            totalCount,
            query.Page,
            query.PageSize);
    }


    public async Task<AnswerDTO> GetByIdAsync(int id, string? includes = null)
    {
        var spec = new BaseSpecification<Answer>(a => a.DeletedAt == null && a.Id == id);
        includes?.Split(',').Select(i => char.ToUpper(i[0]) + i[1..]).ToList().ForEach(spec.AddInclude);
        var answer = await _unitOfWork.Repository<Answer>().FirstOrDefaultAsync(spec)
            ?? throw new EntityNotFoundException(nameof(Answer), id);
        return _mapper.Map<AnswerDTO>(answer);
    }
    public async Task<AnswerDTO> CreateAsync(AnswerCreateDTO createDTO)
    {
        var answer = await _unitOfWork.Repository<Answer>().AddAsync(_mapper.Map<Answer>(createDTO));
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<AnswerDTO>(answer);
    }
    public async Task<AnswerDTO> UpdateAsync(int id, AnswerUpdateDTO updateDTO)
    {
        var existingAnswer = await _unitOfWork.Repository<Answer>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(Answer), id);
        _mapper.Map(updateDTO, existingAnswer);
        _unitOfWork.Repository<Answer>().Update(existingAnswer);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<AnswerDTO>(existingAnswer);
    }

    public async Task<AnswerDTO> PatchAsync(int id, AnswerPatchDTO patchDTO)
    {
        var existingAnswer = await _unitOfWork.Repository<Answer>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(Answer), id);
        _mapper.Map(patchDTO, existingAnswer);
        _unitOfWork.Repository<Answer>().Update(existingAnswer);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<AnswerDTO>(existingAnswer);
    }

    public async Task DeleteAsync(int id)
    {
        var existingAnswer = await _unitOfWork.Repository<Answer>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(Answer), id);
        _unitOfWork.Repository<Answer>().Delete(existingAnswer);
        await _unitOfWork.SaveChangesAsync();
    }

}
