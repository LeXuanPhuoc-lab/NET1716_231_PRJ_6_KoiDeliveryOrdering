using KoiDeliveryOrdering.API.Payloads;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrdering.API.Controllers
{
    [ApiController]
    public class AnimalController : ControllerBase 
    {
        private readonly IAnimalService _animalService;

        public AnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet(ApiRoute.Animal.GetAllAnimalType)]
        public async Task<IServiceResult> GetAllAnimalTypeAsync()
        {
            return await _animalService.FindAllAnimalTypeAsync();
        }

        [HttpGet(ApiRoute.Animal.GetAllHealthStatus)]
        public async Task<IServiceResult> GetAllHealthStatusAsync()
        {
            return await _animalService.FindAllHealthStatusAsync();
        }
    }
}
