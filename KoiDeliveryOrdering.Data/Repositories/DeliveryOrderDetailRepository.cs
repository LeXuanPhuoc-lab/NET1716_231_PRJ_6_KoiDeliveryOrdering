using KoiDeliveryOrdering.Data.Base;
using KoiDeliveryOrdering.Data.Context;
using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.Data.Repositories
{
    public class DeliveryOrderDetailRepository : GenericRepository<DeliveryOrderDetail>
    {
        public DeliveryOrderDetailRepository(KoiDeliveryOrderingDbContext dbContext) : base(dbContext)
        {
        }
    }
}
