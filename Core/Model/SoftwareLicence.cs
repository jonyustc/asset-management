namespace Core.Model
{
    public class SoftwareLicence
    {
        public int Id { get; set; }
		public int PackageId { get; set; }

		public virtual SoftawarePackage SoftawarePackage { get; set; }
		public int legalEntityId { get; set; }

		public virtual LegalEntity LegalEntity { get; set; }
		public DateTime PurchaseDate { get; set; }
		public DateTime ExpireDate { get; set; }
		public bool IsPaid { get; set; }

		public string CreatedBy { get; set; }
		public string UpdatedBy { get; set; }
		public string DeletedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public DateTime? DeleteDate { get; set; }
		public bool IsDelete { get; set; }
    }
}