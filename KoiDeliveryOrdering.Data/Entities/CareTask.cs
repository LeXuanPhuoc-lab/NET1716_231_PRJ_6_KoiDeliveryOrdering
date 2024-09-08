using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Data.Entities;

public partial class CareTask
{
    public int CareTaskId { get; set; }

    public string TaskName { get; set; } = null!;

    public string? Description { get; set; }

    public string? Unit { get; set; }

    public virtual ICollection<DailyCareSchedule> DailyCareSchedules { get; set; } = new List<DailyCareSchedule>();
}
