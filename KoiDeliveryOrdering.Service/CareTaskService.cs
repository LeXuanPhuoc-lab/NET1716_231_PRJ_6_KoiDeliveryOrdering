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
                    return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new List<CareTask>());
                }

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, careTaskEntities.ToList());
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
                var careTaskEntity = await unitOfWork.CareTaskRepository.FindOneWithConditionAsync(
                    d => d.CareTaskId == id);

                if (careTaskEntity == null)
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new CareTask());
                }

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, careTaskEntity);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
            }
        }

        public async Task<IServiceResult> InsertAsync(CareTask careTask)
        {
            try
            {
                await unitOfWork.CareTaskRepository.PrepareInsertAsync(careTask);
                var isCreated = await unitOfWork.CareTaskRepository.SaveChangeWithTransactionAsync() > 0;

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
                var careTaskEntity = await unitOfWork.CareTaskRepository.FindOneWithConditionAsync(d =>
                    d.CareTaskId == id);

                // Check exist daily care schedule
                if (careTaskEntity == null)
                {
                    return new ServiceResult(Const.FAIL_REMOVE_CODE, Const.FAIL_REMOVE_MSG, false);
                }

                await unitOfWork.CareTaskRepository.PrepareRemoveAsync(careTaskEntity.CareTaskId);
                var isRemoved = await unitOfWork.CareTaskRepository.SaveChangeWithTransactionAsync() > 0;

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

        public async Task<IServiceResult> UpdateAsync(CareTask careTask)
        {
            try
            {
                unitOfWork.CareTaskRepository.PrepareUpdate(careTask);
                var isUpdated = await unitOfWork.CareTaskRepository.SaveChangeWithTransactionAsync() > 0;

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
