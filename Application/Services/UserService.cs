using Application.DTOs;
using Application.DTOs.Users;
using Application.Interfaces;
using AutoMapper;
using Domain.Core.Repositories;
using Domain.Core.Specifications;
using Domain.Entities;
using Microsoft.AspNetCore.Http;



namespace Application.Services;

public class UserService(IUnitOfWork unitOfWork, IMediaService mediaService, IEmailService emailService, ISmsService smsService, IMapper mapper) : IUserService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    private readonly IMediaService _mediaService = mediaService;
    private readonly IEmailService _emailService = emailService;
    private readonly ISmsService _smsService = smsService;

    public async Task<PaginatedResult<UserDTO>> GetAllAsync(UserQueryDTO query)
    {
        var spec = new BaseSpecification<User>(a => a.DeletedAt == null);
        var data = await _unitOfWork.Repository<User>().ListAsync(spec);
        var totalCount = await _unitOfWork.Repository<User>().CountAsync(spec);
        return new PaginatedResult<UserDTO>(
            data.Select(_mapper.Map<UserDTO>).ToList(),
            totalCount,
            query.Page,
            query.PageSize);
    }

    public async Task<UserDTO> GetByIdAsync(int id, string? includes = null)
    {
        //var user = await _userRepository.GetByIdAsync(id, includes)
        //        ?? throw new EntityNotFoundException(nameof(User), id);
        //return _mapper.Map<UserDTO>(user);
        var answer = await _unitOfWork.Repository<Answer>().GetByIdAsync(id)
       //?? throw new EntityNotFoundException(nameof(Answer), id);
       ?? throw new Exception(nameof(Answer));
        return _mapper.Map<UserDTO>(answer);
    }

    public async Task<UserDTO> CreateAsync(UserCreateDTO createDTO)
    {
        //var user = _mapper.Map<User>(createDTO);

        //var password = PasswordGenerator.GeneratePassword(12);
        //user.Password = BCrypt.Net.BCrypt.HashPassword(password);

        //var createdUser = await _userRepository.CreateAsync(user);

        //await _emailService.SendAccountCreationEmail(createdUser, password);
        //return _mapper.Map<UserDTO>(createdUser);

        var answer = await _unitOfWork.Repository<Answer>().AddAsync(_mapper.Map<Answer>(createDTO));
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<UserDTO>(answer);
    }

    public async Task<UserDTO> UploadAvatarAsync(int id, IFormFile file)
    {
        var user = await _userRepository.GetByIdAsync(id)
                ?? throw new EntityNotFoundException(nameof(UserAnswer), id);
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
        var user = await _userRepository.GetByIdAsync(userId, null)
                ?? throw new EntityNotFoundException(nameof(User), userId);
        string phoneNumber = user.Phone;
        string message = "You have received a package!";
        //string message = "Cam on quy khach da su dung dich vu cua chung toi. Chuc quy khach mot ngay tot lanh!";
        await _smsService.SendSMSAsync(phoneNumber, message);
    }

    public async Task<UserDTO> GetMeAsync(int userId, string? includes = null)
    {
        var user = await _userRepository.GetByIdAsync(userId, includes)
                ?? throw new EntityNotFoundException(nameof(User), userId);
        return _mapper.Map<UserDTO>(user);
    }
}
