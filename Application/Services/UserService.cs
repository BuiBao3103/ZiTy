using Application.Core.Constants;
using Application.Core.Services;
using Application.DTOs;
using Application.DTOs.Users;
using Application.Interfaces;
using AutoMapper;
using Domain.Core.Repositories;
using Domain.Core.Specifications;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using zity.Utilities;



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
        var totalCount = await _unitOfWork.Repository<User>().CountAsync(spec);
        query.Includes?.Split(',').ToList().ForEach(spec.AddInclude);
        if (!string.IsNullOrEmpty(query.Sort))
            if (query.Sort.StartsWith("-"))
                spec.ApplyOrderByDescending(query.Sort[1..]);
            else
                spec.ApplyOrderBy(query.Sort);
        spec.ApplyPaging(query.PageSize * (query.Page - 1), query.PageSize);
        var data = await _unitOfWork.Repository<User>().ListAsync(spec);
        return new PaginatedResult<UserDTO>(
            data.Select(_mapper.Map<UserDTO>).ToList(),
            totalCount,
            query.Page,
            query.PageSize);
    }

    public async Task<UserDTO> GetByIdAsync(int id, string? includes = null)
    {
        var spec = new BaseSpecification<User>(a => a.DeletedAt == null && a.Id == id);
        includes?.Split(',').ToList().ForEach(spec.AddInclude);
        var user = await _unitOfWork.Repository<User>().FirstOrDefaultAsync(spec)
            ?? throw new EntityNotFoundException(nameof(User), id);
        return _mapper.Map<UserDTO>(user);

    }

    public async Task<UserDTO> CreateAsync(UserCreateDTO createDTO)
    {
        var user = _mapper.Map<User>(createDTO);

        var password = PasswordGenerator.GeneratePassword(12);
        user.Password = BCrypt.Net.BCrypt.HashPassword(password);

        var createdUser = await _unitOfWork.Repository<User>().AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        await _emailService.SendAccountCreationEmail(createdUser, password);
        return _mapper.Map<UserDTO>(createdUser);
    }

    public async Task<UserDTO> UploadAvatarAsync(int id, IFormFile file)
    {
        var user = await _unitOfWork.Repository<User>().GetByIdAsync(id)
                ?? throw new EntityNotFoundException(nameof(User), id);
        if (!string.IsNullOrEmpty(user.Avatar))
        {
            await _mediaService.DeleteImageAsync(user.Avatar, CloudinaryConstants.USER_AVATARS_FOLDER);
        }
        var avatarUrl = await _mediaService.UploadImageAsync(file, CloudinaryConstants.USER_AVATARS_FOLDER);
        user.Avatar = avatarUrl;
        _unitOfWork.Repository<User>().Update(user);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<UserDTO>(user);
    }

    public async Task NotifyReceivedPackage(int userId)
    {
        var user = await _unitOfWork.Repository<User>().GetByIdAsync(userId)
                 ?? throw new EntityNotFoundException(nameof(User), userId);
        string phoneNumber = user.Phone;
        string message = "You have received a package!";
        //string message = "Cam on quy khach da su dung dich vu cua chung toi. Chuc quy khach mot ngay tot lanh!";
        await _smsService.SendSMSAsync(phoneNumber, message);
    }

    public async Task<UserDTO> GetMeAsync(int userId)
    {
        var spec = new BaseSpecification<User>(a => a.DeletedAt == null && a.Id == userId);
        spec.AddInclude(a => a.Relationships);
        var user = await _unitOfWork.Repository<User>().FirstOrDefaultAsync(spec)
               ?? throw new EntityNotFoundException(nameof(User), userId);
        return _mapper.Map<UserDTO>(user);
    }

    public async Task UpdateCurrentPassword(int userId, UpdatePasswordDTO updatePasswordDTO)
    {
        var user = await _unitOfWork.Repository<User>().GetByIdAsync(userId)
                ?? throw new EntityNotFoundException(nameof(User), userId);
        if (!BCrypt.Net.BCrypt.Verify(updatePasswordDTO.CurrentPassword, user.Password))
            throw new BusinessRuleException("Current password is incorrect");
        if (updatePasswordDTO.NewPassword != updatePasswordDTO.ConfirmPassword)
            throw new BusinessRuleException("New password and confirm password do not match");
        user.Password = BCrypt.Net.BCrypt.HashPassword(updatePasswordDTO.NewPassword);
        _unitOfWork.Repository<User>().Update(user);
        await _unitOfWork.SaveChangesAsync();
    }
}
