using zity.DTOs.Relationships;
using zity.Models;

namespace zity.Mappers
{
    public class RelationshipMapper
    {
        public static RelationshipDTO ToDTO(Relationship relationship)
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

        public static Relationship ToModelFromCreate(RelationshipDTO RelationshipDTO)
        {
            return new Relationship
            {
                Role = RelationshipDTO.Role,
                UserId = RelationshipDTO.UserId,
                ApartmentId = RelationshipDTO.ApartmentId,
            };
        }
    }
}
