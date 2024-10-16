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

                            return View(garages);
                        }
                    }
                }
            }
            return View(new List<GarageModel>());
        }

        // GET: Garages/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var garage = await _context.Garages
        //        .FirstOrDefaultAsync(m => m.GarageId == id);
        //    if (garage == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(garage);
        //}

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
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var garage = await _context.Garages.FindAsync(id);
        //    if (garage == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(garage);
        //}

        // POST: Garages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("GarageId,GarageName,Phone,ManagerName,CityProvince,Street,District,Ward,Longitude,Latitude")] Garage garage)
        //{
        //    if (id != garage.GarageId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(garage);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!GarageExists(garage.GarageId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(garage);
        //}

        // GET: Garages/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var garage = await _context.Garages
        //        .FirstOrDefaultAsync(m => m.GarageId == id);
        //    if (garage == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(garage);
        //}

        // POST: Garages/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var garage = await _context.Garages.FindAsync(id);
        //    if (garage != null)
        //    {
        //        _context.Garages.Remove(garage);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

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
