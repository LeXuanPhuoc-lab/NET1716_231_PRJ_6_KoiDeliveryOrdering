using KoiDeliveryOrdering.Data.Base;
using KoiDeliveryOrdering.Data.Context;
using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.Data.Repositories
{
    public class DailyCareScheduleRepository : GenericRepository<DailyCareSchedule>
    {
        public DailyCareScheduleRepository(KoiDeliveryOrderingDbContext dbContext) : base(dbContext)
        {
        }
    }
}
