using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Domain.Entities;

namespace GadgetLand.Infrastructure.Persistence.Repositories;

public class OrdersRepository(GadgetLandDbContext dbContext) : BaseRepository<int, Order>(dbContext), IOrdersRepository
{

}
