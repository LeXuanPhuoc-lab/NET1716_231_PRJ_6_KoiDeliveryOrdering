using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Service.Interfaces
{
    public interface IGarageService
    {
        Task<IServiceResult> FindAsync(int id);
        Task<IServiceResult> FindAllAsync();
        Task<IServiceResult> InsertAsync(Garage garage);
        Task<IServiceResult> UpdateAsync(Garage garage);
        Task<IServiceResult> RemoveAsync(int id);
    }
}
