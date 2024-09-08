using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Data.Entities;

public partial class DeliveryOrder
{
    public int Id { get; set; }

    public Guid DeliveryOrderId { get; set; }

    public string RecipientAddress { get; set; } = null!;

    public double? RecipientLongitude { get; set; }

    public double? RecipientLatitude { get; set; }

    public string? RecipientAppointmentTime { get; set; }

    public string SenderAddress { get; set; } = null!;

    public double? SenderLongitude { get; set; }

    public double? SenderLatitude { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public string OrderStatus { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public decimal? TaxFee { get; set; }

    public int PaymentId { get; set; }

    public bool? IsPurchased { get; set; }

    public bool? IsSenderPurchase { get; set; }

    public bool IsInternational { get; set; }

    public int? VoucherPromotionId { get; set; }

    public int ShippingFeeId { get; set; }

    public int CustomerId { get; set; }

    public int? DocumentId { get; set; }

    public virtual User Customer { get; set; } = null!;

    public virtual ICollection<DeliveryOrderDetail> DeliveryOrderDetails { get; set; } = new List<DeliveryOrderDetail>();

    public virtual Document? Document { get; set; }

    public virtual ICollection<OrderAssignment> OrderAssignments { get; set; } = new List<OrderAssignment>();

    public virtual Payment Payment { get; set; } = null!;

    public virtual ShippingFee ShippingFee { get; set; } = null!;

    public virtual VoucherPromotion? VoucherPromotion { get; set; }
}
