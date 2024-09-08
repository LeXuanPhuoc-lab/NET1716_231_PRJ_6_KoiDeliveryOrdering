using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Data.Entities;

public partial class AnimalType
{
    public int AnimalTypeId { get; set; }

    public string? AnimalTypeDesc { get; set; }

    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();
}
