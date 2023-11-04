namespace Core.Model
{
    public class BaseEntity
    {
        public int Id { get; set; }
		public string CreatedBy { get; set; }
		public string UpdatedBy { get; set; }
		public string DeletedBy { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public DateTime? UpdatedDate { get; set; }
		public DateTime? DeleteDate { get; set; }
		public bool IsDelete { get; set; }
		public bool IsActive { get; set; }
		public string Remarks { get; set; }
    }
}