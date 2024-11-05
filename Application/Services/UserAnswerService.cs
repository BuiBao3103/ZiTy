using Application.DTOs;
using Application.DTOs.UserAnswers;
using Application.Interfaces;
using AutoMapper;
using Domain.Core.Repositories;
using Domain.Core.Specifications;
using Domain.Entities;


namespace Application.Services;

public class UserAnswerService(IUnitOfWork unitOfWork, IMapper mapper) : IUserAnswerService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<PaginatedResult<UserAnswerDTO>> GetAllAsync(UserAnswerQueryDTO query)
    {
        var spec = new BaseSpecification<UserAnswer>(a => a.DeletedAt == null);
        var data = await _unitOfWork.Repository<UserAnswer>().ListAsync(spec);
        var totalCount = await _unitOfWork.Repository<UserAnswer>().CountAsync(spec);
        return new PaginatedResult<UserAnswerDTO>(
            data.Select(_mapper.Map<UserAnswerDTO>).ToList(),
            totalCount,
            query.Page,
            query.PageSize);
    }
    public async Task<UserAnswerDTO> GetByIdAsync(int id, string? includes = null)
    {
        var userAnswer = await _unitOfWork.Repository<UserAnswer>().GetByIdAsync(id)
         //?? throw new EntityNotFoundException(nameof(UserAnswer), id);
         ?? throw new Exception(nameof(UserAnswer));
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
        //?? throw new EntityNotFoundException(nameof(UserAnswer), id);
        ?? throw new Exception(nameof(UserAnswer));
        _mapper.Map(updateDTO, existingUserAnswer);
        _unitOfWork.Repository<UserAnswer>().Update(existingUserAnswer);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<UserAnswerDTO>(existingUserAnswer);
    }

    public async Task<UserAnswerDTO> PatchAsync(int id, UserAnswerPatchDTO patchDTO)
    {
        var existingUserAnswer = await _unitOfWork.Repository<UserAnswer>().GetByIdAsync(id)
        //?? throw new EntityNotFoundException(nameof(UserAnswer), id);
        ?? throw new Exception(nameof(UserAnswer));
        _mapper.Map(patchDTO, existingUserAnswer);
        _unitOfWork.Repository<UserAnswer>().Update(existingUserAnswer);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<UserAnswerDTO>(existingUserAnswer);
    }
    public async Task DeleteAsync(int id)
    {
        var existingUserAnswer = await _unitOfWork.Repository<UserAnswer>().GetByIdAsync(id)
                   //?? throw new EntityNotFoundException(nameof(UserAnswer), id);
                   ?? throw new Exception(nameof(UserAnswer));
        _unitOfWork.Repository<UserAnswer>().Delete(existingUserAnswer);
        await _unitOfWork.SaveChangesAsync();
    }
}
