using zity.DTOs.Users;
using zity.Repositories.Interfaces;
using zity.Mappers;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<PaginatedResult<UserDTO>> GetAllAsync(UserQueryDTO query)
        {
            var pageUsers = await _userRepository.GetAllAsync(query);
            var userDTOs = pageUsers.Contents.Select(UserMapper.ToUserDTO).ToList();
            return new PaginatedResult<UserDTO>(userDTOs, pageUsers.TotalItems, pageUsers.Page, pageUsers.PageSize);
        }
    }
}
