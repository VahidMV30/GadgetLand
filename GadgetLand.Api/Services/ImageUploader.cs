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

    public async Task<IEnumerable<string>> UploadImagesAsync(IFormFile[] images, string directoryName)
    {
        var uploadedFileNames = new List<string>();

        var directoryPath = Path.Combine(webHostEnvironment.WebRootPath, "images", directoryName);

        if (Directory.Exists(directoryPath) is false) Directory.CreateDirectory(directoryPath);

        foreach (var image in images)
        {
            if (image.Length == 0) continue;

            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();
            var newFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(Path.Combine(webHostEnvironment.WebRootPath, $"images/{directoryName}"), newFileName);

            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(fileStream);

            uploadedFileNames.Add(newFileName);
        }

        return uploadedFileNames;
    }

    public void DeleteImage(string fileName, string directoryName)
    {
        var filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", directoryName, fileName);

        if (File.Exists(filePath)) File.Delete(filePath);
    }
}
