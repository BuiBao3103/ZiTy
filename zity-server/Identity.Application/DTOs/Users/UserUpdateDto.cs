using System.ComponentModel.DataAnnotations;

namespace Identity.Application.DTOs.Users;
public class UserUpdateDTO
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
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
    public bool IsStaying { get; set; }
}
