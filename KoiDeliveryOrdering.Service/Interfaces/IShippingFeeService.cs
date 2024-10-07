using KoiDeliveryOrdering.Business.Base;

namespace KoiDeliveryOrdering.Business.Interfaces;

public interface IShippingFeeService
{
    Task<IServiceResult> FindAllAsync();
}