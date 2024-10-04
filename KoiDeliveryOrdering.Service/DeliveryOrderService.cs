using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Contants;
using KoiDeliveryOrdering.Business.Interfaces;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.Data;
using KoiDeliveryOrdering.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KoiDeliveryOrdering.Business;

public class DeliveryOrderService(UnitOfWork unitOfWork) : IDeliveryOrderService
{
    public async Task<IServiceResult> FindAsync(Guid deliveryOrderId)
    {
        try
        {
            var deliveryOrderEntity = await unitOfWork.DeliveryOrderRepository.FindOneWithConditionAsync(
                d => d.DeliveryOrderId == deliveryOrderId);

            if (deliveryOrderEntity == null)
            {
                return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new DeliveryOrder());
            }
            
            return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, deliveryOrderEntity);
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
            var deliveryOrderEntity = await unitOfWork.DeliveryOrderRepository.FindOneWithConditionAsync(
                d => d.Id == id);

            if (deliveryOrderEntity == null)
            {
                return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new DeliveryOrder());
            }

            return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, deliveryOrderEntity);
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
            var deliveryOrderEntities = await unitOfWork.DeliveryOrderRepository.FindAllAsync();

            if (!deliveryOrderEntities.Any())
            {
                return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new List<DeliveryOrder>());
            }

            return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, deliveryOrderEntities.ToList());
        }
        catch (Exception ex)
        {
            return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
        }
    }

    public async Task<IServiceResult> InsertAsync(DeliveryOrder deliveryOrder)
    {
        try
        {
            await unitOfWork.DeliveryOrderRepository.PrepareInsertAsync(deliveryOrder);
            var isCreated = await unitOfWork.DeliveryOrderRepository.SaveChangeWithTransactionAsync() > 0;

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

    public async Task<IServiceResult> UpdateAsync(DeliveryOrder deliveryOrder)
    {
        try
        {
            unitOfWork.DeliveryOrderRepository.PrepareUpdate(deliveryOrder);
            var isUpdated = await unitOfWork.DeliveryOrderRepository.SaveChangeWithTransactionAsync() > 0;

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

    public async Task<IServiceResult> RemoveAsync(Guid deliveryOrderId)
    {
        try
        {
            var deliveryOrderEntity = await unitOfWork.DeliveryOrderRepository.FindOneWithConditionAsync(d => 
                d.DeliveryOrderId == deliveryOrderId);

            // Check exist delivery order
            if (deliveryOrderEntity == null)
            {
                return new ServiceResult(Const.FAIL_REMOVE_CODE, Const.FAIL_REMOVE_MSG, false);
            }

            await unitOfWork.DeliveryOrderRepository.PrepareRemoveAsync(deliveryOrderEntity.Id);
            var isRemoved = await unitOfWork.DeliveryOrderRepository.SaveChangeWithTransactionAsync() > 0;

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

    public async Task<IServiceResult> RemoveAsync(int id)
    {
        try
        {
            var deliveryOrderEntity = await unitOfWork.DeliveryOrderRepository.FindOneWithConditionAsync(d =>
                d.Id == id);

            // Check exist delivery order
            if (deliveryOrderEntity == null)
            {
                return new ServiceResult(Const.FAIL_REMOVE_CODE, Const.FAIL_REMOVE_MSG, false);
            }

            await unitOfWork.DeliveryOrderRepository.PrepareRemoveAsync(deliveryOrderEntity.Id);
            var isRemoved = await unitOfWork.DeliveryOrderRepository.SaveChangeWithTransactionAsync() > 0;

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

    public async Task<IServiceResult> FindAllDeliveryOrderStatusesAsync()
    {
        List<string> orderStatutes = new()
        {
            OrderStatusConstants.Pending,
            OrderStatusConstants.Completed,
            OrderStatusConstants.Canceled,
        };

        return await Task.FromResult(
            new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orderStatutes));
    }

    public async Task<IServiceResult> FindAllAppointmentTimeAsync()
    {
        List<string> deliveryAppointments = new()
        {
            DeliveryAppointmentConstants.AllDay,
            DeliveryAppointmentConstants.MorningTime,
            DeliveryAppointmentConstants.EveningTime,
            DeliveryAppointmentConstants.NightTime,
            DeliveryAppointmentConstants.WithinOfficeHours,
            DeliveryAppointmentConstants.Sunday,
        };

        return await Task.FromResult(
            new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, deliveryAppointments));
    }
}