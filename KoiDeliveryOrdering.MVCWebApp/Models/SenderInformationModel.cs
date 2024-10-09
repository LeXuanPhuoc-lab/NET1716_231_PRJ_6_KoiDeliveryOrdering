namespace KoiDeliveryOrdering.MVCWebApp.Models
{
	public class SenderInformationModel
	{
		public int SenderInformationId { get; set; }

		public int UserId { get; set; }

		public string SenderName { get; set; } = null!;

		public string SenderPhone { get; set; } = null!;

		public string CityProvince { get; set; } = null!;

		public string Street { get; set; } = null!;

		public string District { get; set; } = null!;

		public string Ward { get; set; } = null!;

		public double? Longitude { get; set; }

		public double? Latitude { get; set; }

		public string? SenderAppointmentTime { get; set; }

		public virtual UserModel User { get; set; } = null!;

        public virtual ICollection<DeliveryOrderModel> DeliveryOrders { get; set; } = new List<DeliveryOrderModel>();
    }
}
