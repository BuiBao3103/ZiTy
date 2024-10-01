using System.ComponentModel.DataAnnotations;

namespace zity.DTOs.Relationships
{
    public class RelationshipQueryDTO : BaseQueryDTO
    {
        [RegularExpression(@"^(eq|neq|gt|gte|lt|lte|like|in):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
        public string? Id { get; set; }
        [RegularExpression(@"^(eq):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
        public string? UserId { get; set; }
        [RegularExpression(@"^(eq):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
        public string? ApartmentId { get; set; }
    }
}
