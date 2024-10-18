using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Service.Interfaces
{
    public interface IOrderAssignmentService
    {
        Task<IServiceResult> FindAllAsync();
        Task<IServiceResult> FindAsync(int id);
        Task<IServiceResult> InsertAsync(OrderAssignment orderAssignment);
        Task<IServiceResult> UpdateAsync(OrderAssignment orderAssignment);
        Task<IServiceResult> RemoveAsync(int id);
    }
}
