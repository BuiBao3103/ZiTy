using System.ComponentModel.DataAnnotations;

namespace zity.DTOs.Auth
{
    public class RevokeTokenDto
    {
        [Required]
        public string RefreshToken { get; set; } = null!;
    }
}
