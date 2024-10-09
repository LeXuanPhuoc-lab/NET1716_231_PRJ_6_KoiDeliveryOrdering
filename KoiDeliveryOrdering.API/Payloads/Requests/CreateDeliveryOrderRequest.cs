﻿namespace KoiDeliveryOrdering.API.Payloads.Requests
{
    public class CreateDeliveryOrderRequest
    {
        public string RecipientAddress { get; set; } = null!;

        public string RecipientName { get; set; } = null!;

        public string RecipientPhone { get; set; } = null!;

        public double? RecipientLongitude { get; set; }

        public double? RecipientLatitude { get; set; }

        public string? RecipientAppointmentTime { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string? OrderStatus { get; set; } 

        public decimal? TotalAmount { get; set; }

        public decimal? TaxFee { get; set; }

        public int PaymentId { get; set; }

        public bool? IsPurchased { get; set; }

        public bool IsSenderPurchase { get; set; }

        public bool IsInternational { get; set; }

        public int? VoucherPromotionId { get; set; }

        public int ShippingFeeId { get; set; }

        public int SenderInformationId { get; set; }

        public int? DocumentId { get; set; }

        // Initiate address handling properities 
        public string? ProvinceName { get; set; }
        public string? DistrictName { get; set; }
        public string? WardName { get; set; }
        public List<CreateAnimalRequest> Animals { get; set; } = new();
    }
}