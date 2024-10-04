using KoiDeliveryOrdering.Business.Base;

namespace KoiDeliveryOrdering.Business.Interfaces
{
    public interface ICareTaskService
    {
        Task<IServiceResult> FindAllAsync();
    }
}
