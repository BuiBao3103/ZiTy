namespace zity.Services.Interfaces
{
    public interface IMediaService
    {
        Task DeleteImageAsync(string url, string folder);
        Task<string> UploadImageAsync(IFormFile file, string folder);
    }
}
