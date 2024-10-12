using System.Threading.Tasks;
using zity.DTOs.Relationships;
using zity.Mappers;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{

    public class RelationshipService(IRelationshipRepository relationshipRepository) : IRelationshipService
    {
        private readonly IRelationshipRepository _relationshipRepository = relationshipRepository;

        public async Task<PaginatedResult<RelationshipDTO>> GetAllAsync(RelationshipQueryDTO queryParam)
        {
            var pageRelationships = await _relationshipRepository.GetAllAsync(queryParam);

            return new PaginatedResult<RelationshipDTO>(
                pageRelationships.Contents.Select(RelationshipMapper.ToDTO).ToList(),
                pageRelationships.TotalItems,
                pageRelationships.Page,
                pageRelationships.PageSize);
        }
        public async Task<RelationshipDTO?> GetByIdAsync(int id, string? includes)
        {
            var relationship = await _relationshipRepository.GetByIdAsync(id, includes);
            return relationship != null ? RelationshipMapper.ToDTO(relationship) : null;
        }
        public async Task<RelationshipDTO> CreateAsync(RelationshipCreateDTO createDTO)
        {
            var relationship = RelationshipMapper.ToModelFromCreate(createDTO);
            return RelationshipMapper.ToDTO(await _relationshipRepository.CreateAsync(relationship));
        }
        public async Task<RelationshipDTO?> UpdateAsync(int id, RelationshipUpdateDTO updateDTO)
        {
            var existingRelationship = await _relationshipRepository.GetByIdAsync(id, null);
            if (existingRelationship == null)
            {
                return null;
            }

            RelationshipMapper.UpdateModelFromUpdate(existingRelationship, updateDTO);
            var updatedRelationship = await _relationshipRepository.UpdateAsync(existingRelationship);
            return RelationshipMapper.ToDTO(updatedRelationship);
        }
        public async Task<RelationshipDTO?> PatchAsync(int id, RelationshipPatchDTO patchDTO)
        {
            var existingRelationship = await _relationshipRepository.GetByIdAsync(id, null);
            if (existingRelationship == null)
            {
                return null;
            }

            RelationshipMapper.PatchModelFromPatch(existingRelationship, patchDTO);
            var patchedRelationship = await _relationshipRepository.UpdateAsync(existingRelationship);
            return RelationshipMapper.ToDTO(patchedRelationship);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _relationshipRepository.DeleteAsync(id);
        }


    }
}
