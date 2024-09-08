using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Data.Entities;

public partial class VoucherPromotion
{
    public int VoucherPromotionId { get; set; }

    public string? VoucherPromotionCode { get; set; }

    public virtual ICollection<DeliveryOrder> DeliveryOrders { get; set; } = new List<DeliveryOrder>();
}
