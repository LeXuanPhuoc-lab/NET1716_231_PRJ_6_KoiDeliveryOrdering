using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Data.Entities;
using KoiDeliveryOrdering.Service.Interfaces;
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

        [HttpGet("{id}", Name = nameof(GetTruckByIdAsync))]
        public async Task<IServiceResult> GetTruckByIdAsync(int id)
        {
            return await _truckService.GetTruckByIdAsync(id);
        }

        [HttpGet(Name = nameof(GetAllTrucksAsync))]
        public async Task<IServiceResult> GetAllTrucksAsync()
        {
            return await _truckService.GetAllTrucksAsync();
        }

        [HttpPost(Name = nameof(CreateTruckAsync))]
        public async Task<IServiceResult> CreateTruckAsync([FromBody] Truck truck)
        {
            return await _truckService.CreateTruckAsync(truck);
        }

        [HttpPut(Name = nameof(UpdateTruckAsync))]
        public async Task<IServiceResult> UpdateTruckAsync([FromBody] Truck truck)
        {
            return await _truckService.UpdateTruckAsync(truck);
        }

        [HttpDelete("{id}", Name = nameof(DeleteTruckAsync))]
        public async Task<IServiceResult> DeleteTruckAsync(int id)
        {
            return await _truckService.DeleteTruckAsync(id);
        }
    }
}
