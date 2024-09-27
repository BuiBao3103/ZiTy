using System.ComponentModel.DataAnnotations;

namespace ZiTy.DTOs.Users
{
    public class UserQueryDto : BaseQueryDto
    {
        [RegularExpression(@"^(eq|neq|gt|gte|lt|lte|like|in):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
        public string Id { get; set; } = "";

        [RegularExpression(@"^(eq|neq|like|in):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
        public string Username { get; set; } = "";

        [MaxLength(100, ErrorMessage = "Search term cannot exceed 100 characters")]
        public string Search { get; set; } = "";
    }
}
