namespace Core.Model
{
    public class Site : BaseEntity
    {
        public string Name { get; set; }
		public string Address { get; set; }
		public string Mobile { get; set; }
		public string Telephone { get; set; }
		public string Street { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string PostalCode { get; set; }
		public int CopmanyId { get; set; }
		public Company Company { get; set; }
        public ICollection<Location> Locations { get; set; }
		public ICollection<Asset> Assets { get; set;}
    }
}