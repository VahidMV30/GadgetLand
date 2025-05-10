using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Domain.Entities;

namespace GadgetLand.Infrastructure.Persistence.Repositories;

public class BrandsRepository(GadgetLandDbContext dbContext) : BaseRepository<int, Brand>(dbContext), IBrandsRepository
{

}
