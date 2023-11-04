namespace Core.Model
{
    public class Warehouse : BaseEntity
	{
		public string WarehouseName { get; set; }
		public string Description { get; set; }
		public int LocationId { get; set; }
		public virtual Location Location { get; set; }
	}
}