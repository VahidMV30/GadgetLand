using Microsoft.AspNetCore.Http;

namespace GadgetLand.Contracts.Products;

public record ModifyProductImagesRequest(int Id, string[] ImagesToRemove, IFormFile[] NewImages);
