using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.Service.Interfaces
{
    public interface ICareTaskService
    {
        Task<IServiceResult> FindAllAsync();
        Task<IServiceResult> FindAsync(int id);
        Task<IServiceResult> InsertAsync(CareTask careTask);
        Task<IServiceResult> UpdateAsync(CareTask careTask);
        Task<IServiceResult> RemoveAsync(int id);
    }
}
