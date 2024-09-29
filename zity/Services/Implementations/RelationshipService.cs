using zity.DTOs.Relationships;
using zity.Mappers;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
  
    public class RelationshipService(IRelationshipRepository relationshipRepository) : IRelationshipService
    {
        private readonly IRelationshipRepository _relationshipRepository = relationshipRepository;

        public async Task<PaginatedResult<RelationshipDto>> GetAllAsync(RelationshipQueryDTO query)
        {
            var pageRelationships = await _relationshipRepository.GetAllAsync(query);
            var RelationshipDtos = pageRelationships.Contents.Select(RelationshipMapper.ToDTO).ToList();
            return new PaginatedResult<RelationshipDto>(RelationshipDtos, pageRelationships.TotalItems, pageRelationships.Page, pageRelationships.PageSize);
        }

        public async Task<RelationshipDto> GetByIdAsync(int id, string includes)
        {
            var relationship = await _relationshipRepository.GetByIdAsync(id, includes);
            return RelationshipMapper.ToDTO(relationship);
        }
    }
}
