using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.MVCWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KoiDeliveryOrdering.MVCWebApp.Controllers
{
    public class CareTasksController : Controller
    {
        // GET: CareTasks
        public async Task<IActionResult> Index()
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

                            return View(careTasks);
                        }
                    }
                }
            }
            return View(new List<CareTaskModel>());
        }

        // GET: CareTasks/Details/5
        /*        public async Task<IActionResult> Details(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var careTask = await _context.CareTasks
                        .FirstOrDefaultAsync(m => m.CareTaskId == id);
                    if (careTask == null)
                    {
                        return NotFound();
                    }

                    return View(careTask);
                }*/

        // GET: CareTasks/Create
        /*        public IActionResult Create()
                {
                    return View();
                }*/

        // POST: CareTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*        [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Create([Bind("CareTaskId,TaskName,Description,Unit,Priority,CreatedAt,UpdateAt,DueDate,AssignedTo,CompletedAt,IsRecurring,Notes")] CareTask careTask)
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(careTask);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    return View(careTask);
                }*/

        // GET: CareTasks/Edit/5
        /*        public async Task<IActionResult> Edit(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var careTask = await _context.CareTasks.FindAsync(id);
                    if (careTask == null)
                    {
                        return NotFound();
                    }
                    return View(careTask);
                }*/

        // POST: CareTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*        [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Edit(int id, [Bind("CareTaskId,TaskName,Description,Unit,Priority,CreatedAt,UpdateAt,DueDate,AssignedTo,CompletedAt,IsRecurring,Notes")] CareTask careTask)
                {
                    if (id != careTask.CareTaskId)
                    {
                        return NotFound();
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(careTask);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!CareTaskExists(careTask.CareTaskId))
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
                    return View(careTask);
                }*/

        // GET: CareTasks/Delete/5
        /*        public async Task<IActionResult> Delete(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var careTask = await _context.CareTasks
                        .FirstOrDefaultAsync(m => m.CareTaskId == id);
                    if (careTask == null)
                    {
                        return NotFound();
                    }

                    return View(careTask);
                }*/

        // POST: CareTasks/Delete/5
        /*        [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> DeleteConfirmed(int id)
                {
                    var careTask = await _context.CareTasks.FindAsync(id);
                    if (careTask != null)
                    {
                        _context.CareTasks.Remove(careTask);
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }*/

        /*        private bool CareTaskExists(int id)
                {
                    return _context.CareTasks.Any(e => e.CareTaskId == id);
                }*/
    }
}
