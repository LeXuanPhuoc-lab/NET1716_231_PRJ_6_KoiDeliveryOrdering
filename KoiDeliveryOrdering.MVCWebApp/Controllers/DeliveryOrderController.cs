using Microsoft.AspNetCore.Mvc;
using KoiDeliveryOrdering.Common;
using Newtonsoft.Json;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.MVCWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using KoiDeliveryOrdering.API.Payloads;
using KoiDeliveryOrdering.API.Models;
using static KoiDeliveryOrdering.API.Payloads.ApiRoute;
using KoiDeliveryOrdering.MVCWebApp.Payloads.Responses;
using KoiDeliveryOrdering.API.Payloads.Requests;
using KoiDeliveryOrdering.MVCWebApp.Utils;
using KoiDeliveryOrdering.MVCWebApp.Payloads.Filters;
using KoiDeliveryOrdering.Business.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;


namespace KoiDeliveryOrdering.MVCWebApp.Controllers
{
    public class DeliveryOrderController : Controller
    {
        private readonly AppSettings _appSettings;
        public DeliveryOrderController(IOptionsMonitor<AppSettings> monitor)
        {
            _appSettings = monitor.CurrentValue;
        }

        public List<ShippingFeeModel> ShippingFees { get; set; } = new();

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

                            // Progress paging 
                            var pagination =
                                PaginatedList<DeliveryOrderModel>.Paging(deliveryOrders ?? new(), 1,
                                    5); // Default 5 record each page

                            // Set ViewData
                            ViewData["PageIndex"] = pagination.PageIndex;
                            ViewData["TotalPage"] = pagination.TotalPage;

