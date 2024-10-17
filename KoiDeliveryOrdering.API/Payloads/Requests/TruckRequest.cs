using Microsoft.Extensions.Logging.Abstractions;

namespace KoiDeliveryOrdering.API.Payloads.Requests
{
    public class TruckRequest
    {
    }

    public class CreateTruckRequest
    {
        public string TruckLicensePlate { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Capacity { get; set; }
        public bool IsActive { get; set; } 
        public int GarageId { get; set; }
        public DateTime LastMaintenanceDate { get; set; } 
    }

    public class UpdateTruckRequest
    {
        public int TruckId { get; set; }
        public string TruckLicensePlate { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Capacity { get; set; }
        public bool IsActive { get; set; }
        public int GarageId { get; set; }
        public DateTime LastMaintenanceDate { get; set; }
    }
}
