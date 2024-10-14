using KoiDeliveryOrdering.Business.Base;

namespace KoiDeliveryOrdering.Service.Interfaces
{
    public interface IStaffService
    {
        Task<IServiceResult> FindAllAsync();
    }
}
