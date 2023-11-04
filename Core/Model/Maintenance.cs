namespace Core.Model
{
    public class Maintenance : BaseEntity
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime DueDate { get; set; }
        public string MaintenanceBy { get; set; }
        public MaintenanceStatus MaintenanceStatus { get; set; }
        public DateTime DateCompleted { get; set; }
        public decimal Cost { get; set; }
        public bool Repeating { get; set; }
    }

    public enum MaintenanceStatus
    {
        Scheduled,
        InProgress,
        OnHold,
        Cancelled,
        Completed
    }
}