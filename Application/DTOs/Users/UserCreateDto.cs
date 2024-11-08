using System.ComponentModel.DataAnnotations;


namespace Application.DTOs.Users;

public class UserCreateDTO
{
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    [Phone]
    public string Phone { get; set; } = null!;
    [Required]
    public string Gender { get; set; } = null!;
    [Required]
    public string FullName { get; set; } = null!;
    [Required]
    public string NationId { get; set; } = null!;
    [Required]
    public DateTime DateOfBirth { get; set; }
    [Required]
    public string UserType { get; set; } = null!;

}
