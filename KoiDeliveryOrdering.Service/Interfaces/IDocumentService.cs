using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Business.Interfaces
{
    public interface IDocumentService
    {
        Task<ServiceResult> GetAllAsync();

	}
}
