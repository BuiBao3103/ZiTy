using zity.DTOs.Users;
using zity.Models;

namespace zity.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToDTO(this User userModel)
        {
            return new UserDTO
            {
                Id = userModel.Id,
                Username = userModel.Username,
                Avatar = userModel.Avatar,
                Email = userModel.Email,
                Phone = userModel.Phone,
                FullName = userModel.FullName,
                Gender = userModel.Gender,
                UserType = userModel.UserType,
                NationId = userModel.NationId,
                DateOfBirth = userModel.DateOfBirth,
                CreatedAt = userModel.CreatedAt,
                UpdatedAt = userModel.UpdatedAt,
                IsStaying = userModel.IsStaying,
                //Items = userModel.Items.Select(ItemMapper.ToDTO).ToList(),
                //OtherAnswers = userModel.OtherAnswers.Select(OtherAnswerMapper.ToDTO).ToList(),
                Relationships = userModel.Relationships.Select(RelationshipMapper.ToDTO).ToList(),
                //Surveys = userModel.Surveys.Select(SurveyMapper.ToDTO).ToList(),
                //UserAnswers = userModel.UserAnswers.Select(UserAnswerMapper.ToDTO).ToList()
            };
        }

        public static User FromCreateDTO(UserCreateDTO userCreateDTO)
        {
            return new User
            {
                Username = userCreateDTO.Username,
                Email = userCreateDTO.Email,
                Phone = userCreateDTO.Phone,
                Gender = userCreateDTO.Gender,
                FullName = userCreateDTO.FullName,
                NationId = userCreateDTO.NationId,
                DateOfBirth = userCreateDTO.DateOfBirth,
                IsFirstLogin = true,
                UserType = "RESIDENT",
                IsStaying = true,
                CreatedAt = DateTime.Now
            };
        }
    }
}

