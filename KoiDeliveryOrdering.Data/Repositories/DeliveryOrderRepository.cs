using KoiDeliveryOrdering.Data.Base;
using KoiDeliveryOrdering.Data.Context;
using KoiDeliveryOrdering.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KoiDeliveryOrdering.Data.Repositories;

public class DeliveryOrderRepository : GenericRepository<DeliveryOrder>
{
    public DeliveryOrderRepository(KoiDeliveryOrderingDbContext dbContext)
        : base(dbContext)
    {
    }

    public override async Task<IEnumerable<DeliveryOrder>> FindAllAsync()
    {
        return await _dbSet
            .Include(x => x.SenderInformation)
            .Include(x => x.Payment)
            .Include(x => x.ShippingFee)
            .Include(x => x.Document)
            .ToListAsync();
    }
}