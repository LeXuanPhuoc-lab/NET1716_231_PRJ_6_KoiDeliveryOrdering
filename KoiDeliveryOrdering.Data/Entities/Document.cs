using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KoiDeliveryOrdering.Data.Entities;

public partial class Document
{
    public int Id { get; set; }

    public Guid DocumentId { get; set; }

    public string DocumentNumber { get; set; } = null!;

    public string DocumentType { get; set; } = null!; //Import, Export, Health

    public DateTime IssueDate { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public string? ConsigneeName { get; set; }

    public string? ConsigneePhone { get; set; }

    public string? ConsigneeAddress { get; set; }

    public string? ExporterName { get; set; }

    public string? ExporterPhone { get; set; }

    public string? ExporterAddress { get; set; }

    public string? DispatchMethod { get; set; } = null!;

    public string? FinalDestination { get; set; } = null!;

    public string? TransportationNo { get; set; }

    public string? TransportationType { get; set; } = null!;

    public string? PortOfLoading { get; set; } = null!;

    public string? PortOfDischarge { get; set; } = null!;

    public decimal? ShippingFee { get; set; }

    public int DeliveryOrderId { get; set; }

    public virtual DeliveryOrder DeliveryOrder { get; set; } = null!;

    public virtual ICollection<DocumentDetail> DocumentDetails { get; set; } = new List<DocumentDetail>();
}