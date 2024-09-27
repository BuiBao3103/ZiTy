using System.Linq;
using zity.DTOs.Users;
using zity.Models;

namespace zity.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this User userModel)
        {
            return new UserDto
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

    }
}
