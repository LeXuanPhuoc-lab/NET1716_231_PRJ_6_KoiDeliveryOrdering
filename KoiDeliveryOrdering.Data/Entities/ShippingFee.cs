using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Data.Entities;

public partial class ShippingFee
{
    public int ShippingFeeId { get; set; }

    public decimal DistanceRangeFrom { get; set; }

    public decimal DistanceRangeTo { get; set; }

    public string? ServiceCode { get; set; }

    public int WeightClass { get; set; }

    public decimal BaseFee { get; set; }

    public string? EstimatedTime { get; set; }

    public virtual ICollection<DeliveryOrder> DeliveryOrders { get; set; } = new List<DeliveryOrder>();
}
