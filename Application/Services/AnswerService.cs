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

        public async Task<PaginatedResult<AnswerDTO>> GetAllAsync(AnswerQueryDTO queryParam)
        {
            var answerSpec = new BaseSpecification<Answer>(a => a.DeletedAt != null);
            var data = await _unitOfWork.Repository<Answer>().ListAsync(answerSpec);
            var totalCount = await _unitOfWork.Repository<Answer>().CountAsync(answerSpec);
            return new PaginatedResult<AnswerDTO>(
                data.Select(_mapper.Map<AnswerDTO>).ToList(),
                totalCount,
                queryParam.Page,
                queryParam.PageSize);
        }


        //public async Task<AnswerDTO> GetByIdAsync(int id, string? includes = null)
        //{
        //    var answer = await _answerRepository.GetByIdAsync(id, includes)
        //        ?? throw new EntityNotFoundException(nameof(Answer), id);
        //    return _mapper.Map<AnswerDTO>(answer);
        //}
        //public async Task<AnswerDTO> CreateAsync(AnswerCreateDTO createDTO)
        //{
        //    var answer = _mapper.Map<Answer>(createDTO);
        //    return _mapper.Map<AnswerDTO>(await _answerRepository.CreateAsync(answer));
        //}
        //public async Task<AnswerDTO> UpdateAsync(int id, AnswerUpdateDTO updateDTO)
        //{
        //    var existingAnswer = await _answerRepository.GetByIdAsync(id)
        //        ?? throw new EntityNotFoundException(nameof(Answer), id);
        //    _mapper.Map(updateDTO, existingAnswer);
        //    var updatedAnswer = await _answerRepository.UpdateAsync(existingAnswer);
        //    return _mapper.Map<AnswerDTO>(updatedAnswer);
        //}

        //public async Task<AnswerDTO> PatchAsync(int id, AnswerPatchDTO patchDTO)
        //{
        //    var existingAnswer = await _answerRepository.GetByIdAsync(id)
        //        ?? throw new EntityNotFoundException(nameof(Answer), id);
        //    _mapper.Map(patchDTO, existingAnswer);
        //    var patchedAnswer = await _answerRepository.UpdateAsync(existingAnswer);
        //    return _mapper.Map<AnswerDTO>(patchedAnswer);
        //}

        //public async Task DeleteAsync(int id)
        //{
        //    await _answerRepository.DeleteAsync(id);
        //}

    }
}
