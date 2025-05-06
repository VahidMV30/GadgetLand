using ErrorOr;
using GadgetLand.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GadgetLand.Application.Features.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name, string Slug, IFormFile Image) : IRequest<ErrorOr<OperationResponse>>;
