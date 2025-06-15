using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Interfaces.Repositories;

public interface ICitiesRepository : IBaseRepository<int, City>
{
    Task<IEnumerable<City>> GetCitiesByProvinceIdAsync(int provinceId);
}
