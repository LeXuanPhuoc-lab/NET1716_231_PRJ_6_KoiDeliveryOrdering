using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KoiDeliveryOrdering.Data.Entities;

public partial class VoucherPromotion
{
    public int VoucherPromotionId { get; set; }

    public string? VoucherPromotionCode { get; set; }

    public decimal PromotionRate { get; set; }

    [JsonIgnore]
    public virtual ICollection<UserDTO> Users { get; set; } = new List<UserDTO>();

    [JsonIgnore]
    public virtual ICollection<DeliveryOrder> DeliveryOrders { get; set; } = new List<DeliveryOrder>();
}
