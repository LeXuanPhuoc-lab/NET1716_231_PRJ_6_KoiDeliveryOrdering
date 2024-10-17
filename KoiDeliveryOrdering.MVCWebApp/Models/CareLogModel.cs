namespace KoiDeliveryOrdering.MVCWebApp.Models
{
    public class CareLogModel
    {
        public int Id { get; set; }

        public int DailyCareScheduleId { get; set; }

        public DateTime LogDate { get; set; }

        public decimal? ActualValue { get; set; }

        public string? Status { get; set; }

        public string? StaffComments { get; set; }

        public int StaffId { get; set; }

        public virtual DailyCareScheduleModels DailyCareSchedule { get; set; } = null!;

        /*        public virtual Staff Staff { get; set; } = null!;*/
    }
}
