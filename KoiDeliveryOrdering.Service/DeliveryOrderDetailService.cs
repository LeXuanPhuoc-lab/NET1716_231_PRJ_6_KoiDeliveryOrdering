using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Interfaces;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.Data;
using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.Business
{
    public class DeliveryOrderDetailService : IDeliveryOrderDetailService
    {
        private readonly UnitOfWork unitOfWork;

        public DeliveryOrderDetailService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IServiceResult> FindAllAsync()
        {
            try
            {
                var deliveryOrderDetailEntities = await unitOfWork.DeliveryOrderDetailRepository.FindAllAsync();

                if (!deliveryOrderDetailEntities.Any())
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new List<DeliveryOrderDetail>());
                }

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, deliveryOrderDetailEntities.ToList());
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
            }
        }
    }
}
