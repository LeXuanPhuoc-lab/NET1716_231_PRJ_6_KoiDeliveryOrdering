using System.Text.Json.Serialization;

namespace KoiDeliveryOrdering.MVCWebApp.Models
{
    public class ShippingFeeModel
    {
        public int ShippingFeeId { get; set; }

        public decimal DistanceRangeFrom { get; set; }

        public decimal DistanceRangeTo { get; set; }

        public string? ServiceCode { get; set; }

        public int WeightClass { get; set; }

        public decimal BaseFee { get; set; }

        public string? EstimatedTime { get; set; }

        [JsonIgnore]
        public virtual ICollection<DeliveryOrderModel> DeliveryOrders { get; set; } 
            = new List<DeliveryOrderModel>();
    }
}
