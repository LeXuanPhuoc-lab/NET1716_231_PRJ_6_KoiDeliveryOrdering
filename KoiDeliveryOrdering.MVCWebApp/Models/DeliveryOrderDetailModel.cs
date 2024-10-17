using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.MVCWebApp.Models
{
    public class DeliveryOrderDetailModel
    {
        public int Id { get; set; }

        public Guid DeliveryOrderDetailId { get; set; }

        public int AnimalId { get; set; }

        public int DeliveryOrderId { get; set; }

        public string? PreDeliveryHealthStatus { get; set; }

        public string? PostDeliveryHealthStatus { get; set; }

        public virtual AnimalModel Animal { get; set; } = null!;

        //public virtual ICollection<DailyCareSchedule> DailyCareSchedules { get; set; } = new List<DailyCareSchedule>();

        public virtual DeliveryOrderModel DeliveryOrder { get; set; } = null!;
    }
}
