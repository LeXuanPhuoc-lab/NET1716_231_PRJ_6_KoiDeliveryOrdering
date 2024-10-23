namespace KoiDeliveryOrdering.Data.Dtos.Documents;

public class SearchDocumentQueryDto
{
    public string? DocumentNumber { get; set; } = "";
    public string? DocumentType { get; set; } = "";
    public string? TransportationType { get; set; } = "";
}