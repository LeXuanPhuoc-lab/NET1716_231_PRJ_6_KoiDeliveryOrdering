namespace KoiDeliveryOrdering.API.Payloads.Requests
{
    public class OrderAssignmentRequest
    {
    }

    public class OrderAssignmentCreateRequest
    {
        public int DeliveryOrderId { get; set; }

        public int DriverId { get; set; }

        public int FishCarerId { get; set; }

        public int AssignedTruckId { get; set; }

        public DateTime AssignedDate { get; set; }

        public string DeliveryStatus { get; set; } = null!;
    }

    public class OrderAssignmentUpdateRequest
    {
        public int OrderAssignmentId { get; set; }

        public int DeliveryOrderId { get; set; }

        public int DriverId { get; set; }

        public int FishCarerId { get; set; }

        public int AssignedTruckId { get; set; }

        public DateTime AssignedDate { get; set; }

        public string DeliveryStatus { get; set; } = null!;
    }
}
