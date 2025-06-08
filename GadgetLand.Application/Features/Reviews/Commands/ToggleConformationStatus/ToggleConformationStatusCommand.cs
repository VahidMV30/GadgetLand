using ErrorOr;
using GadgetLand.Contracts;
using MediatR;

namespace GadgetLand.Application.Features.Reviews.Commands.ToggleConformationStatus;

public record ToggleConformationStatusCommand(int Id) : IRequest<ErrorOr<OperationResponse>>;
