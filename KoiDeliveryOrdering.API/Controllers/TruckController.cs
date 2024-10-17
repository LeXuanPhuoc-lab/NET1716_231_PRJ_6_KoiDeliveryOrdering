using KoiDeliveryOrdering.API.Payloads;
using KoiDeliveryOrdering.API.Payloads.Requests;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Data.Entities;
using KoiDeliveryOrdering.Service.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace KoiDeliveryOrdering.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TruckController : ControllerBase
    {
        private readonly ITruckService _truckService;

        public TruckController(ITruckService truckService)
        {
            _truckService = truckService;
        }

        [HttpGet(ApiRoute.Truck.GetById)]
        public async Task<IServiceResult> FindAsync(int id)
        {
            return await _truckService.FindAsync(id);
        }

        [HttpGet(ApiRoute.Truck.GetAll)]
        public async Task<IServiceResult> FindAllAsync()
        {
            return await _truckService.FindAllAsync();
        }

        [HttpPost(ApiRoute.Truck.Insert)]
        public async Task<IServiceResult> CreateAsync([FromBody] CreateTruckRequest truck)
        {
            var truckEntity = truck.Adapt<Truck>();
            return await _truckService.InsertAsync(truckEntity);
        }

        [HttpPut(ApiRoute.Truck.Update)]
        public async Task<IServiceResult> UpdateAsync([FromBody] Truck truck)
        {
            return await _truckService.UpdateAsync(truck);
        }

        [HttpDelete(ApiRoute.Truck.Remove)]
        public async Task<IServiceResult> DeleteAsync(int id)
        {
            return await _truckService.RemoveAsync(id);
        }
    }
}
