using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Service.Interfaces
{
    public interface ITruckService
    {
        Task<IServiceResult> FindAllAsync();
        Task<IServiceResult> FindAsync(int id);
        Task<IServiceResult> InsertAsync(Truck truck);
        Task<IServiceResult> UpdateAsync(Truck truck);
        Task<IServiceResult> RemoveAsync(int id);
    }
}
