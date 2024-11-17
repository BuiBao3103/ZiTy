using System.ComponentModel.DataAnnotations;

namespace Identity.Application.DTOs.Auth;

public class UpdatePasswordFirstLoginDTO
{
    [Required]
    public string NewPassword { get; set; } = null!;

    [Required]
    public string ConfirmPassword { get; set; } = null!;
}
