using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.Data;
using KoiDeliveryOrdering.Data.Entities;
using KoiDeliveryOrdering.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Service
{
    public class TruckService : ITruckService
    {
        private readonly UnitOfWork _unitOfWork;

        public TruckService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IServiceResult> FindAllAsync()
        {
            try
            {
                var trucks = await _unitOfWork.TruckRepository.FindAllWithConditionAsync(includeProperties: "Garage");
                return trucks.Any()
                    ? new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, trucks)
                    : new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new List<Truck>());
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
                var truck = await _unitOfWork.TruckRepository.FindOneWithConditionAsync(t => t.TruckId == id);
                return truck != null
                    ? new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, truck)
                    : new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new Truck());
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
            }
        }

        public async Task<IServiceResult> InsertAsync(Truck truck)
        {
            try
            {
                await _unitOfWork.TruckRepository.PrepareInsertAsync(truck);
                var isCreated = await _unitOfWork.TruckRepository.SaveChangeWithTransactionAsync() > 0;
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

        public async Task<IServiceResult> UpdateAsync(Truck truck)
        {
            try
            {
                _unitOfWork.TruckRepository.PrepareUpdate(truck);
                var isUpdated = await _unitOfWork.TruckRepository.SaveChangeWithTransactionAsync() > 0;
                
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

        public async Task<IServiceResult> RemoveAsync(int id)
        {
            try
            {
                var truckEntity = await _unitOfWork.TruckRepository.FindOneWithConditionAsync(d =>
                    d.TruckId.Equals(id));

                if (truckEntity == null)
                {
                    return new ServiceResult(Const.FAIL_REMOVE_CODE, Const.FAIL_REMOVE_MSG, false);
                }

                await _unitOfWork.TruckRepository.PrepareRemoveAsync(truckEntity.TruckId);
                var isRemoved = await _unitOfWork.TruckRepository.SaveChangeWithTransactionAsync() > 0;

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
    }
}
