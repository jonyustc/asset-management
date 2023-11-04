using System.Runtime.Serialization;

namespace Core.Model
{
    public class Event : BaseEntity
    {
        public Event()
        {
        }

        public Event(Asset asset,bool isActive)
        {
            Asset = asset;
            IsActive = isActive;
        }

        public EventStatus EventStatus { get; set; } = EventStatus.Available;
        public DateTimeOffset EventDate { get; set; } = DateTime.Now;
        public DateTimeOffset ExpireDate { get; set; } = DateTime.Now;
        public string AssignTo { get; set; }
        public int? EmployeeId { get; set; }
        public int? customerId { get; set; }
        public int? siteId { get; set; }
        public int? departmentId { get; set; }
        public int? locationId { get; set; }
        public string Note { get; set; }
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
    }

    public enum EventStatus
    {
        [EnumMember(Value ="Available")]
        Available,
        [EnumMember(Value ="Deployed")]
        Deployed,
        [EnumMember(Value ="UnderSupport")]
        UnderSupport,
        [EnumMember(Value ="Broken")]
        Broken,
        [EnumMember(Value ="Dispose")]
        Dispose,
        [EnumMember(Value ="Repair")]
        Repair,
        [EnumMember(Value ="Lost")]
        Lost,
        [EnumMember(Value ="Found")]
        Found,
        [EnumMember(Value ="Warranty")]
        Warranty,
        [EnumMember(Value ="Maintenance")]
        Maintenance
    }
}