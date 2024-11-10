namespace Application.DTOs.Users;

public class UserPatchDTO
{
    public string? Email { get; set; } = null!;
    public string? Phone { get; set; } = null!;
    public string? Gender { get; set; } = null!;
    public string? FullName { get; set; } = null!;
    public string? NationId { get; set; } = null!;
    public DateTime? DateOfBirth { get; set; }
    public bool? IsStaying { get; set; }
}

