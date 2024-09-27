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

        public async Task<PaginatedResult<RelationshipDTO>> GetAllAsync(RelationshipQueryDTO query)
        {
            var pageRelationships = await _relationshipRepository.GetAllAsync(query);
            var relationshipDTOs = pageRelationships.Contents.Select(RelationshipMapper.ToRelationshipDTO).ToList();
            return new PaginatedResult<RelationshipDTO>(relationshipDTOs, pageRelationships.TotalItems, pageRelationships.Page, pageRelationships.PageSize);
        }
    }
}
