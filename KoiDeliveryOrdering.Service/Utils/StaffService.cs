using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.Data;
using KoiDeliveryOrdering.Data.Entities;
using KoiDeliveryOrdering.Service.Interfaces;

namespace KoiDeliveryOrdering.Service.Utils
{
    public class StaffService : IStaffService
    {
        private readonly UnitOfWork unitOfWork;

        public StaffService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IServiceResult> FindAllAsync()
        {
            try
            {
                var staffEntities = await unitOfWork.StaffRepository.FindAllWithConditionAsync();

                if (!staffEntities.Any())
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new List<Staff>());
                }

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, staffEntities.ToList());
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
            }
        }
    }
}
