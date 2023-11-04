namespace Core.Model
{
    public class Location : BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Details { get; set; }
		public int SiteId { get; set; }
		public Site Site { get; set; }
	}
}