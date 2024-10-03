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
                UpdatedAt = relationship.UpdatedAt,
                UserId = relationship.UserId,
                ApartmentId = relationship.ApartmentId,
                User = relationship.User != null ? UserMapper.ToUserDTO(relationship.User) : null,
                //Apartment = relationship.Apartment != null ? ApartmentMapper.ToDTO(relationship.Apartment) : null,
                //Bills = relationship.Bills.Select(bill => BillMapper.ToDTO(bill)).ToList(),
                //Reports = relationship.Reports.Select(report => ReportMapper.ToDTO(report)).ToList(),
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

        public static Relationship UpdateModelFromUpdate(Relationship relationship, RelationshipUpdateDTO updateDTO)
        {
            relationship.Role = updateDTO.Role;
            relationship.UserId = updateDTO.UserId;
            relationship.ApartmentId = updateDTO.ApartmentId;
            relationship.UpdatedAt = DateTime.Now;
            return relationship;
        }

        public static Relationship PatchModelFromPatch(Relationship relationship, RelationshipPatchDTO patchDTO)
        {
            if (patchDTO.Role != null)
                relationship.Role = patchDTO.Role;
            if (patchDTO.UserId != null)
                relationship.UserId = patchDTO.UserId.Value;
            if (patchDTO.ApartmentId != null)
                relationship.ApartmentId = patchDTO.ApartmentId;
            relationship.UpdatedAt = DateTime.Now;
            return relationship;
        }
    }
}