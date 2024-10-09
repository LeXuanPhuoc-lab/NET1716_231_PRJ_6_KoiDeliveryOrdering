using System.Text.Json.Serialization;

namespace KoiDeliveryOrdering.MVCWebApp.Models
{
    public class PaymentModel
    {
        public int PaymentId { get; set; }

        public string PaymentMethod { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<DeliveryOrderModel> DeliveryOrders { get; set; } = new List<DeliveryOrderModel>();
    }
}
