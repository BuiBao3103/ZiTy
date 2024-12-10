using System.ComponentModel.DataAnnotations;
namespace Application.DTOs.Users;

public class UpdatePasswordDTO
{
    [Required]
    public string CurrentPassword { get; set; } = null!;
    [Required]
    public string NewPassword { get; set; } = null!;
    [Required]
    public string ConfirmPassword { get; set; } = null!;
}
