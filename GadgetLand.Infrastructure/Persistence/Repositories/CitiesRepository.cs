using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GadgetLand.Infrastructure.Persistence.Repositories;

public class CitiesRepository(GadgetLandDbContext dbContext) : BaseRepository<int, City>(dbContext), ICitiesRepository
{
    public async Task<IEnumerable<City>> GetCitiesByProvinceIdAsync(int provinceId)
    {
        return await dbContext.Cities.Where(x => x.ProvinceId == provinceId).ToListAsync();
    }
}
