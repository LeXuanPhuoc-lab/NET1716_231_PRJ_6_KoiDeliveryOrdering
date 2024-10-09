using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KoiDeliveryOrdering.Data.Entities;

public partial class Document
{
    public int Id { get; set; }

    public Guid DocumentId { get; set; }

    public string? DocumentNumber { get; set; }

    public string DocumentType { get; set; } = null!;

    public DateTime IssueDate { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public string ConsigneeName { get; set; } = null!;

    public string ConsigneePhone { get; set; } = null!;

    public string ConsigneeAddress { get; set; } = null!;

    public string ExporterName { get; set; } = null!;

    public string ExporterPhone { get; set; } = null!;

    public string ExporterAddress { get; set; } = null!;

    public string DispatchMethod { get; set; } = null!;

    public string FinalDestination { get; set; } = null!;

    public string? TransportationNo { get; set; }

    public string TransportationType { get; set; } = null!;

    public string PortOfLoading { get; set; } = null!;

    public string PortOfDischarge { get; set; } = null!;

    public decimal? TaxFee { get; set; }

    public decimal? ShippingFee { get; set; }

    public decimal? AssurranceFee { get; set; }

    [JsonIgnore]
    public virtual ICollection<DeliveryOrder> DeliveryOrders { get; set; } = new List<DeliveryOrder>();

    public virtual ICollection<DocumentDetail> DocumentDetails { get; set; } = new List<DocumentDetail>();
}
