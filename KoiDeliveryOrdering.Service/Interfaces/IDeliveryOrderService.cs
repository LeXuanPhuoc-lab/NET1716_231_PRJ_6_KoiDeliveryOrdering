using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.Business.Interfaces;

public interface IDeliveryOrderService
{
    Task<IServiceResult> FindAsync(Guid deliveryOrderId);
    Task<IServiceResult> FindAsync(int id);
    Task<IServiceResult> FindAllAsync();
    Task<IServiceResult> FindAllDeliveryOrderStatusesAsync();
    Task<IServiceResult> FindAllAppointmentTimeAsync();
    Task<IServiceResult> InsertAsync(DeliveryOrder deliveryOrder);
    Task<IServiceResult> UpdateAsync(DeliveryOrder deliveryOrder);
    Task<IServiceResult> RemoveAsync(Guid deliveryOrderId);
    Task<IServiceResult> RemoveAsync(int id);
}