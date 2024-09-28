using KoiDeliveryOrdering.Data.Context;
using KoiDeliveryOrdering.Data.Repositories;

namespace KoiDeliveryOrdering.Data;

public class UnitOfWork(KoiDeliveryOrderingDbContext unitOfWorkContext) : IDisposable
{
    private UserRepository _userRepository = null!;
    private DeliveryOrderRepository _deliveryOrderRepository = null!;
    
    public UserRepository UserRepository
        // New instance is require, as an application not define abstractions for 
        // repositories to utilizing [Service Lifetime in ASP.NET Core] 
        => _userRepository ??= new (unitOfWorkContext);
    
    public DeliveryOrderRepository DeliveryOrderRepository
        => _deliveryOrderRepository ??= new (unitOfWorkContext);
    
    #region Diposable 
    private bool _disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                unitOfWorkContext.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    #endregion
}