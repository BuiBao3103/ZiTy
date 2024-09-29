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
        public static Relationship ToModelFromCreate(RelationshipCreateDTO relationshipCreateDTO)
        {
            return new Relationship
            {
                Role = relationshipCreateDTO.Role,
                UserId = relationshipCreateDTO.UserId,
                ApartmentId = relationshipCreateDTO.ApartmentId,
                CreatedAt = DateTime.Now,
            };
        }
    }
}
