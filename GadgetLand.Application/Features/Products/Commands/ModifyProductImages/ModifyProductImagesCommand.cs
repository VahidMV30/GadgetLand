using ErrorOr;
using GadgetLand.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GadgetLand.Application.Features.Products.Commands.ModifyProductImages;

public record ModifyProductImagesCommand(int Id, string[] ImagesToRemove, IFormFile[] NewImages) : IRequest<ErrorOr<OperationResponse>>;
