namespace KoiDeliveryOrdering.MVCWebApp.Payloads.Filters
{
    public class DeliveryOrderFilter
    {
        public string? SenderAddressSearch { get; set; }
        public string? RecipientAddressSearch { get; set; }
        //public DateTime? CreateDateSearch { get; set; }
        //public DateTime? DeliveryDateSearch { get; set; }
        public string? PaymentMethodSearch { get; set; }
        public decimal? ShippingFeeSearch { get; set; }
        public decimal? TotalAmountSearch { get; set; }
        public string? OrderStatusSearch { get; set; }
        public string? SearchValue { get; set; }
        public bool? IsManySearch { get; set; } = false;
        public string? OrderBy { get; set; }
        public int PageIndex { get; set; } = 1;
    }
}
