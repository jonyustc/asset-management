namespace API.Dto
{
    public class CompanyCreateDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public string Web { get; set; }
		public string Mobile { get; set; }
		public string Telephone { get; set; }
		public string Street { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
		public string LogoUrl { get; set; }
        public int? Status { get; set; }
    }
}