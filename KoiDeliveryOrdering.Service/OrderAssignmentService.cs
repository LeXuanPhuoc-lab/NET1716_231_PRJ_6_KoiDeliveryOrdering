using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.Data;
using KoiDeliveryOrdering.Data.Context;
using KoiDeliveryOrdering.Data.Entities;
using KoiDeliveryOrdering.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Service
{
    public class OrderAssignmentService : IOrderAssignmentService
    {
        private readonly UnitOfWork _unitOfWork;

        public OrderAssignmentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IServiceResult> FindAllAsync()
        {
            try
            {
                var orderAssignment = await _unitOfWork.OrderAssignmentRepository.FindAllWithConditionAsync(includeProperties: "AssignedTruck,DeliveryOrder,Driver,FishCarer");
                return orderAssignment.Any()
                    ? new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orderAssignment)
                    : new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new List<OrderAssignment>());
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
                var orderAssignment = await _unitOfWork.OrderAssignmentRepository.FindOneWithConditionAsync(t => t.OrderAssignmentId == id, includeProperties: "AssignedTruck,DeliveryOrder,Driver,FishCarer");
                return orderAssignment != null
                    ? new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orderAssignment)
                    : new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new OrderAssignment());
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
            }
        }

        public async Task<IServiceResult> InsertAsync(OrderAssignment orderAssignment)
        {
            try
            {
                await _unitOfWork.OrderAssignmentRepository.PrepareInsertAsync(orderAssignment);
                var isCreated = await _unitOfWork.OrderAssignmentRepository.SaveChangeWithTransactionAsync() > 0;

                if (!isCreated)
                {
                    return new ServiceResult(Const.FAIL_INSERT_CODE, Const.FAIL_INSERT_MSG, false);
                }

                return new ServiceResult(Const.SUCCESS_INSERT_CODE, Const.SUCCESS_INSERT_MSG, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
            }
        }

        public async Task<IServiceResult> UpdateAsync(OrderAssignment orderAssignment)
        {
            try
            {
                _unitOfWork.OrderAssignmentRepository.PrepareUpdate(orderAssignment);
                var isUpdated = await _unitOfWork.OrderAssignmentRepository.SaveChangeWithTransactionAsync() > 0;

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
                var orderAssignment = await _unitOfWork.OrderAssignmentRepository.FindOneWithConditionAsync(d =>
                    d.OrderAssignmentId.Equals(id));

                if (orderAssignment == null)
                {
                    return new ServiceResult(Const.FAIL_REMOVE_CODE, Const.FAIL_REMOVE_MSG, false);
                }

                await _unitOfWork.OrderAssignmentRepository.PrepareRemoveAsync(orderAssignment.OrderAssignmentId);
                var isRemoved = await _unitOfWork.OrderAssignmentRepository.SaveChangeWithTransactionAsync() > 0;

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
