using KoiDeliveryOrdering.API.Payloads;
using KoiDeliveryOrdering.API.Payloads.Requests;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Data.Entities;
using KoiDeliveryOrdering.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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
        public async Task<IServiceResult> UpdateGarageAsync([FromBody] UpdateGarageRequest garage)
        {
            var garageEntity = garage.Adapt<Garage>();
            return await _garageService.UpdateAsync(garageEntity);
        }

        [HttpDelete(ApiRoute.Garage.Remove)]
        public async Task<IServiceResult> DeleteGarageAsync(int id)
        {
            return await _garageService.RemoveAsync(id);
        }

        private async Task<(double? Latitude, double? Longitude)> GetCoordinatesFromHereAsync(string city, string street, string district, string ward)
        {
            string apiKey = "hhH7xYjbGaeyKOX-4EPSQglll7-HU79A0AAfkhte_D8";
            string address = $"{street}, {ward}, {district}, {city}";
            string url = ApiRoute.HereMap.BaseUrl;
            url = url.Replace("{address}", address);
            url = url.Replace("{apiKey}", apiKey);
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(url);
                var json = JObject.Parse(response);

                var location = json["items"]?[0]?["position"];
                if (location != null)
                {
                    double latitude = (double)location["lat"];
                    double longitude = (double)location["lng"];
                    return (latitude, longitude);
                }
            }

            return (null, null);
        }
    }
}
