using Microsoft.AspNetCore.Http;
namespace Application.Core.Services;

public interface IMediaService
{
    Task DeleteImageAsync(string url, string folder);
    Task<string> UploadImageAsync(IFormFile file, string folder);
}
