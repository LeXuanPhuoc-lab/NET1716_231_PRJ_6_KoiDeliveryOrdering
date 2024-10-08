namespace KoiDeliveryOrdering.MVCWebApp.Models
{
    public class CareTaskModel
    {
        public int CareTaskId { get; set; }

        public string TaskName { get; set; } = null!;

        public string? Description { get; set; }

        public string? Unit { get; set; }
        public string? Priority { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; } = DateTime.Now;
        public DateTime? DueDate { get; set; } // Ngày đến hạn của task

        public string? AssignedTo { get; set; } // Người phụ trách thực hiện task

        public DateTime? CompletedAt { get; set; } // Ngày hoàn thành task (nếu có)

        public bool IsRecurring { get; set; } // Task có lặp lại không

        public string? Notes { get; set; } // Ghi chú bổ sung cho task

        public virtual ICollection<DailyCareScheduleModels> DailyCareSchedules { get; set; } = new List<DailyCareScheduleModels>();
    }
}
