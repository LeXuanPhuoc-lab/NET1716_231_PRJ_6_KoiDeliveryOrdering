using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Interfaces;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.Data;
using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.Business;

public class PaymentService(UnitOfWork unitOfWork) : IPaymentService
{
    public async Task<IServiceResult> FindAllAsync()
    {
        try
        {
            var paymentMethods = await unitOfWork.PaymentRepository.FindAllAsync();

            if (paymentMethods.Any())
            {
                // Load success
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, paymentMethods);
            }
            
            // Fail to load
            return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new List<Payment>());
        }
        catch (Exception ex)
        {
            return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
        }
    }
}