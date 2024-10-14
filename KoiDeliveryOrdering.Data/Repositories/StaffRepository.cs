using KoiDeliveryOrdering.Data.Base;
using KoiDeliveryOrdering.Data.Context;
using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.Data.Repositories
{
    public class StaffRepository : GenericRepository<Staff>
    {
        public StaffRepository(KoiDeliveryOrderingDbContext dbContext) : base(dbContext)
        {
        }
    }
}
