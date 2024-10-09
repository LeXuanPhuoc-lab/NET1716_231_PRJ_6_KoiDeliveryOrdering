using KoiDeliveryOrdering.Business.Base;

namespace KoiDeliveryOrdering.Business.Interfaces
{
    public interface IDeliveryOrderDetailService
    {
        Task<IServiceResult> FindAllAsync();
    }
}
