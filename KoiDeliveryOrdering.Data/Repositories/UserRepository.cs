using KoiDeliveryOrdering.Data.Base;
using KoiDeliveryOrdering.Data.Context;
using KoiDeliveryOrdering.Data.Entities;
using Microsoft.EntityFrameworkCore;

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

    //  Summary:
    //      This function only use when Authentication functions (Login/Login out) 
    //      are not implemented yet.
    public async Task<IList<SenderInformation>> FindAllSenderInformationAsync()
    {
        return await DbContext.SenderInformations.Include(si => si.User).ToListAsync();
    }

    public async Task<IList<VoucherPromotion>> FindAllVoucherByUsernameAsync(string username)
    {
        var userEntity = await _dbSet
            .Include(x => x.VoucherPromotions)
            .FirstOrDefaultAsync(x => x.Username == username);
        if (userEntity == null) return new List<VoucherPromotion>();

        return userEntity.VoucherPromotions.ToList();
    }
}