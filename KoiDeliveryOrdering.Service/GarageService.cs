using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.Data;
using KoiDeliveryOrdering.Data.Entities;
using KoiDeliveryOrdering.Service.Interfaces;

namespace KoiDeliveryOrdering.Service
{
    public class GarageService(UnitOfWork unitOfWork) : IGarageService
    {
        public async Task<IServiceResult> FindAsync(int id)
        {
            try
            {
                var garageEntity = await unitOfWork.GarageRepository.FindOneWithConditionAsync(
                    d => d.GarageId.Equals(id));

                if (garageEntity == null)
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new Garage());
                }

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, garageEntity);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
            }
        }

        public async Task<IServiceResult> FindAllAsync()
        {
            try
            {
                var garageEntities = await unitOfWork.GarageRepository.FindAllAsync();

                if (!garageEntities.Any())
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new List<Garage>());
                }

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, garageEntities.ToList());
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
            }
        }

        public async Task<IServiceResult> InsertAsync(Garage garage)
        {
            try
            {
                await unitOfWork.GarageRepository.PrepareInsertAsync(garage);
                var isCreated = await unitOfWork.GarageRepository.SaveChangeWithTransactionAsync() > 0;

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
                var garageEntity = await unitOfWork.GarageRepository.FindOneWithConditionAsync(d =>
                    d.GarageId.Equals(id));

                if (garageEntity == null)
                {
                    return new ServiceResult(Const.FAIL_REMOVE_CODE, Const.FAIL_REMOVE_MSG, false);
                }

                await unitOfWork.GarageRepository.PrepareRemoveAsync(garageEntity.GarageId);
                var isRemoved = await unitOfWork.GarageRepository.SaveChangeWithTransactionAsync() > 0;

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

        public async Task<IServiceResult> UpdateAsync(Garage garage)
        {
            try
            {
                unitOfWork.GarageRepository.PrepareUpdate(garage);
                var isUpdated = await unitOfWork.GarageRepository.SaveChangeWithTransactionAsync() > 0;

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
