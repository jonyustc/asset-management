namespace Core.Model
{
    public class SoftawarePackage : BaseEntity
    {
        public string Name { get; set; }
		public int VersionId { get; set; }
		public virtual SoftwareVersion SoftwareVersion { get; set; }
		public double Price { get; set; }
		public int YearCount { get; set; }
    }
}