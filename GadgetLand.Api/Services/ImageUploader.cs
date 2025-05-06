using GadgetLand.Application.Interfaces.Services;

namespace GadgetLand.Api.Services;

public class ImageUploader(IWebHostEnvironment webHostEnvironment) : IImageUploader
{
    public async Task<string> UploadImageAsync(IFormFile image, string directoryName)
    {
        var extension = Path.GetExtension(image.FileName).ToLowerInvariant();

        var directoryPath = Path.Combine(webHostEnvironment.WebRootPath, "images", directoryName);

        if (Directory.Exists(directoryPath) is false) Directory.CreateDirectory(directoryPath);

        var newFileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(Path.Combine(webHostEnvironment.WebRootPath, $"images/{directoryName}"), newFileName);

        await using var fileStream = new FileStream(filePath, FileMode.Create);

        await image.CopyToAsync(fileStream);

        return newFileName;
    }
}
