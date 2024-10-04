using Microsoft.AspNetCore.Mvc;
using KoiDeliveryOrdering.Common;
using Newtonsoft.Json;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.MVCWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using KoiDeliveryOrdering.Data.Entities;
using KoiDeliveryOrdering.API.Payloads.Requests;
using KoiDeliveryOrdering.API.Payloads;
using KoiDeliveryOrdering.API.Models;
using KoiDeliveryOrdering.Business.Contants;

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

        // GET: DeliveryOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DeliveryOrderModel? deliveryOrder = null!;
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                           Const.APIEndpoint + "delivery-orders/" + id))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            deliveryOrder = JsonConvert.DeserializeObject<DeliveryOrderModel>(
                                result.Data.ToString()!);
                        }
                    }
                }
            }

            return deliveryOrder != null
                ? View(deliveryOrder)
                : NotFound();
        }

        // GET: DeliveryOrder/Create
        public async Task<IActionResult> Create()
        {
            // ViewData["DocumentId"] = new SelectList(_context.Documents, "Id", "ConsigneeAddress");
            ViewData["SenderInformationId"] = new SelectList(await this.GetAllSenderInformationAsync(), "SenderInformationId", "SenderName");
            ViewData["PaymentId"] = new SelectList(await this.GetAllPaymentAsync(), "PaymentId", "PaymentMethod");
            ViewData["ShippingFeeId"] = new SelectList(await this.GetAllShippingFeeAsync(), "ShippingFeeId", "BaseFee");
            ViewData["OrderStatus"] = new SelectList(await this.GetAllDeliveryOrderStatusesAsync(), "OrderStatus");
            ViewData["DeliveryAppointments"] = new SelectList(await this.GetAllDeliveryAppointmentAsync(), "RecipientAppointmentTime");
            ViewData["Provinces"] = new SelectList(await this.GetProvincesAsync(), "Code", "Name");
            ViewData["AnimalType"] = new SelectList(await this.GetAllAnimalTypeAsync(), "AnimalTypeId", "AnimalTypeDesc");

            // Add Voucher Promotion View Data
            await HandleAddVoucherPromotionViewDataAsync();
            // Add Health Status View Data
            await HandleAddHealthStatusViewDataAsync();


            return View();
        }

        private async Task HandleAddHealthStatusViewDataAsync()
        {
            var healthStatusList = await this.GetAllHealthStatusAsync();
            ViewData["HealthStatus"] = new SelectList(healthStatusList.Select(status => new { Value = status, Text = status }), "Value", "Text");
        }
        
        private async Task HandleAddVoucherPromotionViewDataAsync()
        {
            var promotions = await this.GetAllVoucherPromotionByUsernameAsync();

            var combinedPromotions = promotions.Select(p => new {
                p.VoucherPromotionId,
                CombinedDisplayText = $"Code: {p.VoucherPromotionCode} - Reduction rate: {p.PromotionRate}%"
            }).ToList();

            ViewData["VoucherPromotionId"] = new SelectList(combinedPromotions,
                "VoucherPromotionId",
                "CombinedDisplayText");
        }

        public async Task<List<PaymentModel>> GetAllPaymentAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                           Const.APIEndpoint + "payments"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if(result != null && result.Data != null)
                        {
                            var deliveryOrders = JsonConvert.DeserializeObject<List<PaymentModel>>(
                                result.Data.ToString()!);

                            return deliveryOrders ?? new List<PaymentModel>();
                        }
                    }
                }
            }
            
            return new List<PaymentModel>();
        }

        public async Task<List<ShippingFeeModel>> GetAllShippingFeeAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                           Const.APIEndpoint + "shipping-fees"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if(result != null && result.Data != null)
                        {
                            var shippingFees = JsonConvert.DeserializeObject<List<ShippingFeeModel>>(
                                result.Data.ToString()!);

                            return shippingFees ?? new List<ShippingFeeModel>();
                        }
                    }
                }
            }
            
            return new List<ShippingFeeModel>();
        }

        public async Task<List<string>> GetAllDeliveryOrderStatusesAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                           Const.APIEndpoint + "delivery-orders/statuses"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var orderStatuses = JsonConvert.DeserializeObject<List<string>>(
                                result.Data.ToString()!);

                            return orderStatuses ?? new List<string>();
                        }
                    }
                }
            }

            return new List<string>();
        }

        public async Task<List<string>> GetAllDeliveryAppointmentAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                           Const.APIEndpoint + "delivery-orders/appointment"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var orderStatuses = JsonConvert.DeserializeObject<List<string>>(
                                result.Data.ToString()!);

                            return orderStatuses ?? new List<string>();
                        }
                    }
                }
            }

            return new List<string>();
        }

        public async Task<List<string>> GetAllHealthStatusAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                           Const.APIEndpoint + "animals/health-statuses"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var healthStatuses = JsonConvert.DeserializeObject<List<string>>(
                                result.Data.ToString()!);

                            return healthStatuses ?? new List<string>();
                        }
                    }
                }
            }

            return new List<string>();
        }

        public async Task<List<SenderInformationModel>> GetAllSenderInformationAsync()
        {
			using (var httpClient = new HttpClient())
			{
				using (var resp = await httpClient.GetAsync(
						   Const.APIEndpoint + "users/sender-informations"))
				{
					if (resp.IsSuccessStatusCode)
					{
						var context = await resp.Content.ReadAsStringAsync();
						var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
						if (result != null && result.Data != null)
						{
							var senderInformations = JsonConvert.DeserializeObject<List<SenderInformationModel>>(
								result.Data.ToString()!);

							return senderInformations ?? new List<SenderInformationModel>();
						}
					}
				}
			}

			return new List<SenderInformationModel>();
		}

        public async Task<List<ProvinceModel>> GetProvincesAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(ApiRoute.VietnameProvincesOnline.GetListProvinces))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<ProvinceModel>>(context.ToString());
                        if (result != null)
                        {
                            return result ?? new List<ProvinceModel>();
                        }
                    }
                }
            }

            return new List<ProvinceModel>();
        }

        public async Task<List<VoucherPromotionModel>> GetAllVoucherPromotionByUsernameAsync()
        {
            // Get username from session
            var username = HttpContext.Session.GetString("Username") ?? string.Empty;

            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                           Const.APIEndpoint + "delivery-orders/vouchers/" + username))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var voucherPromotions = JsonConvert.DeserializeObject<List<VoucherPromotionModel>>(
                                result.Data.ToString()!);

                            return voucherPromotions ?? new List<VoucherPromotionModel>();
                        }
                    }
                }
            }

            return new List<VoucherPromotionModel>();
        }

        public async Task<List<AnimalTypeModel>> GetAllAnimalTypeAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                           Const.APIEndpoint + "animals/types"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var animalTypes = JsonConvert.DeserializeObject<List<AnimalTypeModel>>(
                                result.Data.ToString()!);

                            return animalTypes ?? new List<AnimalTypeModel>();
                        }
                    }
                }
            }

            return new List<AnimalTypeModel>();
        }



        // POST: DeliveryOrder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDeliveryOrderRequest deliveryOrder)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Add missing properties required
            deliveryOrder.CreateDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, 
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            deliveryOrder.TotalAmount = 0;
            deliveryOrder.TaxFee = (decimal?) 0.1;
            deliveryOrder.IsPurchased = false;
            deliveryOrder.OrderStatus = OrderStatusConstants.Pending;


            bool saveStatus = false;
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.PostAsJsonAsync(
                           Const.APIEndpoint + "delivery-orders", deliveryOrder))
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
                ViewData["SenderInformationId"] = new SelectList(await this.GetAllSenderInformationAsync(), "SenderInformationId", "SenderName");
                ViewData["PaymentId"] = new SelectList(await this.GetAllPaymentAsync(), "PaymentId", "PaymentMethod");
                ViewData["ShippingFeeId"] = new SelectList(await this.GetAllShippingFeeAsync(), "ShippingFeeId", "BaseFee");
                ViewData["OrderStatus"] = new SelectList(await this.GetAllDeliveryOrderStatusesAsync(), "OrderStatus");
                ViewData["DeliveryAppointments"] = new SelectList(await this.GetAllDeliveryAppointmentAsync(), "RecipientAppointmentTime");
                ViewData["Provinces"] = new SelectList(await this.GetProvincesAsync(), "Code", "Name");
                ViewData["AnimalType"] = new SelectList(await this.GetAllAnimalTypeAsync(), "AnimalTypeId", "AnimalTypeDesc");

                // Add Voucher Promotion View Data
                await HandleAddVoucherPromotionViewDataAsync();
                // Add Health Status View Data
                await HandleAddHealthStatusViewDataAsync();

                return View();
            }
        }

        // GET: DeliveryOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			DeliveryOrderModel? deliveryOrder = null!;
			using (var httpClient = new HttpClient())
			{
				using (var resp = await httpClient.GetAsync(
						   Const.APIEndpoint + "delivery-orders/" + id))
				{
					if (resp.IsSuccessStatusCode)
					{
						var context = await resp.Content.ReadAsStringAsync();
						var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
						if (result != null && result.Data != null)
						{
							deliveryOrder = JsonConvert.DeserializeObject<DeliveryOrderModel>(
								result.Data.ToString()!);
						}
					}
				}
			}

			//ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Email", deliveryOrder.CustomerId);
			//ViewData["DocumentId"] = new SelectList(_context.Documents, "Id", "ConsigneeAddress", deliveryOrder.DocumentId);
			//ViewData["PaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentMethod", deliveryOrder.PaymentId);
			//ViewData["ShippingFeeId"] = new SelectList(_context.ShippingFees, "ShippingFeeId", "ShippingFeeId", deliveryOrder.ShippingFeeId);
			//ViewData["VoucherPromotionId"] = new SelectList(_context.VoucherPromotions, "VoucherPromotionId", "VoucherPromotionId", deliveryOrder.VoucherPromotionId);

			ViewData["PaymentId"] = new SelectList(await this.GetAllPaymentAsync(), "PaymentId", "PaymentMethod");
			ViewData["ShippingFeeId"] = new SelectList(await this.GetAllShippingFeeAsync(), "ShippingFeeId", "BaseFee");
            ViewData["OrderStatus"] = new SelectList(await this.GetAllDeliveryOrderStatusesAsync(), "OrderStatus");

            return deliveryOrder != null
				 ? View(deliveryOrder)
				 : NotFound();
		}

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

        // GET: DeliveryOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DeliveryOrderModel? deliveryOrder = null!;
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                           Const.APIEndpoint + "delivery-orders/" + id))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            deliveryOrder = JsonConvert.DeserializeObject<DeliveryOrderModel>(
                                result.Data.ToString()!);
                        }
                    }
                }
            }

            return deliveryOrder != null
                ? View(deliveryOrder)
                : NotFound();
        }

        // POST: DeliveryOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleteStatus = false;

            using(var httpClient = new HttpClient())
            {
                using(var response = await httpClient.DeleteAsync(
                    Const.APIEndpoint + "delivery-orders/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(content);
                        if(result != null && result.Status == Const.SUCCESS_REMOVE_CODE)
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

        //private bool DeliveryOrderExists(int id)
        //{
        //    return _context.DeliveryOrders.Any(e => e.Id == id);
        //}
    }
}
