using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Data.Entities;

public partial class Truck
{
    public int TruckId { get; set; }

    public string TruckLicensePlate { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int Capacity { get; set; }

    public bool IsActive { get; set; }

    public int GarageId { get; set; }

    public DateTime? LastMaintenanceDate { get; set; }

    public virtual Garage Garage { get; set; } = null!;

    public virtual ICollection<OrderAssignment> OrderAssignments { get; set; } = new List<OrderAssignment>();
}
