using KoiDeliveryOrdering.Data.Base;
using KoiDeliveryOrdering.Data.Context;
using KoiDeliveryOrdering.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KoiDeliveryOrdering.Data.Repositories;

public class DeliveryOrderRepository : GenericRepository<DeliveryOrder>
{
    public DeliveryOrderRepository(KoiDeliveryOrderingDbContext dbContext)
        : base(dbContext)
    {
    }

    public override async Task<DeliveryOrder?> FindOneWithConditionAsync(
        Expression<Func<DeliveryOrder, bool>>? filter, 
        Func<IQueryable<DeliveryOrder>, IOrderedQueryable<DeliveryOrder>>? orderBy = null, 
        string? includeProperties = "")
    {
        var deliveryOrder = await base.FindOneWithConditionAsync(filter, orderBy, includeProperties);
        if (deliveryOrder == null) return null;


        // Include animal for delivery order detail (if any)
        if (deliveryOrder.DeliveryOrderDetails.Any())
        {
            foreach (var d in deliveryOrder.DeliveryOrderDetails)
            {
                d.Animal = await DbContext.Animals.FirstOrDefaultAsync(a => a.Id == d.AnimalId) ?? null!;
            }
        }

        return deliveryOrder;
    }

    public override async Task<IEnumerable<DeliveryOrder>> FindAllAsync()
    {
        return await _dbSet
            .Include(x => x.SenderInformation)
            .Include(x => x.Payment)
            .Include(x => x.ShippingFee)
            .Include(x => x.VoucherPromotion)
            .ToListAsync();
    }
}