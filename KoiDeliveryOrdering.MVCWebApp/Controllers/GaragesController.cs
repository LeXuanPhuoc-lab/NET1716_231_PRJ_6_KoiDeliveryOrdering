using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiDeliveryOrdering.Data.Context;
using KoiDeliveryOrdering.Data.Entities;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.MVCWebApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using KoiDeliveryOrdering.API.Payloads;
using KoiDeliveryOrdering.API.Models;
using static KoiDeliveryOrdering.API.Payloads.ApiRoute;
using KoiDeliveryOrdering.API.Payloads.Requests;
using KoiDeliveryOrdering.MVCWebApp.Utils;

namespace KoiDeliveryOrdering.MVCWebApp.Controllers
{
    public class GaragesController : Controller
    {
        // GET: Garages
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "garages"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var garages = JsonConvert.DeserializeObject<List<GarageModel>>(
                                result.Data.ToString());

                            var pagination =
                                PaginatedList<GarageModel>.Paging(garages ?? new(), 1,
                                    5);

                            ViewData["PageIndex"] = pagination.PageIndex;
                            ViewData["TotalPage"] = pagination.TotalPage;

                            return View(pagination);
                        }
                    }
                }
            }
            return View(new PaginatedList<GarageModel>(new (), 1, 1));
        }

        public async Task<IActionResult> IndexWithAjax(string searchValue, int pageIndex = 1)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "garages"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var garages = JsonConvert.DeserializeObject<List<GarageModel>>(
                                result.Data.ToString());

                            if (garages != null && garages.Any())
                            {
                                if (!string.IsNullOrEmpty(searchValue))
                                {
                                    searchValue = searchValue.Trim();
                                    var searchReq = searchValue.Split(',');
                                    if(searchReq.Length == 3)
                                    {
                                        garages = garages.Where(garage =>
                                            garage.GarageName.Contains(searchReq[0].Trim()) &&
                                            garage.ManagerName.Contains(searchReq[1].Trim()) &&
                                            garage.CityProvince.Contains(searchReq[2].Trim())
                                        ).ToList();
                                    } else
                                    {
                                        garages = garages
                                            .Where(garage =>
                                                garage.GarageName.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                                                garage.Phone.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                                                garage.ManagerName.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                                                garage.CityProvince.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                                                garage.Street.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                                                garage.District.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                                                garage.Ward.Contains(searchValue, StringComparison.OrdinalIgnoreCase)
                                                )
                                            .ToList();
                                    }
                                }

                                var pagination =
                                    PaginatedList<GarageModel>.Paging(garages ?? new(), pageIndex,
                                        5);

                                ViewData["PageIndex"] = pagination.PageIndex;
                                ViewData["TotalPage"] = pagination.TotalPage;

                                return new JsonResult(new
                                { PageIndex = pagination.PageIndex, TotalPage = pagination.TotalPage, Garages = pagination });
                            }
                        }
                    }
                }
            }

            return new JsonResult(new
            { PageIndex = 1, TotalPage = 1, DailyCareSchedules = new List<DailyCareScheduleModels>() });
        }

        // GET: Garages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GarageModel? garage = null!;
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                           Const.APIEndpoint + "garages/" + id))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            garage = JsonConvert.DeserializeObject<GarageModel>(
                                result.Data.ToString()!);
                        }
                    }
                }
            }

            return garage != null
                 ? View(garage)
                 : NotFound();
        }

        // GET: Garages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Garages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GarageName,Phone,ManagerName,CityProvince,Street,District,Ward")] CreateGarageRequest garage)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var position = await GetCoordinatesFromHereAsync(garage.CityProvince, garage.Street, garage.District, garage.Ward);
            garage.Latitude = position.Latitude;
            garage.Longitude = position.Longitude;

            bool insertStatus = false;
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.PostAsJsonAsync(
                           Const.APIEndpoint + "garages", garage))
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
            } else return View();
        }

        // GET: Garages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UpdateGarageRequest? garage = null!;
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                           Const.APIEndpoint + "garages/" + id))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            garage = JsonConvert.DeserializeObject<UpdateGarageRequest>(
                                result.Data.ToString()!);
                        }
                    }
                }
            }

            return garage != null 
                 ? View(garage)
                 : NotFound();
        }

        // POST: Garages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GarageId,GarageName,Phone,ManagerName,CityProvince,Street,District,Ward")] UpdateGarageRequest garage)
        {
            if (id != garage.GarageId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(garage);
            }

            var position = await GetCoordinatesFromHereAsync(garage.CityProvince, garage.Street, garage.District, garage.Ward);
            garage.Latitude = position.Latitude;
            garage.Longitude = position.Longitude;

            bool insertStatus = false;
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.PutAsJsonAsync(
                           Const.APIEndpoint + "garages", garage))
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

        // GET: Garages/Delete/5
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

            GarageModel? garage = null!;
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                           Const.APIEndpoint + "garages/" + id))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            garage = JsonConvert.DeserializeObject<GarageModel>(
                                result.Data.ToString()!);
                        }
                    }
                }
            }

            return garage != null
                ? View(garage)
                : NotFound();
        }

        // POST: Garages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleteStatus = false;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(
                    Const.APIEndpoint + "garages/" + id))
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

        //private bool GarageExists(int id)
        //{
        //    return _context.Garages.Any(e => e.GarageId == id);
        //}

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
