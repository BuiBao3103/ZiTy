using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZiTy.DTOs.Users;
using ZiTy.Models;
using ZiTy.Repositories.Interfaces;
using ZiTy.Mappers;
using Microsoft.EntityFrameworkCore;
using ZiTy.Services.Interfaces;
using zity.DTOs.Users;

namespace ZiTy.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> GetAllAsync(UserQueryDto query)
        {
            var users = await _userRepository.GetAllAsync();
            var usersDto = users.Select(user => user.ToUserDto()).ToList();
            return usersDto;
        }

    }
}