                            return View(pagination);
                        }
                    }
                }
            }
            return View(new PaginatedList<DeliveryOrderModel>(new(), 1, 5));
        }

        // GET: DeliveryOrder (With condition)
        public async Task<IActionResult> Search(DeliveryOrderFilter req)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "delivery-orders"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var deliveryOrders = JsonConvert.DeserializeObject<List<DeliveryOrderModel>>(
                                result.Data.ToString());

                            if (deliveryOrders != null && deliveryOrders.Any())
                            {
                                // Check whether is many search or not 
                                if (req.IsManySearch.HasValue && !req.IsManySearch.Value)
                                {
                                    // Searching (if any)
                                    if (!string.IsNullOrEmpty(req.SearchValue))
                                    {
                                        var searchValue = req.SearchValue.Trim();

                                        // Try to parse number 
                                        decimal parsedAmount;
                                        bool isNumericSearch = decimal.TryParse(searchValue, out parsedAmount);

                                        // Try to parse bool
                                        bool parseBool;
                                        bool isBoolean = bool.TryParse(searchValue, out parseBool);

                                        // Perform the search 
                                        deliveryOrders = deliveryOrders
                                            .Where(order =>
                                                // Recipient Information
                                                order.RecipientName.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                                                order.RecipientPhone.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                                                order.RecipientAddress.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||

                                                // Sender Address
                                                string.Join(", ",
                                                    order.SenderInformation.Street,
                                                    order.SenderInformation.Ward,
                                                    order.SenderInformation.District,
                                                    order.SenderInformation.CityProvince)
                                                .Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||

                                                // Tax Fee, Total Amount, Promotion
                                                (isNumericSearch && order.TotalAmount == parsedAmount) ||
                                                (isNumericSearch && order.TaxFee == parsedAmount) ||
                                                (isNumericSearch && order.ShippingFee != null && order.ShippingFee.BaseFee == parsedAmount) ||
                                                (isNumericSearch && order.VoucherPromotion != null && order.VoucherPromotion.PromotionRate == parsedAmount) ||

                                                // IsInternational, IsSenderPay
                                                (isBoolean && order.IsSenderPurchase == parseBool) ||
                                                (isBoolean && order.IsInternational == parseBool) ||

                                                // Other info
                                                (order.Payment != null && order.Payment.PaymentMethod.Contains(searchValue, StringComparison.OrdinalIgnoreCase)) ||
                                                (order.OrderStatus.Contains(searchValue, StringComparison.OrdinalIgnoreCase)))
                                            .ToList();
                                    }
                                }else if(req.IsManySearch.HasValue && req.IsManySearch.Value)
                                {
                                    // Perform the many search 
                                    deliveryOrders = deliveryOrders
                                        .Where(order =>
                                            // Recipient Address 
                                            (string.IsNullOrEmpty(req.SenderAddressSearch) ||
                                             order.RecipientAddress.Contains(req.SenderAddressSearch, StringComparison.OrdinalIgnoreCase)) &&

                                            // Sender Address 
                                            (string.IsNullOrEmpty(req.RecipientAddressSearch) ||
                                             string.Join(", ",
                                                order.SenderInformation.Street,
                                                order.SenderInformation.Ward,
                                                order.SenderInformation.District,
                                                order.SenderInformation.CityProvince)
                                            .Contains(req.RecipientAddressSearch, StringComparison.OrdinalIgnoreCase)) &&

                                            // Order Status 
                                            (string.IsNullOrEmpty(req.OrderStatusSearch) ||
                                             order.OrderStatus.Contains(req.OrderStatusSearch, StringComparison.OrdinalIgnoreCase)) &&

                                            // Payment Method
                                            (string.IsNullOrEmpty(req.PaymentMethodSearch) ||
                                             (order.Payment != null &&
                                              order.Payment.PaymentMethod.Equals(req.PaymentMethodSearch))) &&

                                            // Shipping Fee
                                            (!req.ShippingFeeSearch.HasValue ||
                                             order.ShippingFee != null && order.ShippingFee.BaseFee == req.ShippingFeeSearch.Value) &&

                                            // Total Amount 
                                            (!req.TotalAmountSearch.HasValue ||
                                                order.TotalAmount == req.TotalAmountSearch.Value))
                                        .ToList();
                                }

                                // Sorting (if any)
                                if (!string.IsNullOrEmpty(req.OrderBy))
                                {
                                    deliveryOrders = SortingHelper.SortDeliveryOrderByColumn(deliveryOrders ?? new(), req.OrderBy).ToList();
                                }

                                // Progress paging 
                                var pagination =
                                    PaginatedList<DeliveryOrderModel>.Paging(deliveryOrders ?? new(), req.PageIndex,
                                        5); // Default 5 record each page

                                // Set ViewData
                                ViewData["PageIndex"] = pagination.PageIndex;
                                ViewData["TotalPage"] = pagination.TotalPage;

                                return new JsonResult(new
                                { PageIndex = pagination.PageIndex, TotalPage = pagination.TotalPage, DeliveryOrders = pagination });
                            }
                        }
                    }
                }
            }

            return new JsonResult(new
                { PageIndex = 1, TotalPage = 1, DeliveryOrders = new List<DeliveryOrderModel>() });
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
            await SetDefaultViewDataAsync();

            return View();
        }

        private async Task SetDefaultViewDataAsync()
        {
            ViewData["SenderInformationId"] = new SelectList(await this.GetAllSenderInformationAsync(), "SenderInformationId", "SenderName");
            ViewData["PaymentId"] = new SelectList(await this.GetAllPaymentAsync(), "PaymentId", "PaymentMethod");
            ViewData["OrderStatus"] = new SelectList(await this.GetAllDeliveryOrderStatusesAsync(), "OrderStatus");
            ViewData["DeliveryAppointments"] = new SelectList(await this.GetAllDeliveryAppointmentAsync(), "RecipientAppointmentTime");
            //ViewData["Provinces"] = new SelectList(await this.GetProvincesAsync(), "Code", "Name");
            ViewData["Provinces"] = new SelectList(await this.GetProvincesAsync(), "ProvinceId", "ProvinceName");
            ViewData["AnimalType"] = new SelectList(await this.GetAllAnimalTypeAsync(), "AnimalTypeId", "AnimalTypeDesc");

            // Add Voucher Promotion View Data
            await HandleAddVoucherPromotionViewDataAsync();
            // Add Health Status View Data
            await HandleAddHealthStatusViewDataAsync();
            // Add Shipping Fee View Data
            await HandleAddShippingFeeViewDataAsync();
        }

        private async Task HandleAddShippingFeeViewDataAsync()
        {
            var shippingFees = await this.GetAllShippingFeeAsync();

            var combinedShippingFees = shippingFees.Select(p => new {
                p.ShippingFeeId,
                CombinedDisplayText = $"Range {p.DistanceRangeFrom} - {p.DistanceRangeTo}km; Weight: {p.WeightClass}kg; Fee: {p.BaseFee}₫; Estimate time: {p.EstimatedTime}"
            }).ToList();

            ViewData["ShippingFeeId"] = new SelectList(combinedShippingFees,
                "ShippingFeeId",
                "CombinedDisplayText");
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

                            // Assign to shipping fee prop
                            ShippingFees = shippingFees ?? new();

                            return ShippingFees;
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

        public async Task<List<GHNProvinceModel>> GetProvincesAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync($"{ApiRoute.APIUrl}/{ApiRoute.DeliveryOrder.GetAllProvinceLocal}"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());

                        if (result != null && result.Data != null && result.Status == Const.SUCCESS_READ_CODE)
                        {
                            var ghnProvinces = JsonConvert.DeserializeObject<List<GHNProvinceModel>>(
                                result.Data.ToString()!);

                            return ghnProvinces ?? new List<GHNProvinceModel>();
                        }
                    }
                }
            }

            return new List<GHNProvinceModel>();
        }

        public async Task<GHNProvinceModel> GetProvinceByCodeAsync(int code)
        {
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                    $"{ApiRoute.APIUrl}/{ApiRoute.DeliveryOrder.GetAllProvinceLocal}" + $"/q?code={code}"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());

                        if (result != null && result.Data != null && result.Status == Const.SUCCESS_READ_CODE)
                        {
                            var ghnProvince = JsonConvert.DeserializeObject<GHNProvinceModel>(
                                result.Data.ToString()!);

                            return ghnProvince ?? new GHNProvinceModel();
                        }
                    }
                }
            }

            return new GHNProvinceModel();
        }

        public async Task<GHNDistrictModel> GetDistrictByCodeAsync(int code, int provinceCode)
        {
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                    $"{ApiRoute.APIUrl}/{ApiRoute.DeliveryOrder.GetAllDistrictLocal}" + $"/q?code={code}&provinceCode={provinceCode}"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());

                        if (result != null && result.Data != null && result.Status == Const.SUCCESS_READ_CODE)
                        {
                            var ghnDistrict = JsonConvert.DeserializeObject<GHNDistrictModel>(
                                result.Data.ToString()!);

                            return ghnDistrict ?? new GHNDistrictModel();
                        }
                    }
                }
            }

            return new GHNDistrictModel();
        }

        public async Task<GHNWardModel> GetWardByCodeAsync(int code, int districtCode)
        {
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                    $"{ApiRoute.APIUrl}/{ApiRoute.DeliveryOrder.GetAllWardLocal}" + $"/q?code={code}&districtCode={districtCode}"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());

                        if (result != null && result.Data != null && result.Status == Const.SUCCESS_READ_CODE)
                        {
                            var ghnWard = JsonConvert.DeserializeObject<GHNWardModel>(
                                result.Data.ToString()!);

                            return ghnWard ?? new GHNWardModel();
                        }
                    }
                }
            }

            return new GHNWardModel();
        }

        public async Task<(string longtitude, string latitude)> GetLongAndLatByAddressAsync(string address)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("MyApp/1.0 (stevenmarks147@gmail.com)");

                using (var resp = await client.GetAsync(ApiRoute.NominatimMap.NominatimUrl +
                    $"/search?q={Uri.EscapeDataString(address)}&format=json&limit=1"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        string content = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<LocationResponse>>(content.ToString());
                        if (result != null && result.Any())
                        {
                            var firstEle = result.First();

                            return (firstEle.Lon, firstEle.Lat);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error retrieving data from Nominatim API");
                    }
                }
            }

            return (string.Empty, string.Empty);
        }

        // POST: DeliveryOrder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDeliveryOrderRequest deliveryOrderReq)
        {
            if (!ModelState.IsValid)
            {
                //return View();

                return await this.Create();
            }

            if (!deliveryOrderReq.Animals.Any())
            {
                ModelState.AddModelError("Animals", "Please add animal for the order");

                return await this.Create();
            }

            // Handling request address (Province, District, Ward) by code
            int.TryParse(deliveryOrderReq.ProvinceName, out var parsedProvinceCode);
            int.TryParse(deliveryOrderReq.DistrictName, out var parsedDistrictCode);
            int.TryParse(deliveryOrderReq.WardName, out var parsedWardCode);

            if (parsedProvinceCode > 0 
                && parsedDistrictCode > 0 
                && parsedWardCode > 0)
            {
                var provinceModel = await GetProvinceByCodeAsync(parsedProvinceCode);
                var districtModel = await GetDistrictByCodeAsync(parsedDistrictCode, parsedProvinceCode);
                var wardModel = await GetWardByCodeAsync(parsedWardCode, parsedDistrictCode);

                deliveryOrderReq.ProvinceName = 
                    provinceModel != null ? provinceModel.ProvinceName : string.Empty;
                deliveryOrderReq.DistrictName =
                    districtModel != null ? districtModel.DistrictName : string.Empty;
                deliveryOrderReq.WardName =
                    wardModel != null ? wardModel.WardName : string.Empty;
            }

            // Retrieve longtitude and latitude base on pure address string
            var completeAddr =
                $"{deliveryOrderReq.RecipientAddress}, {deliveryOrderReq.WardName}, " +
                $"{deliveryOrderReq.DistrictName}, {deliveryOrderReq.ProvinceName}";
            var longLatResp = await GetLongAndLatByAddressAsync(completeAddr);
            // Assign long, lat for user order
            if(!string.IsNullOrEmpty(longLatResp.latitude) 
                && !string.IsNullOrEmpty(longLatResp.longtitude))
            {
                deliveryOrderReq.RecipientLongitude = double.Parse(longLatResp.longtitude);
                deliveryOrderReq.RecipientLatitude = double.Parse(longLatResp.latitude);

                // Assign complete address
                deliveryOrderReq.RecipientAddress = completeAddr;
            }
            else
            {
                ModelState.AddModelError("RecipientAddress", $"Not found address: {completeAddr}");
                return await this.Create();
            }


            bool saveStatus = false;
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.PostAsJsonAsync(
                           Const.APIEndpoint + "delivery-orders", deliveryOrderReq))
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

        // GET: DeliveryOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UpdateDeliveryOrderRequest? toUpdatedeliveryOrder = null!;
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
                            toUpdatedeliveryOrder = JsonConvert.DeserializeObject<UpdateDeliveryOrderRequest>(
                                result.Data.ToString()!);
                        }
					}
				}
			}


            // Set default view data
            await SetDefaultViewDataAsync();

            return toUpdatedeliveryOrder != null
				 ? View(toUpdatedeliveryOrder)
				 : NotFound();
		}

        // POST: DeliveryOrder/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateDeliveryOrderRequest toUpdateDeliveryOrder)
        {
            if (!ModelState.IsValid)
            {
                return await this.Edit(id);
            }

            if (!toUpdateDeliveryOrder.DeliveryOrderDetails.Any())
            {
                ModelState.AddModelError("DeliveryOrderDetails", "Not allow to remove all delivery order detail");
                return await this.Edit(id);
            }

            // Handling request address (Province, District, Ward) by code (if any)
            var isUpdateAddress = false;
            // Handling request address (Province, District, Ward) by code
            int.TryParse(toUpdateDeliveryOrder.ToUpdateProvinceName, out var parsedProvinceCode);
            int.TryParse(toUpdateDeliveryOrder.ToUpdateDistrictName, out var parsedDistrictCode);
            int.TryParse(toUpdateDeliveryOrder.ToUpdateWardName, out var parsedWardCode);

            if (parsedProvinceCode > 0
                && parsedDistrictCode > 0
                && parsedWardCode > 0)
            {
                var provinceModel = await GetProvinceByCodeAsync(parsedProvinceCode);
                var districtModel = await GetDistrictByCodeAsync(parsedDistrictCode, parsedProvinceCode);
                var wardModel = await GetWardByCodeAsync(parsedWardCode, parsedDistrictCode);

                toUpdateDeliveryOrder.ProvinceName =
                    provinceModel != null ? provinceModel.ProvinceName : string.Empty;
                toUpdateDeliveryOrder.DistrictName =
                    districtModel != null ? districtModel.DistrictName : string.Empty;
                toUpdateDeliveryOrder.WardName =
                    wardModel != null ? wardModel.WardName : string.Empty;

                // Mark as update address
                isUpdateAddress = true;
            }

            // Retrieve longtitude and latitude base on pure address string
            var completeAddr = isUpdateAddress
                ? $"{toUpdateDeliveryOrder.RecipientAddress}, {toUpdateDeliveryOrder.WardName}, " +
                  $"{toUpdateDeliveryOrder.DistrictName}, {toUpdateDeliveryOrder.ProvinceName}"
                : toUpdateDeliveryOrder.RecipientAddress;
            var longLatResp = await GetLongAndLatByAddressAsync(completeAddr);
            // Assign long, lat for user order
            if (!string.IsNullOrEmpty(longLatResp.latitude)
                && !string.IsNullOrEmpty(longLatResp.longtitude))
            {
                toUpdateDeliveryOrder.RecipientLongitude = double.Parse(longLatResp.longtitude);
                toUpdateDeliveryOrder.RecipientLatitude = double.Parse(longLatResp.latitude);

                // Re-assign complete address
                toUpdateDeliveryOrder.RecipientAddress = completeAddr;
            }
            else
            {
                ModelState.AddModelError("RecipientAddress", $"Not found address: {completeAddr}");
                return await this.Edit(id);
            }

            bool saveStatus = false;
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.PutAsJsonAsync(
                           Const.APIEndpoint + "delivery-orders", toUpdateDeliveryOrder))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context);

                        if (result != null && result.Status == Const.SUCCESS_UPDATE_CODE)
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

                return await this.Edit(id);
            }
        }

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
    }
}
