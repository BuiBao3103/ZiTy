using zity.DTOs.Users;
using zity.Repositories.Interfaces;
using zity.Mappers;
using zity.Services.Interfaces;
using zity.Utilities;
using zity.Constraints;
using zity.ExceptionHandling;
using AutoMapper;
using zity.Models;

namespace zity.Services.Implementations
{
    public class UserService(IUserRepository userRepository, IMediaService mediaService, IEmailService emailService, ISmsService smsService, IMapper mapper) : IUserService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMediaService _mediaService = mediaService;
        private readonly IEmailService _emailService = emailService;
        private readonly ISmsService _smsService = smsService;

        public async Task<PaginatedResult<UserDTO>> GetAllAsync(UserQueryDTO query)
        {
            var pageUsers = await _userRepository.GetAllAsync(query);
            var users = pageUsers.Contents.Select(_mapper.Map<UserDTO>).ToList();
            return new PaginatedResult<UserDTO>(
                users,
                pageUsers.TotalItems,
                pageUsers.Page,
                pageUsers.PageSize);
        }

        public async Task<UserDTO?> GetByIdAsync(int id, string? includes = null)
        {
            var user = await _userRepository.GetByIdAsync(id, includes);
            return user != null ? _mapper.Map<UserDTO>(user) : null;
        }

        public async Task<UserDTO> CreateAsync(UserCreateDTO userCreateDTO)
        {
            var user = _mapper.Map<User>(userCreateDTO);

            var password = PasswordGenerator.GeneratePassword(12);
            user.Password = BCrypt.Net.BCrypt.HashPassword(password);

            var createdUser = await _userRepository.CreateAsync(user);

            await _emailService.SendAccountCreationEmail(createdUser, password);
            return _mapper.Map<UserDTO>(createdUser);
        }

        public async Task<UserDTO?> UploadAvatarAsync(int id, IFormFile file)
        {
            var user = await _userRepository.GetByIdAsync(id, null);
            if (user == null)
                return null;
            if (!string.IsNullOrEmpty(user.Avatar))
            {
                await _mediaService.DeleteImageAsync(user.Avatar, CloudinaryConstants.USER_AVATARS_FOLDER);
            }
            var avatarUrl = await _mediaService.UploadImageAsync(file, CloudinaryConstants.USER_AVATARS_FOLDER);
            user.Avatar = avatarUrl;
            await _userRepository.UpdateAsync(user);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task NotifyReceivedPackage(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId, null);
            if (user == null)
            {
                throw new AppError(message: "User not found", statusCode: 404);
            }
            string phoneNumber = user.Phone;
            string message = "You have received a package!";
            //string message = "Cam on quy khach da su dung dich vu cua chung toi. Chuc quy khach mot ngay tot lanh!";
            await _smsService.SendSMSAsync(phoneNumber, message);
        }
    }
}
