using KoiDeliveryOrdering.Data.Base;
using KoiDeliveryOrdering.Data.Context;
using KoiDeliveryOrdering.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Data.Repositories
{
    public class GarageRepository : GenericRepository<Garage>
    {
        public GarageRepository(KoiDeliveryOrderingDbContext dbContext)
        : base(dbContext)
        {
        }
    }
}
