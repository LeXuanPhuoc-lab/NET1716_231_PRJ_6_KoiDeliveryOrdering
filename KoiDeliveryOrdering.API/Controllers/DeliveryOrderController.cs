using KoiDeliveryOrdering.API.Models;
using KoiDeliveryOrdering.API.Payloads;
using KoiDeliveryOrdering.API.Payloads.Requests;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Interfaces;
using KoiDeliveryOrdering.Business.Models;
using KoiDeliveryOrdering.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static KoiDeliveryOrdering.API.Payloads.ApiRoute;

namespace KoiDeliveryOrdering.API.Controllers
{
    [ApiController]
    public class DeliveryOrderController : ControllerBase
    {
        private readonly IDeliveryOrderService _deliveryOrderService;
        private readonly IShippingFeeService _shippingFeeService;
        private readonly IPaymentService _paymentService;
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;

        public DeliveryOrderController(
            IDeliveryOrderService deliveryOrderService,
            IShippingFeeService shippingFeeService,
            IPaymentService paymentService,
            IUserService userService,
            IOptionsMonitor<AppSettings> monitor)
        {
            _deliveryOrderService = deliveryOrderService;
            _shippingFeeService = shippingFeeService;
            _paymentService = paymentService;
            _userService = userService;
            _appSettings = monitor.CurrentValue;
        }

        [HttpGet(ApiRoute.DeliveryOrder.GetAll)]
        public async Task<IServiceResult> GetAllDeliveryOrderAsync()
        {
            return await _deliveryOrderService.FindAllAsync();
        }

        [HttpGet(ApiRoute.DeliveryOrder.GetById)]
        public async Task<IServiceResult> GetDeliveryOrderByIdAsync([FromRoute] int id)
        {
            return await _deliveryOrderService.FindAsync(id);
        }

        [HttpPost(ApiRoute.DeliveryOrder.Insert)]
        public async Task<IServiceResult> InsertDeliveryOrderAsync([FromBody] CreateDeliveryOrderRequest req)
        {
            return await _deliveryOrderService.InsertAsync(req.ToDeliveryOrder());
        }

        [HttpPut(ApiRoute.DeliveryOrder.Update)]
        public async Task<IServiceResult> UpdateDeliveryOrderAsync([FromBody] UpdateDeliveryOrderRequest req)
        {
            return await _deliveryOrderService.UpdateAsync(req.ToDeliveryOrder());
        }

        [HttpDelete(ApiRoute.DeliveryOrder.Remove)]
        public async Task<IServiceResult> RemoveDeliveryOrderAsync([FromRoute] int id)
        {
            return await _deliveryOrderService.RemoveAsync(id);
        }

        [HttpGet(ApiRoute.DeliveryOrder.GetAllPayment)]
        public async Task<IServiceResult> GetAllPaymentAsync()
        {
            return await _paymentService.FindAllAsync();
        }

        [HttpGet(ApiRoute.DeliveryOrder.GetAllShippingFee)]
        public async Task<IServiceResult> GetAllShippingFeeAsync()
        {
            return await _shippingFeeService.FindAllAsync();
        }

        [HttpGet(ApiRoute.DeliveryOrder.GetAllOrderStatus)]
        public async Task<IServiceResult> GetAllOrderStatusAsync()
        {
            return await _deliveryOrderService.FindAllDeliveryOrderStatusesAsync();
        }

        [HttpGet(ApiRoute.DeliveryOrder.GetAllAppointmentTime)]
        public async Task<IServiceResult> GetAllAppointmentTimeAsync()
        {
            return await _deliveryOrderService.FindAllAppointmentTimeAsync();
        }

        [HttpGet(ApiRoute.DeliveryOrder.GetAllVoucherByUsername)]
        public async Task<IServiceResult> GetAllVoucherByUsernameAsync([FromRoute] string username)
        {
            return await _userService.FindAllVoucherByUsernameAsync(username);
        }

