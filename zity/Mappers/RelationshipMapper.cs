using zity.DTOs.Relationships;
using zity.Models;

namespace zity.Mappers
{
    public class RelationshipMapper
    {
        public static RelationshipDTO ToRelationshipDTO(Relationship relationship)
        {
            return new RelationshipDTO
            {
                Id = relationship.Id,
                Role = relationship.Role,
                CreatedAt = relationship.CreatedAt,
                UserId = relationship.UserId,
                ApartmentId = relationship.ApartmentId,
                User = relationship.User != null ? UserMapper.ToUserDTO(relationship.User) : null,
            };
        }
    }
}
