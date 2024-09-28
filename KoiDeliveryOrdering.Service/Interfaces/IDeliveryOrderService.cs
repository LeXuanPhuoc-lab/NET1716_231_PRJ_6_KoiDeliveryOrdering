using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.Business.Interfaces;

public interface IDeliveryOrderService
{
    Task<IServiceResult> FindAsync(Guid deliveryOrderId);
    Task<IServiceResult> FindAllAsync();
    Task<IServiceResult> InsertAsync(DeliveryOrder deliveryOrder);
    Task<IServiceResult> UpdateAsync(DeliveryOrder deliveryOrder);
    Task<IServiceResult> RemoveAsync(Guid deliveryOrderId);
}