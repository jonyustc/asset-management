namespace API.Dto
{
    public class AssetToReturnDto : BaseEntityDto
    {
        public string Name { get; set; }
		public string AssetTagId { get; set; }
		public string Description { get; set; }
		public string PurchasedDate { get; set; }
		public string WarrantyExpired { get; set; }
		public string PurchasedFrom { get; set; }
		public decimal? Cost { get; set; }
		public string Brand { get; set; }
		public string SerialNo { get; set; }
		public string Model { get; set; }
		public string PhotoUrl { get; set; }
        public string Status { get; set; }
		public int CompanyId { get; set; }

		public string Location { get; set; }
		public string Category { get; set; }
		public string Department { get; set; }
    }
}