using KoiDeliveryOrdering.API.Payloads;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Interfaces;
using KoiDeliveryOrdering.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrdering.API.Controllers
{
    [ApiController]
    public class DeliveryOrderController : ControllerBase
    {
        private readonly IDeliveryOrderService _deliveryOrderService;

        public DeliveryOrderController(IDeliveryOrderService deliveryOrderService)
        {
            _deliveryOrderService = deliveryOrderService;
        }

        [HttpGet(ApiRoute.DeliveryOrder.GetAll)]
        public async Task<IServiceResult> GetAllDeliveryOrderAsync()
        {
            return await _deliveryOrderService.FindAllAsync();
        }

        [HttpGet(ApiRoute.DeliveryOrder.GetById)]
        public async Task<IServiceResult> GetDeliveryOrderByIdAsync([FromRoute] Guid id)
        {
            return await _deliveryOrderService.FindAsync(id);
        }

        [HttpPost(ApiRoute.DeliveryOrder.Insert)]
        public async Task<IServiceResult> InsertDeliveryOrderAsync([FromBody] DeliveryOrder req)
        {
            return await _deliveryOrderService.InsertAsync(req);
        }

        [HttpPut(ApiRoute.DeliveryOrder.Update)]
        public async Task<IServiceResult> UpdateDeliveryOrderAsync([FromBody] DeliveryOrder req)
        {
            return await _deliveryOrderService.UpdateAsync(req);
        }

        [HttpDelete(ApiRoute.DeliveryOrder.Remove)]
        public async Task<IServiceResult> RemoveDeliveryOrderAsync([FromRoute] Guid id)
        {
            return await _deliveryOrderService.RemoveAsync(id);
        }

    }
}
