using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Auth;

public class UpdatePasswordFirstLoginDTO
{
    [Required]
    public string NewPassword { get; set; } = null!;

    [Required]
    public string ConfirmPassword { get; set; } = null!;
}
