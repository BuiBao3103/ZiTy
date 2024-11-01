using Application.Core.Services;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using MyApp.Domain.Configurations;

namespace Infrastructure.Services;

public class MediaService : IMediaService
{
    private Cloudinary _cloudinary;
    private readonly AppSettings _appSettings;

    MediaService(AppSettings appSettings)
    {
        _appSettings = appSettings;
        var account = new Account(
            _appSettings.CloudinarySettings.CloudName,
            _appSettings.CloudinarySettings.ApiKey,
            _appSettings.CloudinarySettings.ApiSecret
        );
        _cloudinary = new Cloudinary(account);
    }

    public async Task DeleteImageAsync(string url, string folder)
    {
        if (!string.IsNullOrEmpty(url))
        {
            var publicId = GetPublicIdFromUrl(url);
            var deletionParams = new DeletionParams(folder + publicId);
            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

            if (deletionResult.Result != "ok")
            {
                //throw new AppError("Failed to delete the old image.", StatusCodes.Status500InternalServerError, "DELETE_IMAGE_FAILED");
            }
        }
    }

    public async Task<string> UploadImageAsync(IFormFile file, string folder)
    {
        using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Folder = folder,
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.Error != null)
        {
            //throw new AppError(uploadResult.Error.Message, StatusCodes.Status500InternalServerError, "UPLOAD_IMAGE_FAILED");
        }

        return uploadResult.SecureUrl.ToString();
    }

    private static string GetPublicIdFromUrl(string url)
    {
        var uri = new Uri(url);
        var segments = uri.Segments;
        var publicIdWithExtension = segments[^1];
        var publicId = Path.GetFileNameWithoutExtension(publicIdWithExtension);
        return publicId;
    }
}
