using ErrorOr;
using GadgetLand.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GadgetLand.Application.Features.Brands.Commands.CreateBrand;

public record CreateBrandCommand(string Name, string Slug, IFormFile Image) : IRequest<ErrorOr<OperationResponse>>;
