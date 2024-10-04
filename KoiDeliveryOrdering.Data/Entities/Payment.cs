using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KoiDeliveryOrdering.Data.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public string PaymentMethod { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<DeliveryOrder> DeliveryOrders { get; set; } = new List<DeliveryOrder>();
}
