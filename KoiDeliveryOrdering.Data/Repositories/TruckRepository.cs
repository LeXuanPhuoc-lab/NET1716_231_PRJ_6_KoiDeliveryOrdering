using KoiDeliveryOrdering.Data.Base;
using KoiDeliveryOrdering.Data.Context;
using KoiDeliveryOrdering.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Data.Repositories
{
    public class TruckRepository : GenericRepository<Truck>
    {
        public TruckRepository(KoiDeliveryOrderingDbContext dbContext)
            : base(dbContext)
        {
        }
}
}
