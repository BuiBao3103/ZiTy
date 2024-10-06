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
                DateOfBirth = userModel.DateOfBirth
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

