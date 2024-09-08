using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Data.Entities;

public partial class DocumentDetail
{
    public int DocumentDetailId { get; set; }

    public string? DocumentDetailDescription { get; set; }

    public string ItemName { get; set; } = null!;

    public decimal ItemWeight { get; set; }

    public string ItemCategory { get; set; } = null!;

    public decimal ItemEstimatePrice { get; set; }

    public int? ItemQuantity { get; set; }

    public int DocumentId { get; set; }

    public virtual Document Document { get; set; } = null!;
}
