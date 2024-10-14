using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using KoiDeliveryOrdering.Data.Enum;

namespace KoiDeliveryOrdering.Data.Dtos.Documents;

public record DocumentMutationDto
{
    [Required]
    [DisplayName("Document type")]
    public string DocumentType { get; set; } = null!; //Import, Export, Health

    [Required] [DisplayName("Issue date")] public DateOnly? IssueDate { get; set; }

    [Required]
    [DisplayName("Expiration date")]
    public DateOnly? ExpirationDate { get; set; }

    [Required]
    [DisplayName("Consignee name")]
    public string? ConsigneeName { get; set; }

    [Required]
    [DisplayName("Consignee phone")]
    [RegularExpression(@"^(\+84|0)[1-9]\d{8}$", ErrorMessage = "Consignee phone is invalid")]
    public string? ConsigneePhone { get; set; }

    [Required]
    [DisplayName("Consignee address")]
    public string? ConsigneeAddress { get; set; }

    [Required]
    [DisplayName("Exporter name")]
    public string? ExporterName { get; set; }

    [Required]
    [DisplayName("Exporter phone")]
    [RegularExpression(@"^(\+84|0)[1-9]\d{8}$", ErrorMessage = "Exporter phone is invalid")]
    public string? ExporterPhone { get; set; }

    [Required]
    [DisplayName("Exporter address")]
    public string? ExporterAddress { get; set; }

    [Required]
    [DisplayName("Dispatch method")]
    public string? DispatchMethod { get; set; } = null!;

    [Required]
    [DisplayName("Transportation type")]
    public string? TransportationType { get; set; } = null!;

    [Required]
    [DisplayName("Delivery order id")]
    public int DeliveryOrderId { get; set; }

    public List<DocumentDetailDto> DocumentDetails { get; set; } = [];
}