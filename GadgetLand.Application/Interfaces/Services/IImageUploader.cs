using Microsoft.AspNetCore.Http;

namespace GadgetLand.Application.Interfaces.Services;

public interface IImageUploader
{
    Task<string> UploadImageAsync(IFormFile image, string directoryName);
}
