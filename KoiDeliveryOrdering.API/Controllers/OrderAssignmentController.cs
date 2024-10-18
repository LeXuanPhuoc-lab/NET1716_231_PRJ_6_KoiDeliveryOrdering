using KoiDeliveryOrdering.API.Payloads.Requests;
using KoiDeliveryOrdering.API.Payloads;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Service;
using KoiDeliveryOrdering.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.API.Controllers
{
    [ApiController]
    public class OrderAssignmentController : ControllerBase
    {
        private readonly IOrderAssignmentService _orderAssignmentService;
        public OrderAssignmentController(IOrderAssignmentService orderAssignmentService)
        {
            _orderAssignmentService = orderAssignmentService;
        }

        [HttpGet(ApiRoute.OrderAssignment.GetById)]
        public async Task<IServiceResult> FindAsync(int id)
        {
            return await _orderAssignmentService.FindAsync(id);
        }

        [HttpGet(ApiRoute.OrderAssignment.GetAll)]
        public async Task<IServiceResult> FindAllAsync()
        {
            return await _orderAssignmentService.FindAllAsync();
        }


        [HttpPost(ApiRoute.OrderAssignment.Insert)]
        public async Task<IServiceResult> CreateAsync([FromBody] OrderAssignmentCreateRequest orderAssignment)
        {
            var OrderAssignmentEntity = orderAssignment.Adapt<OrderAssignment>();
            return await _orderAssignmentService.InsertAsync(OrderAssignmentEntity);
        }

        [HttpPut(ApiRoute.OrderAssignment.Update)]
        public async Task<IServiceResult> UpdateAsync([FromBody] OrderAssignmentUpdateRequest orderAssignment)
        {
            var orderAssignmentEntity = orderAssignment.Adapt<OrderAssignment>();
            return await _orderAssignmentService.UpdateAsync(orderAssignmentEntity);
        }

        [HttpDelete(ApiRoute.OrderAssignment.Remove)]
        public async Task<IServiceResult> RemoveAsync(int id)
        {
            return await _orderAssignmentService.RemoveAsync(id);
        }
    }
}
