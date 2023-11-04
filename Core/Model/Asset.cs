namespace Core.Model
{
    public class Asset : BaseEntity
	{
		public string Name { get; set; }
		public string AssetTagId { get; set; }
		public string Description { get; set; }
		public DateTimeOffset? PurchasedDate { get; set; }
		public string PurchasedFrom { get; set; }
		public decimal? Cost { get; set; }
		public string Brand { get; set; }
		public string Model { get; set; }
		public string SerialNo { get; set; }
		public string Category { get; set; }
		public int CompanyId { get; set; }
		public Company Company { get; set; }
		public string Department { get; set; }
		public string Location { get; set; }
		public DateTimeOffset? WarrantyExpired { get; set; }
		
		public ICollection<Photo> Photos { get; set; }
		public ICollection<Event> Events { get; set; }
	}
}