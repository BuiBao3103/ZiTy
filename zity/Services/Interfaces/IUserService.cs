using System.Collections.Generic;
using System.Threading.Tasks;
using ZiTy.DTOs.Users;
using ZiTy.Models;

namespace ZiTy.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
    }
}
