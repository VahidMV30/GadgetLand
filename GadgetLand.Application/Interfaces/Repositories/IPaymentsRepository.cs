using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Interfaces.Repositories;

public interface IPaymentsRepository : IBaseRepository<int, Payment>
{
    Task<Payment?> GetPaymentByAuthorityAsync(string authority);
}
