using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GadgetLand.Infrastructure.Persistence.Repositories;

public class SettingsRepository(GadgetLandDbContext dbContext) : BaseRepository<int, Setting>(dbContext), ISettingsRepository
{
    public async Task<Setting?> GetSettingsAsync()
    {
        return await dbContext.Settings.FirstOrDefaultAsync();
    }
}
