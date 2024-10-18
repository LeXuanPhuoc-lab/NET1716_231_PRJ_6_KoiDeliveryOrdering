using KoiDeliveryOrdering.API.Payloads;
using KoiDeliveryOrdering.API.Payloads.Requests;
using KoiDeliveryOrdering.Business;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Interfaces;
using KoiDeliveryOrdering.Data.Entities;
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

        [HttpGet(ApiRoute.Animal.GetAll)]
        public async Task<IServiceResult> GetAllAnimal() 
        {
            return await _animalService.GetAllAnimal();
        }

        [HttpGet(ApiRoute.Animal.GetById)]
        public async Task<IServiceResult> GetById(int id)
        {
            return await _animalService.FindAnimalById(id);
        }

        [HttpDelete(ApiRoute.Animal.Delete)]
        public async Task<IServiceResult> Delete(int id) 
        {
            return await _animalService.RemoveAsync(id);
        }
        [HttpPost(ApiRoute.Animal.Create)]
        public async Task<IServiceResult> Create([FromBody] CreateAnimalRequest req)
        {
            return await _animalService.InsertAsync(req.ToAnimal());
        }

        [HttpPut(ApiRoute.Animal.Update)]
        public async Task<IServiceResult> Update([FromBody] UpdateAnimalRequest req)
        {
            return await _animalService.UpdateAsync(req.ToAnimal());
        }
    }
}
