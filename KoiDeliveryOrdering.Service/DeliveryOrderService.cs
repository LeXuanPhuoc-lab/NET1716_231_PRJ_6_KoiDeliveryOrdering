using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Contants;
using KoiDeliveryOrdering.Business.Interfaces;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.Data;
using KoiDeliveryOrdering.Data.Entities;
using KoiDeliveryOrdering.Service.Utils;

namespace KoiDeliveryOrdering.Business;

public class DeliveryOrderService(UnitOfWork unitOfWork, 
    IShippingFeeService shippingFeeService,
    IAnimalService animalService) : IDeliveryOrderService
{
    public async Task<IServiceResult> FindAsync(Guid deliveryOrderId)
    {
        try
        {
            var deliveryOrderEntity = await unitOfWork.DeliveryOrderRepository.FindOneWithConditionAsync(
                filter: d => d.DeliveryOrderId == deliveryOrderId,
                orderBy: null,
                includeProperties: "SenderInformation,Documents,Payment,ShippingFee,VoucherPromotion");

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
                filter: d => d.Id == id,
                orderBy: null,
                includeProperties: "SenderInformation,Documents,Payment,ShippingFee,VoucherPromotion,DeliveryOrderDetails");

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
            // Get all shipping fees
            var shippingFeeResult = await shippingFeeService.FindAllAsync();
            var shippingFees = shippingFeeResult.Status == Const.SUCCESS_READ_CODE 
                ? shippingFeeResult.Data as List<ShippingFee>
                : new List<ShippingFee>();

            // Assump delivery date
            deliveryOrder.DeliveryDate = DeliveryDateHelper.AssumpDeliveryDate(
                deliveryOrder.ShippingFeeId, shippingFees!, deliveryOrder.CreateDate);

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
            var deliveryOrderEntity = await unitOfWork.DeliveryOrderRepository.FindOneWithConditionAsync(
                filter: d => d.Id == deliveryOrder.Id,
                orderBy: null,
                includeProperties: "SenderInformation,Documents,Payment,ShippingFee,VoucherPromotion,DeliveryOrderDetails");

                if (deliveryOrderEntity != null)
                {
                    // Progress update delivery order properties
                    deliveryOrderEntity.RecipientName = deliveryOrder.RecipientName;
                    deliveryOrderEntity.RecipientPhone = deliveryOrder.RecipientPhone;
                    deliveryOrderEntity.RecipientAddress = deliveryOrder.RecipientAddress;
                    deliveryOrderEntity.RecipientLongitude = deliveryOrder.RecipientLongitude;
                    deliveryOrderEntity.RecipientLatitude = deliveryOrder.RecipientLatitude;
                    deliveryOrderEntity.RecipientAppointmentTime = deliveryOrder.RecipientAppointmentTime;
                    deliveryOrderEntity.CreateDate = deliveryOrder.CreateDate;
                    deliveryOrderEntity.DeliveryDate = deliveryOrder.DeliveryDate;
                    deliveryOrderEntity.OrderStatus = deliveryOrder.OrderStatus;
                    deliveryOrderEntity.TotalAmount = deliveryOrder.TotalAmount;
                    deliveryOrderEntity.TaxFee = deliveryOrder.TaxFee;
                    deliveryOrderEntity.PaymentId = deliveryOrder.PaymentId;
                    deliveryOrderEntity.IsPurchased = deliveryOrder.IsPurchased;
                    deliveryOrderEntity.IsSenderPurchase = deliveryOrder.IsSenderPurchase;
                    deliveryOrderEntity.IsInternational = deliveryOrder.IsInternational;
                    deliveryOrderEntity.VoucherPromotionId = deliveryOrder.VoucherPromotionId;
                    deliveryOrderEntity.ShippingFeeId = deliveryOrder.ShippingFeeId;
                    deliveryOrderEntity.SenderInformationId = deliveryOrder.SenderInformationId;

                    // Get all shipping fees
                    var shippingFeeResult = await shippingFeeService.FindAllAsync();
                    var shippingFees = shippingFeeResult.Status == Const.SUCCESS_READ_CODE
                        ? shippingFeeResult.Data as List<ShippingFee>
                        : new List<ShippingFee>();
                    // Assump delivery date
                    deliveryOrderEntity.DeliveryDate = DeliveryDateHelper.AssumpDeliveryDate(
                            deliveryOrder.ShippingFeeId, shippingFees!, deliveryOrder.CreateDate);

                    // Update delivery order details (if any)
                    if (deliveryOrder.DeliveryOrderDetails.Any())
                    {
                        // Iterate delivery order details list
                        foreach (var dOrder in deliveryOrderEntity.DeliveryOrderDetails)
                        {
                            // Progress update <- Check if exist 
                            if (deliveryOrder.DeliveryOrderDetails.FirstOrDefault(
                                x => x.Id == dOrder.Id) is var toUpdateOrder && toUpdateOrder != null)
                            {
                                dOrder.PreDeliveryHealthStatus = toUpdateOrder.PreDeliveryHealthStatus;
                                dOrder.PostDeliveryHealthStatus = toUpdateOrder.PostDeliveryHealthStatus;

                                // Update animal
                                await animalService.UpdateAsync(toUpdateOrder.Animal);
                            }
                        }

                        // Get all existing order detail ids
                        var updateOrderDetailIds = deliveryOrder.DeliveryOrderDetails
                            .Select(x => x.Id).ToList();
                        // Perform remove (if any)
                        var toRemoveItems = deliveryOrderEntity.DeliveryOrderDetails
                            .Where(x => !updateOrderDetailIds.Contains(x.Id))
                            .ToList();
                        // Remove items from the original collection
                        foreach (var item in toRemoveItems)
                        {
                            var toRemoveAnimalId = item.AnimalId;
                            deliveryOrderEntity.DeliveryOrderDetails.Remove(item);

                            // Perform remove animal (as no data tracking)
                            await animalService.RemoveAsync(toRemoveAnimalId);
                        }

                        // Get all existing order detail ids
                        var existingOrderDetailIds = deliveryOrderEntity.DeliveryOrderDetails
                            .Select(x => x.Id).ToList();
                        // Check for addtional items
                        foreach (var item in deliveryOrder.DeliveryOrderDetails)
                        {
                            // Add new <- Not exist
                            if (!existingOrderDetailIds.Contains(item.Id))
                            {
                                deliveryOrderEntity.DeliveryOrderDetails.Add(item);
                            }
                        }
                    }

                    unitOfWork.DeliveryOrderRepository.PrepareUpdate(deliveryOrderEntity);
                    var isUpdated = await unitOfWork.DeliveryOrderRepository.SaveChangeWithTransactionAsync() > 0;

                    if (!isUpdated)
                    {
                        return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, false);
                    }

                    return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, true);
                }

            return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, false);

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
            var deliveryOrderEntity = await unitOfWork.DeliveryOrderRepository.FindOneWithConditionAsync(
                filter: d => d.Id == id,
                orderBy: null,
                includeProperties: "SenderInformation,Documents,Payment,ShippingFee,VoucherPromotion,DeliveryOrderDetails");

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