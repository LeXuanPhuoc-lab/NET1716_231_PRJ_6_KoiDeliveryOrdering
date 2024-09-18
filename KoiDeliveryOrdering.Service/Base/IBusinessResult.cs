namespace KoiDeliveryOrdering.Business.Base
{
    public interface IBusinessResult
    {
        int Status { get; set; }
        string? Message { get; set; }
        object? Data { get; set; }
    }
}