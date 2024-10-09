using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.Business.Interfaces;

public interface IPaymentService
{
    Task<IServiceResult> FindAllAsync();
}