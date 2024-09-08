using KoiDeliveryOrdering.Data.Base;
using KoiDeliveryOrdering.Data.Context;
using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.Data.Repositories;

public class UserRepository : GenericRepository<User>
{
    public UserRepository(KoiDeliveryOrderingDbContext dbContext) 
        : base(dbContext)
    {
    }
    
    // Custom more repository functions here...
    // Using _dbSet property inherited from GenericRepository<T>
    // NOT recommend access directly to DbContext 
}