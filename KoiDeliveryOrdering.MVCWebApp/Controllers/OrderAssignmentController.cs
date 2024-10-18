using KoiDeliveryOrdering.API.Payloads.Requests;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.MVCWebApp.Models;
using KoiDeliveryOrdering.MVCWebApp.Utils;
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
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "orderassignment"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var orderAssignments = JsonConvert.DeserializeObject<List<OrderAssignmentModel>>(
                                result.Data.ToString());

                            // Progress paging 
                            var pagination =
                                PaginatedList<OrderAssignmentModel>.Paging(orderAssignments ?? new(), 1,
                                    5); // Default 5 record each page

                            // Set ViewData
                            ViewData["PageIndex"] = pagination.PageIndex;
                            ViewData["TotalPage"] = pagination.TotalPage;

                            return View(pagination);
                        }
                    }
                }
            }
            return View(new PaginatedList<OrderAssignmentModel>(new(), 1, 5));
        }

        // GET: OrderAssignmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        
        private async Task SetDefaultViewDataAsync()
        {
            ViewBag.DeliveryOrderId = new SelectList(await this.GetAllDeliveryOrderformationAsync(), "DeliveryOrderId", "OrderStatus");
            ViewBag.DriverId = new SelectList(await this.GetAllDriverformationAsync(), "DriverId", "Email");
            ViewBag.FishCarerId = new SelectList(await this.GetAllDriverformationAsync(), "FishCarerId", "Email");
            ViewBag.AssignedTruckId = new SelectList(await this.GetAllTruckformationAsync(), "TruckId", "TruckLicensePlate");
        }

        public async Task<List<TruckModel>> GetAllTruckformationAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(Const.APIEndpoint + "truck"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var truckInformation = JsonConvert.DeserializeObject<List<TruckModel>>(result.Data.ToString());
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
                using (var resp = await httpClient.GetAsync(Const.APIEndpoint + "delivery-orders"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var deliveryOrderInformation = JsonConvert.DeserializeObject<List<DeliveryOrderModel>>(result.Data.ToString());
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
                using (var resp = await httpClient.GetAsync(Const.APIEndpoint + "staff"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var staffInformation = JsonConvert.DeserializeObject<List<StaffModel>>(result.Data.ToString());
                            return staffInformation ?? new List<StaffModel>();
                        }
                    }
                }
            }
            return new List<StaffModel>();
        }

        // GET: OrderAssignmentController/Create
        [HttpGet]
        [Route("OrderAssignment/Create")]
        public async Task<IActionResult> Create()
        {
            await SetDefaultViewDataAsync();
            return View();
        }


        // POST: OrderAssignmentController/Create
        [HttpPost]
        [Route("OrderAssignment/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeliveryOrderId,DriverId,FishCarerId,AssignedTruckId,AssignedDate,DeliveryStatus")] OrderAssignmentCreateRequest orderAssignment)
        {
            if (!ModelState.IsValid)
            {
                return await this.Create();
            }

            bool saveStatus = false;
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.PostAsJsonAsync(Const.APIEndpoint + "OrderAssignment", orderAssignment))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context);

                        if (result != null && result.Status == Const.SUCCESS_INSERT_CODE)
                        {
                            saveStatus = true;
                        }
                        else
                        {
                            saveStatus = false;
                        }
                    }
                }
            }

            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                await SetDefaultViewDataAsync();
                return View();
            }
        }

        // GET: OrderAssignmentController/Edit/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderAssignmentModel? orderAssignment = null!;
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                           Const.APIEndpoint + "OrderAssignment/" + id))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            orderAssignment = JsonConvert.DeserializeObject<OrderAssignmentModel>(
                                result.Data.ToString()!);
                        }
                    }
                }
            }

            return orderAssignment != null
                 ? View(orderAssignment)
                 : NotFound();
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
