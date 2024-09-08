using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Data.Entities;

public partial class DeliveryOrderDetail
{
    public int Id { get; set; }

    public Guid DeliveryOrderDetailId { get; set; }

    public int AnimalId { get; set; }

    public int DeliveryOrderId { get; set; }

    public string? PreDeliveryHealthStatus { get; set; }

    public string? PostDeliveryHealthStatus { get; set; }

    public virtual Animal Animal { get; set; } = null!;

    public virtual ICollection<DailyCareSchedule> DailyCareSchedules { get; set; } = new List<DailyCareSchedule>();

    public virtual DeliveryOrder DeliveryOrder { get; set; } = null!;
}
