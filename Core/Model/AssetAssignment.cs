namespace Core.Model
{
    public class AssetAssignment : BaseEntity
	{
		public string UserId { get; set; }
		public virtual ApplicationUser AssetUser { get; set; }
		public int AssetId { get; set; }
		public virtual Asset AssetItem { get; set; }
	}
}