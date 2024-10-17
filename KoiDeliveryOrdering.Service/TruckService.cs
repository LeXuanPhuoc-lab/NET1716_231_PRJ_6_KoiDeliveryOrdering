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
                var trucks = await _unitOfWork.TruckRepository.FindAllWithConditionAsync();
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
                var existingTruck = await _unitOfWork.TruckRepository.FindOneWithConditionAsync(t => t.TruckId == truck.TruckId);
                if (existingTruck == null)
                {
                    return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }

                existingTruck.Model = truck.Model; // Update properties as needed
                await _unitOfWork.TruckRepository.UpdateAsync(existingTruck, saveChanges: false);
                var isUpdated = await _unitOfWork.TruckRepository.SaveChangeWithTransactionAsync() > 0;

                return isUpdated
                    ? new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG)
                    : new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
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
                var truck = await _unitOfWork.TruckRepository.FindOneWithConditionAsync(t => t.TruckId == id);
                if (truck == null)
                {
                    return new ServiceResult(Const.FAIL_REMOVE_CODE, Const.FAIL_REMOVE_MSG);
                }

                await _unitOfWork.TruckRepository.PrepareRemoveAsync(truck);
                var isRemoved = await _unitOfWork.TruckRepository.SaveChangeWithTransactionAsync() > 0;

                return isRemoved
                    ? new ServiceResult(Const.SUCCESS_REMOVE_CODE, Const.SUCCESS_REMOVE_MSG)
                    : new ServiceResult(Const.FAIL_REMOVE_CODE, Const.FAIL_REMOVE_MSG);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
            }
        }
    }
}
