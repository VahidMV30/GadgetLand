using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Domain.Entities;

namespace GadgetLand.Infrastructure.Persistence.Repositories;

public class CategoriesRepository(GadgetLandDbContext dbContext) : BaseRepository<int, Category>(dbContext), ICategoriesRepository
{

}
