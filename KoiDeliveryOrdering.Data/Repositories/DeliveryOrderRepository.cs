using KoiDeliveryOrdering.Data.Base;
using KoiDeliveryOrdering.Data.Context;
using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.Data.Repositories;

public class DeliveryOrderRepository : GenericRepository<DeliveryOrder>
{
    public DeliveryOrderRepository(KoiDeliveryOrderingDbContext dbContext)
        : base(dbContext)
    {
    }
}