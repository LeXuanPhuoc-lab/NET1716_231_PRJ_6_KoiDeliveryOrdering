using KoiDeliveryOrdering.Data.Base;
using KoiDeliveryOrdering.Data.Context;
using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.Data.Repositories
{
    public class CareTaskRepository : GenericRepository<CareTask>
    {
        public CareTaskRepository(KoiDeliveryOrderingDbContext dbContext) : base(dbContext)
        {
        }
    }
}
