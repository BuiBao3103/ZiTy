using zity.DTOs.Relationships;
using zity.Models;

namespace zity.Mappers
{
    public class RelationshipMapper
    {
        public static RelationshipDto ToDTO(Relationship relationship)
        {
            return new RelationshipDto
            {
                Id = relationship.Id,
                Role = relationship.Role,
                CreatedAt = relationship.CreatedAt,
                UserId = relationship.UserId,
                ApartmentId = relationship.ApartmentId,
                User = relationship.User != null ? UserMapper.ToUserDTO(relationship.User) : null,
            };
        }

        public static Relationship ToModelFromCreate(RelationshipDto RelationshipDto)
        {
            return new Relationship
            {
                Role = RelationshipDto.Role,
                UserId = RelationshipDto.UserId,
                ApartmentId = RelationshipDto.ApartmentId,
            };
        }
    }
}
