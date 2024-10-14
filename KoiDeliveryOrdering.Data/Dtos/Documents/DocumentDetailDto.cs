using System.ComponentModel.DataAnnotations;

namespace KoiDeliveryOrdering.Data.Dtos.Documents;

public class DocumentDetailDto
{
    public string? DocumentDetailDescription { get; set; }

    [Required]
    public string ItemName { get; set; } = null!;

    [Required]
    public decimal ItemWeight { get; set; }

    [Required]
    public string ItemCategory { get; set; } = null!;
    
    public decimal ItemEstimatePrice { get; set; }

    [Required]
    public int ItemQuantity { get; set; }
}