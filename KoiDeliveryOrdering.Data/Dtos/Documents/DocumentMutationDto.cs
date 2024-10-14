using System.ComponentModel.DataAnnotations;
using KoiDeliveryOrdering.Data.Enum;

namespace KoiDeliveryOrdering.Data.Dtos.Documents;

public record DocumentMutationDto
{
    [EnumDataType(typeof(EDocumentType))]
    [Required]
    public string DocumentType { get; set; } = null!; //Import, Export, Health

    [Required]
    public DateOnly? IssueDate { get; set; }

    [Required]
    public DateOnly? ExpirationDate { get; set; }

    [Required]
    public string? ConsigneeName { get; set; }

    [Required]
    public string? ConsigneePhone { get; set; }

    [Required]
    public string? ConsigneeAddress { get; set; }

    [Required]
    public string? ExporterName { get; set; }

    [Required]
    public string? ExporterPhone { get; set; }

    [Required]
    public string? ExporterAddress { get; set; }

    [Required]
    public string? DispatchMethod { get; set; } = null!;

    [Required]
    public string? TransportationType { get; set; } = null!;

    [Required]
    public int DeliveryOrderId { get; set; }

    public List<DocumentDetailDto> DocumentDetails { get; set; } = [];
}