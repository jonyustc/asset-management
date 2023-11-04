namespace Core.Model
{
    public class LegalEntity : BaseEntity
    {
        public LegalEntity()
        {
			EntityId = Guid.NewGuid();
        }
		public Guid EntityId { get; set; }
		public string LegalEntityName { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public virtual ICollection<Company> Companies { get; set; }
    }
}