using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.MVCWebApp.Models;
using KoiDeliveryOrdering.MVCWebApp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace KoiDeliveryOrdering.MVCWebApp.Controllers
{
    public class DailyCareSchedulesController : Controller
    {

        // GET: DailyCareSchedules
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "daily-care-schedule"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var dailyCareSchedule = JsonConvert.DeserializeObject<List<DailyCareScheduleModels>>(
                                result.Data.ToString());

                            // Progress paging 
                            var pagination =
                                PaginatedList<DailyCareScheduleModels>.Paging(dailyCareSchedule ?? new(), 1,
                                    5); // Default 5 record each page

                            // Set ViewData
                            ViewData["PageIndex"] = pagination.PageIndex;
                            ViewData["TotalPage"] = pagination.TotalPage;

                            return View(pagination);
                        }
                    }
                }
            }
            return View(new PaginatedList<DailyCareScheduleModels>(new(), 1, 5));
        }

        // GET: DailyCareSchedules (With condition)
        public async Task<IActionResult> IndexWithAjax(string searchValue, int pageIndex = 1)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "daily-care-schedule"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var dailyCareSchedules = JsonConvert.DeserializeObject<List<DailyCareScheduleModels>>(
                                result.Data.ToString());

                            if (dailyCareSchedules != null && dailyCareSchedules.Any())
                            {
                                // Searching (if any)
                                if (!string.IsNullOrEmpty(searchValue))
                                {
                                    searchValue = searchValue.Trim();

                                    // Try to parse number 
                                    decimal parsedAmount;
                                    bool isNumericSearch = decimal.TryParse(searchValue, out parsedAmount);

                                    // Perform the search using LINQ
                                    dailyCareSchedules = dailyCareSchedules
                                        .Where(dailySchedule =>
                                            dailySchedule.CaregiverName.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                                            dailySchedule.TaskFrequency.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                                            dailySchedule.TaskDuration.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                                            (isNumericSearch && dailySchedule.RecommendedValue == parsedAmount))
                                        .ToList();
                                }

                                // Progress paging 
                                var pagination =
                                    PaginatedList<DailyCareScheduleModels>.Paging(dailyCareSchedules ?? new(), pageIndex,
                                        5); // Default 5 record each page

                                // Set ViewData
                                ViewData["PageIndex"] = pagination.PageIndex;
                                ViewData["TotalPage"] = pagination.TotalPage;

                                return new JsonResult(new
                                { PageIndex = pagination.PageIndex, TotalPage = pagination.TotalPage, DailyCareSchedules = pagination });
                            }
                        }
                    }
                }
            }

            return new JsonResult(new
            { PageIndex = 1, TotalPage = 1, DailyCareSchedules = new List<DailyCareScheduleModels>() });
        }

        // GET: DailyCareSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DailyCareScheduleModels dailyCareSchedule = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + $"daily-care-schedule/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context);

                        if (result != null && result.Data != null)
                        {
                            dailyCareSchedule = JsonConvert.DeserializeObject<DailyCareScheduleModels>(result.Data.ToString());
                        }
                    }
                }
            }

            if (dailyCareSchedule == null)
            {
                return NotFound();
            }

            return View(dailyCareSchedule);
        }


        // GET: DailyCareSchedules/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CareTaskId"] = new SelectList(await this.GetAllCareTaskAsync(), "CareTaskId", "TaskName");
            ViewData["DeliverOrderDetailId"] = new SelectList(await this.GetAllDeliveryOrderDetailAsync(), "Id", "Id");
            ViewData["CaregiverName"] = new SelectList(await this.GetAllStaffAsync(), "FullName", "FullName");
            return View();
        }

        // POST: DailyCareSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*        [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Create([Bind("DailyCareScheduleId,CareTaskId,TaskFrequency,RecommendedValue,StartDate,EndDate,TaskDuration,Notes,IsCritical,DeliverOrderDetailId,CaregiverName,LastPerformedDate")] DailyCareSchedule dailyCareSchedule)
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(dailyCareSchedule);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    ViewData["CareTaskId"] = new SelectList(_context.CareTasks, "CareTaskId", "TaskName", dailyCareSchedule.CareTaskId);
                    ViewData["DeliverOrderDetailId"] = new SelectList(_context.DeliveryOrderDetails, "Id", "Id", dailyCareSchedule.DeliverOrderDetailId);
                    return View(dailyCareSchedule);
                }*/

        // GET: DailyCareSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DailyCareScheduleModels dailyCareSchedule = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + $"daily-care-schedule/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context);

                        if (result != null && result.Data != null)
                        {
                            dailyCareSchedule = JsonConvert.DeserializeObject<DailyCareScheduleModels>(result.Data.ToString());
                        }
                    }
                }
            }

            if (dailyCareSchedule == null)
            {
                return NotFound();
            }

            ViewData["CareTaskId"] = new SelectList(await this.GetAllCareTaskAsync(), "CareTaskId", "TaskName");
            ViewData["DeliverOrderDetailId"] = new SelectList(await this.GetAllDeliveryOrderDetailAsync(), "Id", "Id");
            ViewData["CaregiverName"] = new SelectList(await this.GetAllStaffAsync(), "FullName", "FullName");

            return View(dailyCareSchedule);
        }

        // POST: DailyCareSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*        [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Edit(int id, [Bind("DailyCareScheduleId,CareTaskId,TaskFrequency,RecommendedValue,StartDate,EndDate,TaskDuration,Notes,IsCritical,DeliverOrderDetailId,CaregiverName,LastPerformedDate")] DailyCareSchedule dailyCareSchedule)
                {
                    if (id != dailyCareSchedule.DailyCareScheduleId)
                    {
                        return NotFound();
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(dailyCareSchedule);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!DailyCareScheduleExists(dailyCareSchedule.DailyCareScheduleId))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    ViewData["CareTaskId"] = new SelectList(_context.CareTasks, "CareTaskId", "TaskName", dailyCareSchedule.CareTaskId);
                    ViewData["DeliverOrderDetailId"] = new SelectList(_context.DeliveryOrderDetails, "Id", "Id", dailyCareSchedule.DeliverOrderDetailId);
                    return View(dailyCareSchedule);
                }*/

        // GET: DailyCareSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DailyCareScheduleModels dailyCareSchedule = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + $"daily-care-schedule/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context);

                        if (result != null && result.Data != null)
                        {
                            dailyCareSchedule = JsonConvert.DeserializeObject<DailyCareScheduleModels>(result.Data.ToString());
                        }
                    }
                }
            }

            if (dailyCareSchedule == null)
            {
                return NotFound();
            }

            return View(dailyCareSchedule);
        }

        // POST: DailyCareSchedules/Delete/5
        /*        [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> DeleteConfirmed(int id)
                {
                    var dailyCareSchedule = await _context.DailyCareSchedules.FindAsync(id);
                    if (dailyCareSchedule != null)
                    {
                        _context.DailyCareSchedules.Remove(dailyCareSchedule);
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }*/

        /*        private bool DailyCareScheduleExists(int id)
                {
                    return _context.DailyCareSchedules.Any(e => e.DailyCareScheduleId == id);
                }*/

        public async Task<List<CareTaskModel>> GetAllCareTaskAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "care-task"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var careTasks = JsonConvert.DeserializeObject<List<CareTaskModel>>(
                                result.Data.ToString());

                            return careTasks;
                        }
                    }
                }
            }

            return null;
        }

        public async Task<List<DeliveryOrderDetailModel>> GetAllDeliveryOrderDetailAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "delivery-order-detail"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var deliveryOrderDetails = JsonConvert.DeserializeObject<List<DeliveryOrderDetailModel>>(
                                result.Data.ToString());

                            return deliveryOrderDetails;
                        }
                    }
                }
            }

            return null;
        }

        public async Task<List<StaffModel>> GetAllStaffAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "staff"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var staffs = JsonConvert.DeserializeObject<List<StaffModel>>(
                                result.Data.ToString());

                            return staffs;
                        }
                    }
                }
            }

            return null;
        }
    }
}
