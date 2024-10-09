using KoiDeliveryOrdering.Data.Base;
using KoiDeliveryOrdering.Data.Context;
using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.Data.Repositories;

public class PaymentRepository : GenericRepository<Payment>
{
    public PaymentRepository(KoiDeliveryOrderingDbContext dbContext) 
        : base(dbContext)
    {
    }
}