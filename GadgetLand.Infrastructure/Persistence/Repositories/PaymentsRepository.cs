using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GadgetLand.Infrastructure.Persistence.Repositories;

public class PaymentsRepository(GadgetLandDbContext dbContext) : BaseRepository<int, Payment>(dbContext), IPaymentsRepository
{
    public async Task<Payment?> GetPaymentByAuthorityAsync(string authority)
    {
        return await dbContext.Payments.Include(payment => payment.PaymentItems).FirstOrDefaultAsync(payment => payment.Authority == authority);
    }
}
