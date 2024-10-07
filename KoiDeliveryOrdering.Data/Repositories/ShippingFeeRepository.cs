using KoiDeliveryOrdering.Data.Base;
using KoiDeliveryOrdering.Data.Context;
using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.Data.Repositories;

public class ShippingFeeRepository : GenericRepository<ShippingFee>
{
    public ShippingFeeRepository(KoiDeliveryOrderingDbContext dbContext) 
        : base(dbContext)
    {
    }
}