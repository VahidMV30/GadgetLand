using ErrorOr;
using GadgetLand.Contracts.Settings;
using MediatR;

namespace GadgetLand.Application.Features.Settings.Queries.GetSettings;

public record GetSettingsQuery() : IRequest<ErrorOr<SettingsResponse>>;
