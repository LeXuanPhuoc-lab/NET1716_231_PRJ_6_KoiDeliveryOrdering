using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.Business.Interfaces
{
    public interface IDailyCareScheduleService
    {
        Task<IServiceResult> FindAsync(int id);
        Task<IServiceResult> FindAllAsync();
        Task<IServiceResult> InsertAsync(DailyCareSchedule dailyCareSchedule);
        Task<IServiceResult> UpdateAsync(DailyCareSchedule dailyCareSchedule);
        Task<IServiceResult> RemoveAsync(int id);
    }
}
