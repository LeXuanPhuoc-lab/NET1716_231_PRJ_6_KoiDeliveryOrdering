using KoiDeliveryOrdering.API.Models;
using KoiDeliveryOrdering.API.Payloads;
using KoiDeliveryOrdering.API.Payloads.Requests;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Contants;
using KoiDeliveryOrdering.Business.Interfaces;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.Data.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KoiDeliveryOrdering.API.Controllers
{
    [ApiController]
    public class DeliveryOrderController : ControllerBase
    {
        private readonly IDeliveryOrderService _deliveryOrderService;
        private readonly IShippingFeeService _shippingFeeService;
        private readonly IPaymentService _paymentService;
        private readonly IUserService _userService;

        public DeliveryOrderController(
            IDeliveryOrderService deliveryOrderService,
            IShippingFeeService shippingFeeService,
            IPaymentService paymentService,
            IUserService userService)
        {
            _deliveryOrderService = deliveryOrderService;
            _shippingFeeService = shippingFeeService;
            _paymentService = paymentService;
            _userService = userService;
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
            // Map to DeliveryOrder entity
            var deliveryOrderEntity = req.Adapt<DeliveryOrder>();
            return await _deliveryOrderService.InsertAsync(deliveryOrderEntity);
        }

        [HttpPut(ApiRoute.DeliveryOrder.Update)]
        public async Task<IServiceResult> UpdateDeliveryOrderAsync([FromBody] DeliveryOrder req)
        {
            return await _deliveryOrderService.UpdateAsync(req);
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

        [HttpGet(ApiRoute.DeliveryOrder.GetProvinceByCodeLocal)]
        public async Task<IServiceResult> GetProvinceByCodeAsync([FromRoute] int code)
        {
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                    ApiRoute.VietnameProvincesOnline.VPOnlineBaseUrl + $"/p/{code}?depth=2"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();

                        var result = JsonConvert.DeserializeObject<ProvinceModel>(context.ToString());
                        if (result != null)
                        {
                            return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
                        }
                    }
                }
            }

            return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new ProvinceModel());
        }

        [HttpGet(ApiRoute.DeliveryOrder.GetDistrictByCodeLocal)]
        public async Task<IServiceResult> GetDistrictByCodeAsync([FromRoute] int code)
        {
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                    ApiRoute.VietnameProvincesOnline.VPOnlineBaseUrl + $"/d/{code}?depth=2"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();

                        var result = JsonConvert.DeserializeObject<DistrictModel>(context.ToString());
                        if (result != null)
                        {
                            return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
                        }
                    }
                }
            }

            return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new DistrictModel());
        }
    }
}
