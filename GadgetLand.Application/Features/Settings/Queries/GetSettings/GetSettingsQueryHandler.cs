using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Settings;
using MediatR;

namespace GadgetLand.Application.Features.Settings.Queries.GetSettings;

public class GetSettingsQueryHandler(ISettingsRepository settingsRepository, IMapper mapper) : IRequestHandler<GetSettingsQuery, ErrorOr<SettingsResponse>>
{
    public async Task<ErrorOr<SettingsResponse>> Handle(GetSettingsQuery query, CancellationToken cancellationToken)
    {
        var settings = await settingsRepository.GetSettingsAsync();

        if (settings is null) return SettingErrors.NotFound;

        return mapper.Map<SettingsResponse>(settings);
    }
}
