using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Interfaces;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.Data;
using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.Business
{
    public class CareTaskService : ICareTaskService
    {
        private readonly UnitOfWork unitOfWork;

        public CareTaskService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IServiceResult> FindAllAsync()
        {
            try
            {
                var careTaskEntities = await unitOfWork.CareTaskRepository.FindAllWithConditionAsync();

                if (!careTaskEntities.Any())
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new List<DailyCareSchedule>());
                }

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, careTaskEntities.ToList());
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
            }
        }
    }
}
