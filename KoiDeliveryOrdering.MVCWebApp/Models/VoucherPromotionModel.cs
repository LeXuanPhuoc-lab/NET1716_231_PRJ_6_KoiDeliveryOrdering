using System.Text.Json.Serialization;

namespace KoiDeliveryOrdering.MVCWebApp.Models
{
    public class VoucherPromotionModel
    {
        public int VoucherPromotionId { get; set; }

        public string? VoucherPromotionCode { get; set; }

        public decimal PromotionRate { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserModel> Users { get; set; } = new List<UserModel>();

        [JsonIgnore]
        public virtual ICollection<DeliveryOrderModel> DeliveryOrders { get; set; } = new List<DeliveryOrderModel>();
    }
}
