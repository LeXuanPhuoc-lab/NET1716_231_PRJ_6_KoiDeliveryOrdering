using Microsoft.AspNetCore.Mvc;
using KoiDeliveryOrdering.Common;
using Newtonsoft.Json;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.MVCWebApp.Models;

namespace KoiDeliveryOrdering.MVCWebApp.Controllers
{
    public class DeliveryOrderController : Controller
    {
        // GET: DeliveryOrder
        public async Task<IActionResult> Index()
        {
            using(var httpClient = new HttpClient())
            {
                using(var response = await httpClient.GetAsync(Const.APIEndpoint + "delivery-orders"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if(result != null && result.Data != null)
                        {
                            var deliveryOrders = JsonConvert.DeserializeObject<List<DeliveryOrderModel>>(
                                result.Data.ToString());

                            return View(deliveryOrders);
                        }
                    }
                }
            }
            return View(new List<DeliveryOrderModel>());
        }

        //// GET: DeliveryOrder/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var deliveryOrder = await _context.DeliveryOrders
        //        .Include(d => d.Customer)
        //        .Include(d => d.Document)
        //        .Include(d => d.Payment)
        //        .Include(d => d.ShippingFee)
        //        .Include(d => d.VoucherPromotion)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (deliveryOrder == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(deliveryOrder);
        //}

        //// GET: DeliveryOrder/Create
        //public IActionResult Create()
        //{
        //    ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Email");
        //    ViewData["DocumentId"] = new SelectList(_context.Documents, "Id", "ConsigneeAddress");
        //    ViewData["PaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentMethod");
        //    ViewData["ShippingFeeId"] = new SelectList(_context.ShippingFees, "ShippingFeeId", "ShippingFeeId");
        //    ViewData["VoucherPromotionId"] = new SelectList(_context.VoucherPromotions, "VoucherPromotionId", "VoucherPromotionId");
        //    return View();
        //}

        //// POST: DeliveryOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeliveryOrderModel deliveryOrder)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(deliveryOrder);
                //await _context.SaveChangesAsync();



                return RedirectToAction(nameof(Index));
            }
            //ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Email", deliveryOrder.CustomerId);
            //ViewData["DocumentId"] = new SelectList(_context.Documents, "Id", "ConsigneeAddress", deliveryOrder.DocumentId);
            //ViewData["PaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentMethod", deliveryOrder.PaymentId);
            //ViewData["ShippingFeeId"] = new SelectList(_context.ShippingFees, "ShippingFeeId", "ShippingFeeId", deliveryOrder.ShippingFeeId);
            //ViewData["VoucherPromotionId"] = new SelectList(_context.VoucherPromotions, "VoucherPromotionId", "VoucherPromotionId", deliveryOrder.VoucherPromotionId);
            // return View(deliveryOrder);
            //}

            //// GET: DeliveryOrder/Edit/5
            //public async Task<IActionResult> Edit(int? id)
            //{
            //    if (id == null)
            //    {
            //        return NotFound();
            //    }

            //    var deliveryOrder = await _context.DeliveryOrders.FindAsync(id);
            //    if (deliveryOrder == null)
            //    {
            //        return NotFound();
            //    }
            //    ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Email", deliveryOrder.CustomerId);
            //    ViewData["DocumentId"] = new SelectList(_context.Documents, "Id", "ConsigneeAddress", deliveryOrder.DocumentId);
            //    ViewData["PaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentMethod", deliveryOrder.PaymentId);
            //    ViewData["ShippingFeeId"] = new SelectList(_context.ShippingFees, "ShippingFeeId", "ShippingFeeId", deliveryOrder.ShippingFeeId);
            //    ViewData["VoucherPromotionId"] = new SelectList(_context.VoucherPromotions, "VoucherPromotionId", "VoucherPromotionId", deliveryOrder.VoucherPromotionId);
            //    return View(deliveryOrder);
            //}

            //// POST: DeliveryOrder/Edit/5
            //// To protect from overposting attacks, enable the specific properties you want to bind to.
            //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            //[HttpPost]
            //[ValidateAntiForgeryToken]
            //public async Task<IActionResult> Edit(int id, [Bind("Id,DeliveryOrderId,RecipientAddress,RecipientLongitude,RecipientLatitude,RecipientAppointmentTime,SenderAddress,SenderLongitude,SenderLatitude,CreateDate,DeliveryDate,OrderStatus,TotalAmount,TaxFee,PaymentId,IsPurchased,IsSenderPurchase,IsInternational,VoucherPromotionId,ShippingFeeId,CustomerId,DocumentId")] DeliveryOrder deliveryOrder)
            //{
            //    if (id != deliveryOrder.Id)
            //    {
            //        return NotFound();
            //    }

            //    if (ModelState.IsValid)
            //    {
            //        try
            //        {
            //            _context.Update(deliveryOrder);
            //            await _context.SaveChangesAsync();
            //        }
            //        catch (DbUpdateConcurrencyException)
            //        {
            //            if (!DeliveryOrderExists(deliveryOrder.Id))
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
            //    ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Email", deliveryOrder.CustomerId);
            //    ViewData["DocumentId"] = new SelectList(_context.Documents, "Id", "ConsigneeAddress", deliveryOrder.DocumentId);
            //    ViewData["PaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentMethod", deliveryOrder.PaymentId);
            //    ViewData["ShippingFeeId"] = new SelectList(_context.ShippingFees, "ShippingFeeId", "ShippingFeeId", deliveryOrder.ShippingFeeId);
            //    ViewData["VoucherPromotionId"] = new SelectList(_context.VoucherPromotions, "VoucherPromotionId", "VoucherPromotionId", deliveryOrder.VoucherPromotionId);
            //    return View(deliveryOrder);
            //}

            //// GET: DeliveryOrder/Delete/5
            //public async Task<IActionResult> Delete(int? id)
            //{
            //    if (id == null)
            //    {
            //        return NotFound();
            //    }

            //    var deliveryOrder = await _context.DeliveryOrders
            //        .Include(d => d.Customer)
            //        .Include(d => d.Document)
            //        .Include(d => d.Payment)
            //        .Include(d => d.ShippingFee)
            //        .Include(d => d.VoucherPromotion)
            //        .FirstOrDefaultAsync(m => m.Id == id);
            //    if (deliveryOrder == null)
            //    {
            //        return NotFound();
            //    }

            //    return View(deliveryOrder);
            //}

            //// POST: DeliveryOrder/Delete/5
            //[HttpPost, ActionName("Delete")]
            //[ValidateAntiForgeryToken]
            //public async Task<IActionResult> DeleteConfirmed(int id)
            //{
            //    var deliveryOrder = await _context.DeliveryOrders.FindAsync(id);
            //    if (deliveryOrder != null)
            //    {
            //        _context.DeliveryOrders.Remove(deliveryOrder);
            //    }

            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}

            //private bool DeliveryOrderExists(int id)
            //{
            //    return _context.DeliveryOrders.Any(e => e.Id == id);
            //}
            return Ok();
        }
    }
}
