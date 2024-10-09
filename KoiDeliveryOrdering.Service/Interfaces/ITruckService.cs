using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Service.Interfaces
{
    public interface ITruckService
    {
        Task<IServiceResult> GetAllTrucksAsync();
        Task<IServiceResult> GetTruckByIdAsync(int id);
        Task<IServiceResult> CreateTruckAsync(Truck truck);
        Task<IServiceResult> UpdateTruckAsync(Truck truck);
        Task<IServiceResult> DeleteTruckAsync(int id);
    }
}
