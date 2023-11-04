using System.Text.Json.Serialization;
using Core.Model;

namespace API.Dto
{
    public class AssetToCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
		public string AssetTagId { get; set; }
		public string Description { get; set; }
		public string PurchasedDate { get; set; }
		public string WarrantyExpired { get; set; }
		public string PurchasedFrom { get; set; }
		public decimal? Cost { get; set; }
		public string Brand { get; set; }
		public string Model { get; set; }
		public string SerialNo { get; set; }
		public string Category { get; set; }
		public int CompanyId { get; set; } = 1;

		public string Location { get; set; }
		public string Department { get; set; }
		public List<string> PhotoUrl { get; set; }
		//public string File { get; set; }
		
		[JsonIgnore]
        public List<IFormFile> File { get; set; }
		
		// public Event Event { get; set; }
		// public Photo Photo { get; set; }
    }
}