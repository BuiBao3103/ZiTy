using Application.DTOs;
using Application.DTOs.UserAnswers;
using Application.Interfaces;
using AutoMapper;
using Domain.Core.Repositories;
using Domain.Core.Specifications;
using Domain.Entities;
using Domain.Exceptions;


namespace Application.Services;

public class UserAnswerService(IUnitOfWork unitOfWork, IMapper mapper) : IUserAnswerService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<PaginatedResult<UserAnswerDTO>> GetAllAsync(UserAnswerQueryDTO query)
    {
        var spec = new BaseSpecification<UserAnswer>(a => a.DeletedAt == null);
        var totalCount = await _unitOfWork.Repository<UserAnswer>().CountAsync(spec);
        query.Includes?.Split(',').ToList().ForEach(spec.AddInclude);
        if (!string.IsNullOrEmpty(query.Sort))
            if (query.Sort.StartsWith("-"))
                spec.ApplyOrderByDescending(query.Sort[1..]);
            else
                spec.ApplyOrderBy(query.Sort);
        spec.ApplyPaging(query.PageSize * (query.Page - 1), query.PageSize);
        var data = await _unitOfWork.Repository<UserAnswer>().ListAsync(spec);
        return new PaginatedResult<UserAnswerDTO>(
            data.Select(_mapper.Map<UserAnswerDTO>).ToList(),
            totalCount,
            query.Page,
            query.PageSize);
    }
    public async Task<UserAnswerDTO> GetByIdAsync(int id, string? includes = null)
    {
        var spec = new BaseSpecification<UserAnswer>(a => a.DeletedAt == null && a.Id == id);
        includes?.Split(',').ToList().ForEach(spec.AddInclude);
        var userAnswer = await _unitOfWork.Repository<UserAnswer>().FirstOrDefaultAsync(spec)
            ?? throw new EntityNotFoundException(nameof(UserAnswer), id);
        return _mapper.Map<UserAnswerDTO>(userAnswer);
    }
    public async Task<UserAnswerDTO> CreateAsync(UserAnswerCreateDTO createDTO)
    {
        var userAnswer = await _unitOfWork.Repository<UserAnswer>().AddAsync(_mapper.Map<UserAnswer>(createDTO));
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<UserAnswerDTO>(userAnswer);
    }
    public async Task<UserAnswerDTO> UpdateAsync(int id, UserAnswerUpdateDTO updateDTO)
    {
        var existingUserAnswer = await _unitOfWork.Repository<UserAnswer>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(UserAnswer), id);
        _mapper.Map(updateDTO, existingUserAnswer);
        _unitOfWork.Repository<UserAnswer>().Update(existingUserAnswer);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<UserAnswerDTO>(existingUserAnswer);
    }

    public async Task<UserAnswerDTO> PatchAsync(int id, UserAnswerPatchDTO patchDTO)
    {
        var existingUserAnswer = await _unitOfWork.Repository<UserAnswer>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(UserAnswer), id);
        _mapper.Map(patchDTO, existingUserAnswer);
        _unitOfWork.Repository<UserAnswer>().Update(existingUserAnswer);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<UserAnswerDTO>(existingUserAnswer);
    }
    public async Task DeleteAsync(int id)
    {
        var existingUserAnswer = await _unitOfWork.Repository<UserAnswer>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(UserAnswer), id);
        _unitOfWork.Repository<UserAnswer>().Delete(existingUserAnswer);
        await _unitOfWork.SaveChangesAsync();
    }
}
