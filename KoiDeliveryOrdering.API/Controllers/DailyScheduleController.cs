using KoiDeliveryOrdering.API.Payloads;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Interfaces;
using KoiDeliveryOrdering.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrdering.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyScheduleController : ControllerBase
    {
        private readonly IDailyCareScheduleService dailyCareScheduleService;
        private readonly IDeliveryOrderDetailService deliveryOrderDetailService;
        private readonly ICareTaskService careTaskService;

        public DailyScheduleController(IDailyCareScheduleService dailyCareScheduleService, IDeliveryOrderDetailService deliveryOrderDetailService, ICareTaskService careTaskService)
        {
            this.dailyCareScheduleService = dailyCareScheduleService;
            this.deliveryOrderDetailService = deliveryOrderDetailService;
            this.careTaskService = careTaskService;
        }

        [HttpGet(ApiRoute.DailyCareSchedule.GetAll)]
        public async Task<IServiceResult> GetAllDailyCareScheduleAsync()
        {
            return await dailyCareScheduleService.FindAllAsync();
        }

        [HttpGet(ApiRoute.DailyCareSchedule.GetById)]
        public async Task<IServiceResult> GetDailyCareScheduleByIdAsync([FromRoute] int id)
        {
            return await dailyCareScheduleService.FindAsync(id);
        }

        [HttpPost(ApiRoute.DailyCareSchedule.Insert)]
        public async Task<IServiceResult> InsertDailyCareScheduleAsync([FromBody] DailyCareSchedule req)
        {
            return await dailyCareScheduleService.InsertAsync(req);
        }

        [HttpPut(ApiRoute.DailyCareSchedule.Update)]
        public async Task<IServiceResult> UpdateDailyCareScheduleAsync([FromBody] DailyCareSchedule req)
        {
            return await dailyCareScheduleService.UpdateAsync(req);
        }

        [HttpDelete(ApiRoute.DailyCareSchedule.Remove)]
        public async Task<IServiceResult> RemoveDailyCareScheduleAsync([FromRoute] int id)
        {
            return await dailyCareScheduleService.RemoveAsync(id);
        }

        [HttpGet(ApiRoute.DailyCareSchedule.GetAllCareTask)]
        public async Task<IServiceResult> GetAllCareTaskAsync()
        {
            return await careTaskService.FindAllAsync();
        }

        [HttpGet(ApiRoute.DailyCareSchedule.GetAllDeliveryOrderDetail)]
        public async Task<IServiceResult> GetAllDeliveryOrderDetailAsync()
        {
            return await deliveryOrderDetailService.FindAllAsync();
        }

    }
}
