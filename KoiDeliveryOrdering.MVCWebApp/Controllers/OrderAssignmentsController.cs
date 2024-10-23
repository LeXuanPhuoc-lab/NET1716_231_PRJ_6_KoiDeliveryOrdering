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
using KoiDeliveryOrdering.MVCWebApp.Utils;
using KoiDeliveryOrdering.API.Payloads.Requests;
using static KoiDeliveryOrdering.API.Payloads.ApiRoute;

namespace KoiDeliveryOrdering.MVCWebApp.Controllers
{
    public class OrderAssignmentsController : Controller
    {

        // GET: OrderAssignments
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

        // GET: OrderAssignments/Details/5
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

        private async Task SetDefaultViewDataAsync()
        {
            ViewData["AssignedTruckId"] = new SelectList(await this.GetAllTruckformationAsync(), "TruckId", "TruckLicensePlate");
            ViewData["DeliveryOrderId"] = new SelectList(await this.GetAllDeliveryOrderformationAsync(), "Id", "DeliveryOrderId");
            ViewData["DriverId"] = new SelectList(await this.GetAllDriverformationAsync(), "Id", "Email");
            ViewData["FishCarerId"] = new SelectList(await this.GetAllDriverformationAsync(), "Id", "Email");
        }

        // GET: OrderAssignments/Create
        public async Task<IActionResult> Create()
        {
            await SetDefaultViewDataAsync();
            return View();
        }

        // POST: OrderAssignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderAssignmentId,DeliveryOrderId,DriverId,FishCarerId,AssignedTruckId,AssignedDate,DeliveryStatus")] OrderAssignmentCreateRequest orderAssignment)
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

        // GET: OrderAssignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderAssignmentUpdateRequest? orderAssignment = null!;
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
                            orderAssignment = JsonConvert.DeserializeObject<OrderAssignmentUpdateRequest>(
                                result.Data.ToString()!);
                        }
                    }
                }
            }

            await SetDefaultViewDataAsync();

            return orderAssignment != null
                 ? View(orderAssignment)
                 : NotFound();
        }

        // POST: OrderAssignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderAssignmentId,DeliveryOrderId,DriverId,FishCarerId,AssignedTruckId,AssignedDate,DeliveryStatus")] OrderAssignmentUpdateRequest orderAssignment)
        {
            if (id != orderAssignment.OrderAssignmentId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(orderAssignment);
            }

            bool insertStatus = false;
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.PutAsJsonAsync(
                           Const.APIEndpoint + "OrderAssignment", orderAssignment))
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

        // GET: OrderAssignments/Delete/5
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

        // POST: OrderAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleteStatus = false;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(
                    Const.APIEndpoint + "OrderAssignment/" + id))
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