        [HttpGet(ApiRoute.DeliveryOrder.GetAllProvinceLocal)]
        public async Task<IServiceResult> GetAllProvinceAsync()
        {
            using (var httpClient = new HttpClient())
            {
                // Add token to HTTP Headers
                httpClient.DefaultRequestHeaders.Add("token", _appSettings.GHNToken);

                //using (var resp = await httpClient.GetAsync(ApiRoute.VietnameProvincesOnline.GetListProvinces))
                using (var resp = await httpClient.GetAsync(ApiRoute.GiaoHangNhanh.GetProvince))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        //var result = JsonConvert.DeserializeObject<List<ProvinceModel>>(context.ToString());

                        // Parse response JSON to object
                        var jsonObject = JsonConvert.DeserializeObject<JObject>(context.ToString());

                        // Check if 'data' field exist
                        if (jsonObject != null && jsonObject["data"] != null)
                        {
                            var result = jsonObject["data"]!.ToObject<List<GHNProvinceModel>>();

                            if (result != null)
                            {
                                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result ?? new List<GHNProvinceModel>());
                            }
                        }
                    }
                }
            }

            return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new List<GHNProvinceModel>());
        }

        [HttpGet(ApiRoute.DeliveryOrder.GetProvinceByCodeLocal)]
        public async Task<IServiceResult> GetProvinceByCodeAsync([FromQuery] int code)
        {
            using (var httpClient = new HttpClient())
            {
                // Add token to HTTP Headers
                httpClient.DefaultRequestHeaders.Add("token", _appSettings.GHNToken);

                //using (var resp = await httpClient.GetAsync(ApiRoute.VietnameProvincesOnline.GetListProvinces))
                using (var resp = await httpClient.GetAsync(ApiRoute.GiaoHangNhanh.GetProvince))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        //var result = JsonConvert.DeserializeObject<List<ProvinceModel>>(context.ToString());

                        // Parse response JSON to object
                        var jsonObject = JsonConvert.DeserializeObject<JObject>(context.ToString());

                        // Check if 'data' field exist
                        if (jsonObject != null && jsonObject["data"] != null)
                        {
                            var result = jsonObject["data"]!.ToObject<List<GHNProvinceModel>>();

                            if (result != null)
                            {
                                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result.FirstOrDefault(x => x.ProvinceId == code));
                            }
                        }
                    }
                }
            }

            return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new GHNProvinceModel());
        }

        [HttpGet(ApiRoute.DeliveryOrder.GetAllDistrictLocal)]
        public async Task<IServiceResult> GetAllDistrictAsync([FromQuery] int provinceCode)
        {
            using (var httpClient = new HttpClient())
            {
                // Add token to HTTP Headers
                httpClient.DefaultRequestHeaders.Add("token", _appSettings.GHNToken);

                //using (var resp = await httpClient.GetAsync(
                //    VietnameProvincesOnline.GetDistrictByCode + $"?district_id={code}"))
                using (var resp = await httpClient.GetAsync(
                    GiaoHangNhanh.GetDistrict + $"?province_id={provinceCode}"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();

                        // Parse response JSON to object
                        var jsonObject = JsonConvert.DeserializeObject<JObject>(context.ToString());

                        // Check if 'data' field exist
                        if (jsonObject != null && jsonObject["data"] != null)
                        {
                            var result = jsonObject["data"]!.ToObject<List<GHNDistrictModel>>();

                            if (result != null)
                            {
                                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result ?? new List<GHNDistrictModel>());
                            }
                        }
                    }
                }
            }

            return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new List<GHNDistrictModel>());
        }

        [HttpGet(ApiRoute.DeliveryOrder.GetDistrictByCodeLocal)]
        public async Task<IServiceResult> GetDistrictByCodeAsync([FromQuery] int code, [FromQuery] int provinceCode)
        {
            using (var httpClient = new HttpClient())
            {
                // Add token to HTTP Headers
                httpClient.DefaultRequestHeaders.Add("token", _appSettings.GHNToken);

                //using (var resp = await httpClient.GetAsync(
                //    VietnameProvincesOnline.GetDistrictByCode + $"?district_id={code}"))
                using (var resp = await httpClient.GetAsync(
                    GiaoHangNhanh.GetDistrict + $"?province_id={provinceCode}"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();

                        // Parse response JSON to object
                        var jsonObject = JsonConvert.DeserializeObject<JObject>(context.ToString());

                        // Check if 'data' field exist
                        if (jsonObject != null && jsonObject["data"] != null)
                        {
                            var result = jsonObject["data"]!.ToObject<List<GHNDistrictModel>>();

                            if (result != null)
                            {
                                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result.FirstOrDefault(x => x.DistrictId == code));
                            }
                        }
                    }
                }
            }

            return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new GHNDistrictModel());
        }

        [HttpGet(ApiRoute.DeliveryOrder.GetAllWardLocal)]
        public async Task<IServiceResult> GetAllWardAsync([FromQuery] int districtCode)
        {
            using (var httpClient = new HttpClient())
            {
                // Add token to HTTP Headers
                httpClient.DefaultRequestHeaders.Add("token", _appSettings.GHNToken);

                //using (var resp = await httpClient.GetAsync(
                //    VietnameProvincesOnline.VPOnlineBaseUrl + $"/w/{code}"))
                using (var resp = await httpClient.GetAsync(
                    GiaoHangNhanh.GetWard + $"?district_id={districtCode}"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();

                        // Parse response JSON to object
                        var jsonObject = JsonConvert.DeserializeObject<JObject>(context.ToString());

                        // Check if 'data' field exist
                        if (jsonObject != null && jsonObject["data"] != null)
                        {
                            var result = jsonObject["data"]!.ToObject<List<GHNWardModel>>();

                            if (result != null)
                            {
                                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result ?? new List<GHNWardModel>());
                            }
                        }
                    }
                }
            }

            return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new List<GHNWardModel>());
        }

        [HttpGet(ApiRoute.DeliveryOrder.GetWardByCodeLocal)]
        public async Task<IServiceResult> GetWardByCodeAsync([FromQuery] int code, [FromQuery] int districtCode)
        {
            using (var httpClient = new HttpClient())
            {
                // Add token to HTTP Headers
                httpClient.DefaultRequestHeaders.Add("token", _appSettings.GHNToken);

                //using (var resp = await httpClient.GetAsync(
                //    VietnameProvincesOnline.VPOnlineBaseUrl + $"/w/{code}"))
                using (var resp = await httpClient.GetAsync(
                    GiaoHangNhanh.GetWard + $"?district_id={districtCode}"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();

                        // Parse response JSON to object
                        var jsonObject = JsonConvert.DeserializeObject<JObject>(context.ToString());

                        // Check if 'data' field exist
                        if (jsonObject != null && jsonObject["data"] != null)
                        {
                            var result = jsonObject["data"]!.ToObject<List<GHNWardModel>>();

                            if (result != null)
                            {
                                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result.FirstOrDefault(x => x.WardCode == code));
                            }
                        }
                    }
                }
            }

            return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new GHNWardModel());
        }
    }
}
