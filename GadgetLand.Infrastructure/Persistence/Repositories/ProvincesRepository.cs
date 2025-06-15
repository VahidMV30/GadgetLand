using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Domain.Entities;

namespace GadgetLand.Infrastructure.Persistence.Repositories;

public class ProvincesRepository(GadgetLandDbContext dbContext) : BaseRepository<int, Province>(dbContext), IProvincesRepository
{

}
