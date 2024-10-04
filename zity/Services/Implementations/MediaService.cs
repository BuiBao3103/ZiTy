using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using zity.ExceptionHandling;
using zity.Services.Interfaces;

namespace zity.Services.Implementations
{
    public class MediaService(Cloudinary cloudinary) : IMediaService
    {
        private readonly Cloudinary _cloudinary = cloudinary;

        public async Task DeleteImageAsync(string url, string folder)
        {
            if (!string.IsNullOrEmpty(url))
            {
                var publicId = GetPublicIdFromUrl(url);
                var deletionParams = new DeletionParams(folder + publicId);
                var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

                if (deletionResult.Result != "ok")
                {
                    throw new AppError("Failed to delete the old image.");
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
                throw new AppError(uploadResult.Error.Message);
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
}
