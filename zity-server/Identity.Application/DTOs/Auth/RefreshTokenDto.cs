using System.ComponentModel.DataAnnotations;

namespace Identity.Application.DTOs.Auth;
public class RefreshTokenDto
{
    [Required]
    public string RefreshToken { get; set; } = null!;
}
