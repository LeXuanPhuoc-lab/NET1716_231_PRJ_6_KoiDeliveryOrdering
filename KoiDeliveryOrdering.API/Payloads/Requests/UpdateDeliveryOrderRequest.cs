using KoiDeliveryOrdering.Business.Contants;
using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.API.Payloads.Requests
{
    public class UpdateDeliveryOrderRequest
    {
        public int Id { get; set; }
        public Guid DeliveryOrderId { get; set; }
        public string RecipientAddress { get; set; } = null!;

        public string RecipientName { get; set; } = null!;

        public string RecipientPhone { get; set; } = null!;

        public double? RecipientLongitude { get; set; }

        public double? RecipientLatitude { get; set; }

        public string? RecipientAppointmentTime { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string? OrderStatus { get; set; }

        public decimal? TotalAmount { get; set; }

        public decimal? TaxFee { get; set; }

        public int? PaymentId { get; set; }

        public bool? IsPurchased { get; set; }

        public bool IsSenderPurchase { get; set; }

        public bool IsInternational { get; set; }

        public int? VoucherPromotionId { get; set; }

        public int ShippingFeeId { get; set; }

        public int SenderInformationId { get; set; }

        public int? DocumentId { get; set; }

        // Initiate address handling properities 
        public string? ProvinceName { get; set; }
        public string? DistrictName { get; set; }
        public string? WardName { get; set; }

        public string? ToUpdateProvinceName { get; set; }
        public string? ToUpdateDistrictName { get; set; }
        public string? ToUpdateWardName { get; set; }

        //public List<UpdateAnimalRequest> Animals { get; set; } = new();
        public List<UpdateDeliveryOrderDetailRequest> DeliveryOrderDetails { get; set; } = new();
    }

    public static class UpdateDeliveryOrderRequestExtension
    {
        public static DeliveryOrder ToDeliveryOrder(
            this UpdateDeliveryOrderRequest req)
        {
            var createAtDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
                    TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));

            return new DeliveryOrder
            {
                // Add default properties
                CreateDate = createAtDate,
                TaxFee = (decimal?)10,
                IsPurchased = false,
                OrderStatus = OrderStatusConstants.Pending,

                // Add user order properties
                Id = req.Id,
                DeliveryOrderId = req.DeliveryOrderId,
                TotalAmount = req.TotalAmount ?? 0,
                DeliveryDate = req.DeliveryDate,
                RecipientName = req.RecipientName,
                RecipientPhone = req.RecipientPhone,
                RecipientAddress = req.RecipientAddress,
                RecipientLongitude = req.RecipientLongitude,
                RecipientLatitude = req.RecipientLatitude,
                PaymentId = req.PaymentId,
                RecipientAppointmentTime = req.RecipientAppointmentTime,
                IsSenderPurchase = req.IsSenderPurchase,
                IsInternational = req.IsInternational,
                VoucherPromotionId = req.VoucherPromotionId,
                ShippingFeeId = req.ShippingFeeId,
                SenderInformationId = req.SenderInformationId,
                DeliveryOrderDetails = req.DeliveryOrderDetails.ToListDeliveryOrderDetail(),
            };
        }
    }
}
