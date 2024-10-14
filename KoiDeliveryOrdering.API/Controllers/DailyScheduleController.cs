using KoiDeliveryOrdering.API.Payloads;
using KoiDeliveryOrdering.API.Payloads.Requests;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Interfaces;
using KoiDeliveryOrdering.Data.Entities;
using KoiDeliveryOrdering.Service.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrdering.API.Controllers
{
    [ApiController]
    public class DailyScheduleController : ControllerBase
    {
        private readonly IDailyCareScheduleService dailyCareScheduleService;
        private readonly IDeliveryOrderDetailService deliveryOrderDetailService;
        private readonly ICareTaskService careTaskService;
        private readonly IStaffService staffService;

        public DailyScheduleController(IDailyCareScheduleService dailyCareScheduleService,
            IDeliveryOrderDetailService deliveryOrderDetailService,
            ICareTaskService careTaskService,
            IStaffService staffService)
        {
            this.dailyCareScheduleService = dailyCareScheduleService;
            this.deliveryOrderDetailService = deliveryOrderDetailService;
            this.careTaskService = careTaskService;
            this.staffService = staffService;
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
        public async Task<IServiceResult> InsertDailyCareScheduleAsync([FromBody] DailyCareScheduleRequest req)
        {
            // Map to DailyCareSchedule entity
            var dailyCareScheduleEntity = req.Adapt<DailyCareSchedule>();
            return await dailyCareScheduleService.InsertAsync(dailyCareScheduleEntity);
        }

        [HttpPut(ApiRoute.DailyCareSchedule.Update)]
        public async Task<IServiceResult> UpdateDailyCareScheduleAsync([FromBody] DailyCareScheduleRequest req)
        {
            // Map to DailyCareSchedule entity
            var dailyCareScheduleEntity = req.Adapt<DailyCareSchedule>();
            return await dailyCareScheduleService.UpdateAsync(dailyCareScheduleEntity);
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

        [HttpGet(ApiRoute.DailyCareSchedule.GetAllStaff)]
        public async Task<IServiceResult> GetAllStaff()
        {
            return await staffService.FindAllAsync();
        }

    }
}
