using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Data.Entities;

public partial class Animal
{
    public int Id { get; set; }

    public Guid AnimalId { get; set; }

    public string? Breed { get; set; }

    public string? ColorPattern { get; set; }

    public decimal? Size { get; set; }

    public int? Age { get; set; }

    public decimal? EstimatedPrice { get; set; }

    public string? HealthStatus { get; set; }

    public bool? IsAvailable { get; set; }

    public string? OriginCountry { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public int AnimalTypeId { get; set; }

    public virtual AnimalType AnimalType { get; set; } = null!;

    public virtual ICollection<DeliveryOrderDetail> DeliveryOrderDetails { get; set; } = new List<DeliveryOrderDetail>();
}
