using System.Linq;
using ZiTy.DTOs.Users;
using ZiTy.Models;

namespace ZiTy.Mappers
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
