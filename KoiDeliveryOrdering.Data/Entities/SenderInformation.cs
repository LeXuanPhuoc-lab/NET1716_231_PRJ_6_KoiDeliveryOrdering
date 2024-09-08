using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Data.Entities;

public partial class SenderInformation
{
    public int SenderInformationId { get; set; }

    public Guid UserId { get; set; }

    public string SenderName { get; set; } = null!;

    public string SenderPhone { get; set; } = null!;

    public string CityProvince { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string District { get; set; } = null!;

    public string Ward { get; set; } = null!;

    public double? Longitude { get; set; }

    public double? Latitude { get; set; }

    public string? SenderAppointmentTime { get; set; }

    public virtual User User { get; set; } = null!;
}
