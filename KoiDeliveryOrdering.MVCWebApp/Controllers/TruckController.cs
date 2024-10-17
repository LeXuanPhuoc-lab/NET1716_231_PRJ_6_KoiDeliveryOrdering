using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.MVCWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KoiDeliveryOrdering.MVCWebApp.Controllers
{
    public class TruckController : Controller
    {
        // GET: TruckController
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "truck"))
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
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TruckController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TruckController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TruckController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TruckController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TruckController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TruckController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
