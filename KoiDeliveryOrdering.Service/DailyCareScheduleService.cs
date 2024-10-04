using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Interfaces;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.Data;
using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.Business
{
    public class DailyCareScheduleService : IDailyCareScheduleService
    {
        private readonly UnitOfWork unitOfWork;

        public DailyCareScheduleService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IServiceResult> FindAllAsync()
        {
            try
            {
                var deliveryOrderEntities = await unitOfWork.DailyCareScheduleRepository.FindAllWithConditionAsync();

                if (!deliveryOrderEntities.Any())
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new List<DailyCareSchedule>());
                }

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, deliveryOrderEntities.ToList());
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
            }
        }

        public async Task<IServiceResult> FindAsync(int id)
        {
            try
            {
                var dailyCareScheduleEntity = await unitOfWork.DailyCareScheduleRepository.FindOneWithConditionAsync(
                    d => d.DailyCareScheduleId == id);

                if (dailyCareScheduleEntity == null)
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new DailyCareSchedule());
                }

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, dailyCareScheduleEntity);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
            }
        }

        public async Task<IServiceResult> InsertAsync(DailyCareSchedule dailyCareSchedule)
        {
            try
            {
                await unitOfWork.DailyCareScheduleRepository.PrepareInsertAsync(dailyCareSchedule);
                var isCreated = await unitOfWork.DailyCareScheduleRepository.SaveChangeWithTransactionAsync() > 0;

                if (!isCreated)
                {
                    return new ServiceResult(Const.FAIL_INSERT_CODE, Const.FAIL_INSERT_MSG, false);
                }

                return new ServiceResult(Const.SUCCESS_INSERT_CODE, Const.SUCCESS_INSERT_MSG, true);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
            }
        }

        public async Task<IServiceResult> RemoveAsync(int id)
        {
            try
            {
                var dailyCareScheduleEntity = await unitOfWork.DailyCareScheduleRepository.FindOneWithConditionAsync(d =>
                    d.DailyCareScheduleId == id);

                // Check exist daily care schedule
                if (dailyCareScheduleEntity == null)
                {
                    return new ServiceResult(Const.FAIL_REMOVE_CODE, Const.FAIL_REMOVE_MSG, false);
                }

                await unitOfWork.DailyCareScheduleRepository.PrepareRemoveAsync(dailyCareScheduleEntity.DailyCareScheduleId);
                var isRemoved = await unitOfWork.DailyCareScheduleRepository.SaveChangeWithTransactionAsync() > 0;

                if (!isRemoved)
                {
                    return new ServiceResult(Const.FAIL_REMOVE_CODE, Const.FAIL_REMOVE_MSG, false);
                }

                return new ServiceResult(Const.SUCCESS_REMOVE_CODE, Const.SUCCESS_REMOVE_MSG, true);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
            }
        }

        public async Task<IServiceResult> UpdateAsync(DailyCareSchedule dailyCareSchedule)
        {
            try
            {
                unitOfWork.DailyCareScheduleRepository.PrepareUpdate(dailyCareSchedule);
                var isUpdated = await unitOfWork.DailyCareScheduleRepository.SaveChangeWithTransactionAsync() > 0;

                if (!isUpdated)
                {
                    return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, false);
                }

                return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, true);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
            }
        }
    }
}
