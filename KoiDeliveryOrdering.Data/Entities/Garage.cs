using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Data.Entities;

public partial class Garage
{
    public int GarageId { get; set; }

    public string GarageName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? ManagerName { get; set; }

    public string CityProvince { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string District { get; set; } = null!;

    public string Ward { get; set; } = null!;

    public double? Longitude { get; set; }

    public double? Latitude { get; set; }

    public virtual ICollection<Truck> Trucks { get; set; } = new List<Truck>();
}
