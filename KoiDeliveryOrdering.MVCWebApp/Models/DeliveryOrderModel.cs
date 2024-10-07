using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.MVCWebApp.Models
{
    public class DeliveryOrderModel
    {
        public int Id { get; set; }

        public Guid DeliveryOrderId { get; set; }

        public string RecipientName { get; set; } = null!;

        public string RecipientPhone { get; set; } = null!;

        public string RecipientAddress { get; set; } = null!;

        public double? RecipientLongitude { get; set; }

        public double? RecipientLatitude { get; set; }

        public string? RecipientAppointmentTime { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string OrderStatus { get; set; } = null!;

        public decimal? TotalAmount { get; set; }

        public decimal? TaxFee { get; set; }

        public int PaymentId { get; set; }

        public bool? IsPurchased { get; set; }

        public bool IsSenderPurchase { get; set; }

        public bool IsInternational { get; set; }

        public int? VoucherPromotionId { get; set; }

        public int ShippingFeeId { get; set; }

        public int SenderInformationId { get; set; }

        public int? DocumentId { get; set; }

        public virtual SenderInformationModel SenderInformation { get; set; } = null!;

        public virtual ICollection<DeliveryOrderDetailModel> DeliveryOrderDetails { get; set; } = new List<DeliveryOrderDetailModel>();

        public virtual DocumentModel? Document { get; set; }

        //public virtual ICollection<OrderAssignment> OrderAssignments { get; set; } = new List<OrderAssignment>();

        public virtual PaymentModel Payment { get; set; } = null!;

        public virtual ShippingFeeModel ShippingFee { get; set; } = null!;

        //public virtual VoucherPromotion? VoucherPromotion { get; set; }
    }
}
