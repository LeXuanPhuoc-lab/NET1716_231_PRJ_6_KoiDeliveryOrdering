using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.MVCWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace KoiDeliveryOrdering.MVCWebApp.Controllers
{
    public class OrderAssignmentController : Controller
    {
        // GET: OrderAssignmentController
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "OrderAssignment"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var orderAssignment = JsonConvert.DeserializeObject<List<OrderAssignmentModel>>(
                                result.Data.ToString());

                            return View(orderAssignment);
                        }
                    }
                }
            }
            return View(new List<OrderAssignmentModel>());
        }

        // GET: OrderAssignmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderAssignmentController/Create
        public async Task<IActionResult> Create()
        {
            await SetDefaultViewDataAsync();

            return View();
        }

        private async Task SetDefaultViewDataAsync()
        {
            ViewData["TruckId"] = new SelectList(await this.GetAllTruckformationAsync(), "TruckId", "TruckLicensePlate");
            ViewData["DeliveryOrderId"] = new SelectList(await this.GetAllDeliveryOrderformationAsync(), "DeliveryOrderId", "OrderStatus");
            ViewData["DriverId"] = new SelectList(await this.GetAllDriverformationAsync(), "DriverId", "Email");
        }

        public async Task<List<TruckModel>> GetAllTruckformationAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                           Const.APIEndpoint + "truck"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var truckInformation = JsonConvert.DeserializeObject<List<TruckModel>>(
                                result.Data.ToString()!);

                            return truckInformation ?? new List<TruckModel>();
                        }
                    }
                }
            }
            return new List<TruckModel>();
        }

        public async Task<List<DeliveryOrderModel>> GetAllDeliveryOrderformationAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                           Const.APIEndpoint + "delivery-orders"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var deliveryOrderInformation = JsonConvert.DeserializeObject<List<DeliveryOrderModel>>(
                                result.Data.ToString()!);

                            return deliveryOrderInformation ?? new List<DeliveryOrderModel>();
                        }
                    }
                }
            }
            return new List<DeliveryOrderModel>();
        }

        public async Task<List<StaffModel>> GetAllDriverformationAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                           Const.APIEndpoint + "staff"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var staffInformation = JsonConvert.DeserializeObject<List<StaffModel>>(
                                result.Data.ToString()!);

                            return staffInformation ?? new List<StaffModel>();
                        }
                    }
                }
            }
            return new List<StaffModel>();
        }

        // POST: OrderAssignmentController/Create
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

        // GET: OrderAssignmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderAssignmentController/Edit/5
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

        // GET: OrderAssignmentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderAssignmentController/Delete/5
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
