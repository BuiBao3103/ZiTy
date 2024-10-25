using AutoMapper;
using zity.DTOs.Relationships;
using zity.ExceptionHandling.Exceptions;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class RelationshipService(IRelationshipRepository relationshipRepository, IMapper mapper) : IRelationshipService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IRelationshipRepository _relationshipRepository = relationshipRepository;

        public async Task<PaginatedResult<RelationshipDTO>> GetAllAsync(RelationshipQueryDTO queryParam)
        {
            var pageRelationships = await _relationshipRepository.GetAllAsync(queryParam);
            var relationships = pageRelationships.Contents.Select(_mapper.Map<RelationshipDTO>).ToList();

            return new PaginatedResult<RelationshipDTO>(
                relationships,
                pageRelationships.TotalItems,
                pageRelationships.Page,
                pageRelationships.PageSize);
        }

        public async Task<RelationshipDTO> GetByIdAsync(int id, string? includes = null)
        {
            var relationship = await _relationshipRepository.GetByIdAsync(id, includes)
                    ?? throw new EntityNotFoundException(nameof(Relationship), id);
            return _mapper.Map<RelationshipDTO>(relationship);
        }

        public async Task<RelationshipDTO> CreateAsync(RelationshipCreateDTO createDTO)
        {
            var relationship = _mapper.Map<Relationship>(createDTO);
            return _mapper.Map<RelationshipDTO>(await _relationshipRepository.CreateAsync(relationship));
        }

        public async Task<RelationshipDTO> UpdateAsync(int id, RelationshipUpdateDTO updateDTO)
        {
            var existingRelationship = await _relationshipRepository.GetByIdAsync(id, null)
                    ?? throw new EntityNotFoundException(nameof(Relationship), id);
            _mapper.Map(updateDTO, existingRelationship);
            var updatedRelationship = await _relationshipRepository.UpdateAsync(existingRelationship);
            return _mapper.Map<RelationshipDTO>(updatedRelationship);
        }

        public async Task<RelationshipDTO> PatchAsync(int id, RelationshipPatchDTO patchDTO)
        {
            var existingRelationship = await _relationshipRepository.GetByIdAsync(id, null)
                    ?? throw new EntityNotFoundException(nameof(Relationship), id);
            _mapper.Map(patchDTO, existingRelationship);
            var patchedRelationship = await _relationshipRepository.UpdateAsync(existingRelationship);
            return _mapper.Map<RelationshipDTO>(patchedRelationship);
        }

        public async Task DeleteAsync(int id)
        {
            await _relationshipRepository.DeleteAsync(id);
        }
    }
}
