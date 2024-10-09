using KoiDeliveryOrdering.Data.Base;
using KoiDeliveryOrdering.Data.Context;
using KoiDeliveryOrdering.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Data.Repositories
{
    public class DocumentRepository : GenericRepository<Document>
    {
        public DocumentRepository(KoiDeliveryOrderingDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
