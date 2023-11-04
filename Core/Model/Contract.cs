namespace Core.Model
{
    public class Contract : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string ContractNo { get; set; }
        public decimal Cost { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Vendor { get; set; }
        public string Phone { get; set; }
        public int NoOfLicenses { get; set; }
        public bool IsSoftware { get; set; }



    }
}