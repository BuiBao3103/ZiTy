

using Application.DTOs.Items;
using Application.DTOs.OtherAnswers;
using Application.DTOs.Relationships;
using Application.DTOs.Surveys;
using Application.DTOs.UserAnswers;

namespace Application.DTOs.Users;

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
    public ICollection<ItemDTO> Items { get; set; } = [];
    public ICollection<OtherAnswerDTO> OtherAnswers { get; set; } = [];
    public ICollection<RelationshipDTO> Relationships { get; set; } = [];
    public ICollection<SurveyDTO> Surveys { get; set; } = [];
    public ICollection<UserAnswerDTO> UserAnswers { get; set; } = [];

}
