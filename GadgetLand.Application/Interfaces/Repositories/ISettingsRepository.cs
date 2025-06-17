using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Interfaces.Repositories;

public interface ISettingsRepository : IBaseRepository<int, Setting>
{
    Task<Setting?> GetSettingsAsync();
}
