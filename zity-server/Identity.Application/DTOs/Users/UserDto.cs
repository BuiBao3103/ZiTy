
namespace Identity.Application.DTOs.Users;

public class UserDTO
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string? Avatar { get; set; }
    public bool? IsFirstLogin { get; set; }
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string NationId { get; set; } = null!;
    public string UserType { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public bool? IsStaying { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
