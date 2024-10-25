using AutoMapper;
using zity.DTOs.OtherAnswers;
using zity.ExceptionHandling.Exceptions;
using zity.Mappers;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class OtherAnswerService(IOtherAnswerRepository otherAnswerRepository, IMapper mapper) : IOtherAnswerService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IOtherAnswerRepository _otherAnswerRepository = otherAnswerRepository;

        public async Task<PaginatedResult<OtherAnswerDTO>> GetAllAsync(OtherAnswerQueryDTO queryParam)
        {
            var pageOtherAnswers = await _otherAnswerRepository.GetAllAsync(queryParam);
            var otherAnswers = pageOtherAnswers.Contents.Select(_mapper.Map<OtherAnswerDTO>).ToList();
            return new PaginatedResult<OtherAnswerDTO>(
                otherAnswers,
                pageOtherAnswers.TotalItems,
                pageOtherAnswers.Page,
                pageOtherAnswers.PageSize);
        }


        public async Task<OtherAnswerDTO> GetByIdAsync(int id, string? includes)
        {
            var otherAnswer = await _otherAnswerRepository.GetByIdAsync(id, includes)
                    ?? throw new EntityNotFoundException(nameof(OtherAnswer), id);
            return _mapper.Map<OtherAnswerDTO>(otherAnswer);
        }
        public async Task<OtherAnswerDTO> CreateAsync(OtherAnswerCreateDTO createDTO)
        {
            var otherAnswer = _mapper.Map<OtherAnswer>(createDTO);
            return _mapper.Map<OtherAnswerDTO>(await _otherAnswerRepository.CreateAsync(otherAnswer));
        }
        public async Task<OtherAnswerDTO> UpdateAsync(int id, OtherAnswerUpdateDTO updateDTO)
        {
            var existingOtherAnswer = await _otherAnswerRepository.GetByIdAsync(id)
                    ?? throw new EntityNotFoundException(nameof(OtherAnswer), id);
            _mapper.Map(updateDTO, existingOtherAnswer);
            var updatedOtherAnswer = await _otherAnswerRepository.UpdateAsync(existingOtherAnswer);
            return _mapper.Map<OtherAnswerDTO>(updatedOtherAnswer) ;
        }

        public async Task<OtherAnswerDTO> PatchAsync(int id, OtherAnswerPatchDTO patchDTO)
        {
            var existingOtherAnswer = await _otherAnswerRepository.GetByIdAsync(id)
                    ?? throw new EntityNotFoundException(nameof(OtherAnswer), id);
            _mapper.Map(patchDTO, existingOtherAnswer);
            var patchedOtherAnswer = await _otherAnswerRepository.UpdateAsync(existingOtherAnswer);
            return _mapper.Map<OtherAnswerDTO>(patchedOtherAnswer);
        }

        public async Task DeleteAsync(int id)
        {
           await _otherAnswerRepository.DeleteAsync(id);
        }

    }
}
