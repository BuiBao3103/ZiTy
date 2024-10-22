using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace zity.Models
{
    [Table("refresh_tokens")]
    public class RefreshToken
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("token", TypeName = "varchar(255)")]
        public string Token { get; set; } = null!;

        [Required]
        [Column("expiry_time")]
        public DateTime ExpiryTime { get; set; }

        [Required]
        [Column("is_revoked")]
        public bool IsRevoked { get; set; } = false; // Default to false (not revoked)

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;
    }
}
