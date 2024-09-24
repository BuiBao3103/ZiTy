namespace ZiTy.DTOs.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string? Avatar { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string UserType { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
    }
}
