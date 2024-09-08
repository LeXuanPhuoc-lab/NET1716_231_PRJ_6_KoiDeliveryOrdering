using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Data.Entities;

public partial class DailyCareSchedule
{
    public int DailyCareScheduleId { get; set; }

    public int CareTaskId { get; set; }

    public string? TaskFrequency { get; set; }

    public decimal? RecommendedValue { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int DeliverOrderDetailId { get; set; }

    public virtual ICollection<CareLog> CareLogs { get; set; } = new List<CareLog>();

    public virtual CareTask CareTask { get; set; } = null!;

    public virtual DeliveryOrderDetail DeliverOrderDetail { get; set; } = null!;
}
