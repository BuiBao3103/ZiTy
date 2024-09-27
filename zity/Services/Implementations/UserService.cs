using zity.DTOs.Users;
using zity.Repositories.Interfaces;
using zity.Mappers;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PaginatedResult<UserDto>> GetAllAsync(UserQueryDto query)
        {
            var pageUsers = await _userRepository.GetAllAsync(query);
            var userDtos = pageUsers.Contents.Select(UserMapper.ToUserDto).ToList();
            return new PaginatedResult<UserDto>(userDtos, pageUsers.TotalItems, pageUsers.Page, pageUsers.PageSize);
        }
    }
}
