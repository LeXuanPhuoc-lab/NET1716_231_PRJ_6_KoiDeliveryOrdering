using KoiDeliveryOrdering.API.Payloads.Requests;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.MVCWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace KoiDeliveryOrdering.MVCWebApp.Controllers
{
    public class TruckController : Controller
    {
        // GET: Truck
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "Truck"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var trucks = JsonConvert.DeserializeObject<List<TruckModel>>(
                                result.Data.ToString());

                            return View(trucks);
                        }
                    }
                }
            }
            return View(new List<TruckModel>());
        }

        // GET: TruckController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TruckModel? truck = null!;
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                           Const.APIEndpoint + "truck/" + id))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            truck = JsonConvert.DeserializeObject<TruckModel>(
                                result.Data.ToString()!);
                        }
                    }
                }
            }

            return truck != null
                 ? View(truck)
                 : NotFound();
        }

        [HttpGet]
        [Route("Truck/Create")]
        public async Task<IActionResult> Create()
        {
            await SetDefaultViewDataAsync();

            return View();
        }

        private async Task SetDefaultViewDataAsync()
        {
            ViewData["GarageId"] = new SelectList(await this.GetAllGarageformationAsync(), "GarageId", "GarageName");            
        }

        public async Task<List<GarageModel>> GetAllGarageformationAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                           Const.APIEndpoint + "garages/"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var garageInformation = JsonConvert.DeserializeObject<List<GarageModel>>(
                                result.Data.ToString()!);

                            return garageInformation ?? new List<GarageModel>();
                        }
                    }
                }
            }

            return new List<GarageModel>();
        }

        // POST: Truck/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Truck/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TruckLicensePlate,Model,Capacity,IsActive,GarageId,LastMaintenanceDate")] CreateTruckRequest truck)
        {
            if (!ModelState.IsValid)
            {
                return await this.Create();
            }

            bool insertStatus = false;
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.PostAsJsonAsync(
                           Const.APIEndpoint + "truck/", truck))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context);

                        if (result != null && result.Status == Const.SUCCESS_INSERT_CODE)
                        {
                            insertStatus = true;
                        }
                        else
                        {
                            insertStatus = false;
                        }
                    }
                }
            }

            if (insertStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else return View();
        }

        // GET: TruckController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UpdateTruckRequest? truck = null!;
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                           Const.APIEndpoint + "Truck/" + id))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            truck = JsonConvert.DeserializeObject<UpdateTruckRequest>(
                                result.Data.ToString()!);
                        }
                    }
                }
            }

            await SetDefaultViewDataAsync();

            return truck != null
                 ? View(truck)
                 : NotFound();
        }

        // POST: TruckController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TruckId,TruckLicensePlate,Model,Capacity,IsActive,GarageId,LastMaintenanceDate")] UpdateTruckRequest truck)
        {
            if (id != truck.TruckId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(truck);
            }

            bool insertStatus = false;
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.PutAsJsonAsync(
                           Const.APIEndpoint + "Truck", truck))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context);

                        if (result != null && result.Status == Const.SUCCESS_UPDATE_CODE)
                        {
                            insertStatus = true;
                        }
                        else
                        {
                            insertStatus = false;
                        }
                    }
                }
            }

            if (insertStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else return View();
        }

        // GET: TruckController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            TruckModel? truck = null!;
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                           Const.APIEndpoint + "truck/" + id))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            truck = JsonConvert.DeserializeObject<TruckModel>(
                                result.Data.ToString()!);
                        }
                    }
                }
            }

            return truck != null
                ? View(truck)
                : NotFound();
        }

        // POST: TruckController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleteStatus = false;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(
                    Const.APIEndpoint + "truck/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(content);
                        if (result != null && result.Status == Const.SUCCESS_REMOVE_CODE)
                        {
                            deleteStatus = true;
                        }
                    }
                }
            }

            return deleteStatus
                ? RedirectToAction(nameof(Index))
                : RedirectToAction(nameof(Delete));
        }
    }
}
