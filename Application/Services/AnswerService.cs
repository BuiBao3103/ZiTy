using AutoMapper;
using Application.DTOs.Answers;
using Domain.Entities;
using Application.Interfaces;
using Application.Core.Utilities;
using Domain.Core.Repositories;
using Domain.Core.Specifications;


namespace Application.Services
{
    public class AnswerService(IUnitOfWork unitOfWork, IMapper mapper) : IAnswerService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<PaginatedResult<AnswerDTO>> GetAllAsync(AnswerQueryDTO query)
        {
            var spec = new BaseSpecification<Answer>(a => a.DeletedAt == null);
            var data = await _unitOfWork.Repository<Answer>().ListAsync(spec);
            var totalCount = await _unitOfWork.Repository<Answer>().CountAsync(spec);
            return new PaginatedResult<AnswerDTO>(
                data.Select(_mapper.Map<AnswerDTO>).ToList(),
                totalCount,
                query.Page,
                query.PageSize);
        }


        public async Task<AnswerDTO> GetByIdAsync(int id, string? includes = null)
        {
            var answer = await _unitOfWork.Repository<Answer>().GetByIdAsync(id)
                //?? throw new EntityNotFoundException(nameof(Answer), id);
                ?? throw new Exception(nameof(Answer));
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
                //?? throw new EntityNotFoundException(nameof(Answer), id);
                ?? throw new Exception(nameof(Answer));
            _mapper.Map(updateDTO, existingAnswer);
            _unitOfWork.Repository<Answer>().Update(existingAnswer);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<AnswerDTO>(existingAnswer);
        }

        public async Task<AnswerDTO> PatchAsync(int id, AnswerPatchDTO patchDTO)
        {
            var existingAnswer = await _unitOfWork.Repository<Answer>().GetByIdAsync(id)
                //?? throw new EntityNotFoundException(nameof(Answer), id);
                ?? throw new Exception(nameof(Answer));
            _mapper.Map(patchDTO, existingAnswer);
            _unitOfWork.Repository<Answer>().Update(existingAnswer);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<AnswerDTO>(existingAnswer);
        }

        public async Task DeleteAsync(int id)
        {
            var existingAnswer = await _unitOfWork.Repository<Answer>().GetByIdAsync(id)
                //?? throw new EntityNotFoundException(nameof(Answer), id);
                ?? throw new Exception(nameof(Answer));
            _unitOfWork.Repository<Answer>().Delete(existingAnswer);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
