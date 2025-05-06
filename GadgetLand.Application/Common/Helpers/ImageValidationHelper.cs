using Microsoft.AspNetCore.Http;

namespace GadgetLand.Application.Common.Helpers;

public static class ImageValidationHelper
{
    public static bool BeAValidImageType(IFormFile? image)
    {
        if (image is null || image.Length is 0) return true;

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        var fileExtension = Path.GetExtension(image.FileName).ToLowerInvariant();

        return allowedExtensions.Contains(fileExtension);
    }

    public static bool BeUnder1MB(IFormFile? image)
    {
        if (image is null || image.Length is 0) return true;

        const int maxSizeInBytes = 1 * 1024 * 1024; // 1MB
        return image.Length <= maxSizeInBytes;
    }
}
