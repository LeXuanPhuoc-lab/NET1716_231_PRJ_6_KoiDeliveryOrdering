using KoiDeliveryOrdering.API.Payloads;
using KoiDeliveryOrdering.API.Payloads.Requests;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Data.Entities;
using KoiDeliveryOrdering.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.API.Controllers
{
    [ApiController]
    public class GarageController : ControllerBase
    {
        private readonly IGarageService _garageService;

        public GarageController(IGarageService garageService)
        {
            _garageService = garageService;
        }

        [HttpGet(ApiRoute.Garage.GetById)]
        public async Task<IServiceResult> GetGarageByIdAsync(int id)
        {
            return await _garageService.FindAsync(id);
        }

        [HttpGet(ApiRoute.Garage.GetAll)]
        public async Task<IServiceResult> GetAllGaragesAsync()
        {
            return await _garageService.FindAllAsync();
        }

        [HttpPost(ApiRoute.Garage.Insert)]
        public async Task<IServiceResult> CreateGarageAsync([FromBody] CreateGarageRequest garage)
        {
            var garageEntity = garage.Adapt<Garage>();
            return await _garageService.InsertAsync(garageEntity);
        }

        [HttpPut(ApiRoute.Garage.Update)]
        public async Task<IServiceResult> UpdateGarageAsync([FromBody] Garage garage)
        {
            return await _garageService.UpdateAsync(garage);
        }

        [HttpDelete(ApiRoute.Garage.Remove)]
        public async Task<IServiceResult> DeleteGarageAsync(int id)
        {
            return await _garageService.RemoveAsync(id);
        }
    }
}
